using HarmonyLib;
using ImGuiNET;
using Photon.Pun;
using System;
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

    public static unsafe void Prefix()
    {
        if (fontsLoaded) return;
        fontsLoaded = true;

        try
        {
            var io = ImGui.GetIO();
            var fonts = io.Fonts;

            fonts.AddFontDefault();

            float fontSize = 13.0f;
            ImFontConfigPtr mergeConfig = new ImFontConfigPtr(ImGuiNative.ImFontConfig_ImFontConfig());
            mergeConfig.MergeMode = true;
            mergeConfig.PixelSnapH = true;

            string msyhPath = @"C:\Windows\Fonts\msyh.ttc";
            if (System.IO.File.Exists(msyhPath))
            {
                var f1 = fonts.AddFontFromFileTTF(msyhPath, fontSize, mergeConfig, fonts.GetGlyphRangesChineseFull());
                ConfigManager.Logger.LogInfo($"[PEAK AIO] Chinese font: {(f1.NativePtr != null ? "OK" : "FAILED")}");
                var f2 = fonts.AddFontFromFileTTF(msyhPath, fontSize, mergeConfig, fonts.GetGlyphRangesJapanese());
                ConfigManager.Logger.LogInfo($"[PEAK AIO] Japanese font: {(f2.NativePtr != null ? "OK" : "FAILED")}");
            }
            else
            {
                ConfigManager.Logger.LogWarning("[PEAK AIO] msyh.ttc NOT FOUND");
            }

            string malgunPath = @"C:\Windows\Fonts\malgun.ttf";
            if (System.IO.File.Exists(malgunPath))
            {
                var f3 = fonts.AddFontFromFileTTF(malgunPath, fontSize, mergeConfig, fonts.GetGlyphRangesKorean());
                ConfigManager.Logger.LogInfo($"[PEAK AIO] Korean font: {(f3.NativePtr != null ? "OK" : "FAILED")}");
            }
            else
            {
                ConfigManager.Logger.LogWarning("[PEAK AIO] malgun.ttf NOT FOUND");
            }

            mergeConfig.Destroy();

            bool built = fonts.Build();
            ConfigManager.Logger.LogInfo($"[PEAK AIO] Atlas build: {(built ? "OK" : "FAILED")}, size: {fonts.TexWidth}x{fonts.TexHeight}, fonts: {fonts.Fonts.Size}");
        }
        catch (Exception ex)
        {
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
