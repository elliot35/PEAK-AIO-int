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

    public static unsafe void Prefix()
    {
        if (fontsLoaded) return;
        fontsLoaded = true;

        try
        {
            var io = ImGui.GetIO();
            var fonts = io.Fonts;

            // Clear any pre-existing fonts added by DearImGuiInjection during InitImGui().
            // At this point no GPU texture exists yet, so this is safe.
            fonts.Clear();

            rangesHandle = GCHandle.Alloc(CombinedRanges, GCHandleType.Pinned);
            IntPtr rangesPtr = rangesHandle.AddrOfPinnedObject();

            string msyhPath = @"C:\Windows\Fonts\msyh.ttc";
            if (System.IO.File.Exists(msyhPath))
            {
                var cjkFont = fonts.AddFontFromFileTTF(msyhPath, 14.0f, default, rangesPtr);
                ConfigManager.Logger.LogInfo($"[PEAK AIO] CJK primary font (msyh): {(cjkFont.NativePtr != null ? "OK" : "FAILED")}");
            }
            else
            {
                fonts.AddFontDefault();
                ConfigManager.Logger.LogWarning("[PEAK AIO] msyh.ttc not found, using default font.");
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
