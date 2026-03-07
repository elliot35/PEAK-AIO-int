using HarmonyLib;
using ImGuiNET;
using Photon.Pun;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

[HarmonyPatch(typeof(PointPinger), "ReceivePoint_Rpc")]
public class PointPingPatch
{
    static void Postfix(Vector3 point, Vector3 hitNormal, PointPinger __instance)
    {
        try
        {
            if (!ConfigManager.TeleportToPing.Value)
                return;

            var owner = __instance.character?.photonView?.Owner;
            if (owner != null && owner == PhotonNetwork.LocalPlayer)
            {
                if (Character.localCharacter != null && !Character.localCharacter.data.dead)
                {
                    Vector3 safePoint = point + Vector3.up;
                    Character.localCharacter.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[] {
                        safePoint, true
                    });

                    ConfigManager.Logger.LogInfo("[Patch] Teleported to ping!");
                }
            }
        }
        catch (Exception ex)
        {
            ConfigManager.Logger.LogError("[Patch] Exception: " + ex);
        }
    }
}


[HarmonyPatch(typeof(Character), "Update")]
public class FlyPatch
{
    private static bool isFlying = false;
    private static Vector3 flyVelocity = Vector3.zero;

    public static void SetFlying(bool enable)
    {
        isFlying = enable;
        flyVelocity = Vector3.zero;

        ConfigManager.Logger.LogInfo($"[FlyMod] Flight {(enable ? "enabled" : "disabled")}.");
    }

    public static bool IsFlying => isFlying;

    static void Postfix(Character __instance)
    {
        if (!ConfigManager.FlyMod.Value && !isFlying)
            return;

        if (!__instance.IsLocal)
            return;

        if (!ConfigManager.FlyMod.Value)
        {
            if (isFlying)
            {
                isFlying = false;
                flyVelocity = Vector3.zero;
                ConfigManager.Logger.LogInfo("[FlyMod] Flight disabled.");
            }
            return;
        }

        if (!isFlying)
        {
            isFlying = true;
            ConfigManager.Logger.LogInfo("[FlyMod] Flight enabled.");
        }

        __instance.data.isGrounded = true;
        __instance.data.sinceGrounded = 0f;
        __instance.data.sinceJump = 0f;

        Vector3 input = __instance.input.movementInput;
        Vector3 forward = __instance.data.lookDirection_Flat.normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        Vector3 moveVec = forward * input.y + right * input.x;

        if (__instance.input.jumpIsPressed)
            moveVec += Vector3.up;

        if (__instance.input.crouchIsPressed)
            moveVec += Vector3.down;

        float speed = ConfigManager.FlySpeed.Value;
        float accel = ConfigManager.FlyAcceleration.Value;

        flyVelocity = Vector3.Lerp(flyVelocity, moveVec.normalized * speed, Time.deltaTime * accel);

        var partList = __instance.refs.ragdoll.partList;
        for (int i = 0; i < partList.Count; i++)
        {
            var rig = partList[i]?.Rig;
            if (rig != null)
                rig.linearVelocity = flyVelocity;
        }
    }
}

public static class CJKFontPatch
{
    private static bool fontsLoaded = false;
    private static GCHandle rangesHandle;

    // Combined glyph ranges: Latin + CJK + Hiragana/Katakana + Hangul
    private static readonly ushort[] CombinedRanges = {
        0x0020, 0x00FF, // Basic Latin + Latin-1 Supplement
        0x2000, 0x206F, // General Punctuation
        0x3000, 0x30FF, // CJK Symbols, Hiragana, Katakana
        0x3131, 0x3163, // Korean Alphabets (Jamo)
        0x31F0, 0x31FF, // Katakana Phonetic Extensions
        0x4E00, 0x9FFF, // CJK Unified Ideographs
        0xAC00, 0xD7A3, // Hangul Syllables
        0xFF00, 0xFFEF, // Halfwidth/Fullwidth Forms
        0x0000          // Null terminator
    };

    private static readonly string[] FontCandidates = {
        @"C:\Windows\Fonts\malgun.ttf",
        @"C:\Windows\Fonts\malgunsl.ttf",
        @"C:\Windows\Fonts\msyh.ttc",
        @"C:\Windows\Fonts\meiryo.ttc",
        @"C:\Windows\Fonts\yugothm.ttc",
    };

    public static unsafe void Prefix()
    {
        if (fontsLoaded) return;
        fontsLoaded = true;

        try
        {
            var io = ImGui.GetIO();
            var fonts = io.Fonts;

            rangesHandle = GCHandle.Alloc(CombinedRanges, GCHandleType.Pinned);
            IntPtr rangesPtr = rangesHandle.AddrOfPinnedObject();

            string chosenFont = null;
            foreach (var path in FontCandidates)
            {
                if (System.IO.File.Exists(path))
                {
                    chosenFont = path;
                    break;
                }
            }

            if (chosenFont != null)
            {
                var cjkFont = fonts.AddFontFromFileTTF(chosenFont, 14.0f, default, rangesPtr);
                ConfigManager.Logger.LogInfo($"[PEAK AIO] CJK font ({System.IO.Path.GetFileName(chosenFont)}): {(cjkFont.NativePtr != null ? "OK" : "FAILED")}");

                if (cjkFont.NativePtr != null)
                    io.NativePtr->FontDefault = cjkFont.NativePtr;
            }
            else
            {
                ConfigManager.Logger.LogWarning("[PEAK AIO] No suitable CJK font found, using default font.");
            }

            bool built = fonts.Build();
            ConfigManager.Logger.LogInfo($"[PEAK AIO] Atlas build: {(built ? "OK" : "FAILED")}, size: {fonts.TexWidth}x{fonts.TexHeight}, fonts: {fonts.Fonts.Size}");

            if (rangesHandle.IsAllocated) rangesHandle.Free();
        }
        catch (Exception ex)
        {
            if (rangesHandle.IsAllocated) rangesHandle.Free();
            ConfigManager.Logger.LogWarning("[PEAK AIO] CJK font loading failed: " + ex.Message);
        }
    }
}

public static class ImGuiInputPatch
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    [DllImport("cimgui", CallingConvention = CallingConvention.Cdecl)]
    private static extern unsafe void ImGuiIO_AddMouseButtonEvent(ImGuiIO* self, int mouse_button, byte mouse_down);

    [DllImport("cimgui", CallingConvention = CallingConvention.Cdecl)]
    private static extern unsafe void ImGuiIO_AddMouseWheelEvent(ImGuiIO* self, float wheel_x, float wheel_y);

    private const int VK_LBUTTON = 0x01;
    private const int VK_RBUTTON = 0x02;
    private const int VK_MBUTTON = 0x04;

    private static bool cachedLButton, cachedRButton, cachedMButton;
    private static float cachedScroll;
    private static bool forceInput;
    private static int logFrames;
    private static int renderLogFrames;
    private static bool nativeApiWorks = true;

    public static void SetForceInput(bool enabled) => forceInput = enabled;

    public static void CaptureInput()
    {
        cachedLButton = (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0;
        cachedRButton = (GetAsyncKeyState(VK_RBUTTON) & 0x8000) != 0;
        cachedMButton = (GetAsyncKeyState(VK_MBUTTON) & 0x8000) != 0;

        try { cachedScroll = UnityEngine.Input.mouseScrollDelta.y; }
        catch { cachedScroll = 0f; }
    }

    public static unsafe void ApplyToImGui()
    {
        if (!forceInput) return;

        try
        {
            var io = ImGui.GetIO();

            if (nativeApiWorks)
            {
                try
                {
                    var ioPtr = io.NativePtr;
                    ImGuiIO_AddMouseButtonEvent(ioPtr, 0, cachedLButton ? (byte)1 : (byte)0);
                    ImGuiIO_AddMouseButtonEvent(ioPtr, 1, cachedRButton ? (byte)1 : (byte)0);
                    ImGuiIO_AddMouseButtonEvent(ioPtr, 2, cachedMButton ? (byte)1 : (byte)0);
                    ImGuiIO_AddMouseWheelEvent(ioPtr, 0f, cachedScroll);
                }
                catch
                {
                    nativeApiWorks = false;
                    ConfigManager.Logger.LogWarning("[InputPatch] Native event API failed, using legacy.");
                }
            }

            io.MouseDown[0] = cachedLButton;
            io.MouseDown[1] = cachedRButton;
            io.MouseDown[2] = cachedMButton;
            io.MouseWheel = cachedScroll;

            if (logFrames < 3)
            {
                logFrames++;
                ConfigManager.Logger.LogInfo(
                    $"[InputPatch] nativeApi={nativeApiWorks} " +
                    $"imguiPos=({io.MousePos.X:F0},{io.MousePos.Y:F0}) " +
                    $"displaySize=({io.DisplaySize.X:F0},{io.DisplaySize.Y:F0})");
            }
        }
        catch (Exception ex)
        {
            if (logFrames < 5)
            {
                logFrames++;
                ConfigManager.Logger.LogError($"[InputPatch] Error: {ex}");
            }
        }
    }

    public static void LogPostNewFrame()
    {
        if (!forceInput) return;
        if (renderLogFrames >= 15) return;

        try
        {
            var io = ImGui.GetIO();
            var winPos = ImGui.GetWindowPos();
            var winSize = ImGui.GetWindowSize();
            bool winHovered = ImGui.IsWindowHovered(ImGuiHoveredFlags.RootAndChildWindows | ImGuiHoveredFlags.AllowWhenBlockedByActiveItem);
            bool anyItemHovered = ImGui.IsAnyItemHovered();
            bool winFocused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            bool rectHover = ImGui.IsMouseHoveringRect(
                winPos,
                new System.Numerics.Vector2(winPos.X + winSize.X, winPos.Y + winSize.Y),
                false);

            if (cachedLButton)
            {
                renderLogFrames++;
                ConfigManager.Logger.LogInfo(
                    $"[InputPatch-Render] CLICK " +
                    $"winHovered={winHovered} rectHover={rectHover} winFocused={winFocused} " +
                    $"wantCapture={io.WantCaptureMouse} configFlags=0x{(int)io.ConfigFlags:X} " +
                    $"imguiPos=({io.MousePos.X:F0},{io.MousePos.Y:F0}) " +
                    $"winPos=({winPos.X:F0},{winPos.Y:F0}) winSize=({winSize.X:F0},{winSize.Y:F0})");
            }
            else if (renderLogFrames < 5)
            {
                renderLogFrames++;
                ConfigManager.Logger.LogInfo(
                    $"[InputPatch-Render] " +
                    $"winHovered={winHovered} rectHover={rectHover} winFocused={winFocused} " +
                    $"wantCapture={io.WantCaptureMouse} configFlags=0x{(int)io.ConfigFlags:X} " +
                    $"imguiPos=({io.MousePos.X:F0},{io.MousePos.Y:F0}) " +
                    $"winPos=({winPos.X:F0},{winPos.Y:F0}) winSize=({winSize.X:F0},{winSize.Y:F0})");
            }
        }
        catch { }
    }

    public static void Prefix()
    {
        ApplyToImGui();
    }
}

[HarmonyPatch(typeof(CharacterAfflictions), "UpdateWeight")]
public class Patch_UpdateWeight
{
    private static CharacterAfflictions cachedLocalAfflictions;
    private static Character cachedLocalCharacter;

    static void Postfix(CharacterAfflictions __instance)
    {
        if (!ConfigManager.NoWeight.Value)
            return;

        var localChar = Character.localCharacter;
        if (ReferenceEquals(localChar, null))
            return;

        if (!ReferenceEquals(localChar, cachedLocalCharacter))
        {
            cachedLocalCharacter = localChar;
            cachedLocalAfflictions = localChar.GetComponent<CharacterAfflictions>();
        }

        if (ReferenceEquals(__instance, cachedLocalAfflictions))
        {
            __instance.SetStatus(CharacterAfflictions.STATUSTYPE.Weight, 0f);
        }
    }
}
