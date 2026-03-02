using BepInEx;
using ImGuiNET;
using DearImGuiInjection;
using DearImGuiInjection.BepInEx;
using UnityEngine.Windows;
using UnityEngine;
using System.Xml;
using System.Reflection;
using System;
using BepInEx.Configuration;
using HarmonyLib;
using Photon.Pun;
using System.Collections.Generic;

[BepInDependency(DearImGuiInjection.Metadata.GUID)]
[BepInPlugin("com.onigremlin.peakaio", "PEAK AIO Mod", "1.0.2")]

public class PeakMod : BaseUnityPlugin
{
    // Menu
    private bool styleApplied = false;
    private bool showMenu = false;
    private int selectedTab = 1;
    private static readonly FieldInfo cursorVisibleField = typeof(DearImGuiInjection.DearImGuiInjection)
        .GetField("<IsCursorVisible>k__BackingField", BindingFlags.Static | BindingFlags.NonPublic);

    private void ApplyCustomStyle()
    {
        var style = ImGui.GetStyle();
        var colors = style.Colors;

        var canvasTan = new System.Numerics.Vector4(0.953f, 0.941f, 0.902f, 1.00f);
        var badgeBrown = new System.Numerics.Vector4(0.361f, 0.294f, 0.231f, 1.00f);
        var logInk = new System.Numerics.Vector4(0.18f, 0.18f, 0.18f, 1.00f);
        var sidebarGreen = new System.Numerics.Vector4(0.18f, 0.28f, 0.22f, 1.00f);
        var trailDust = new System.Numerics.Vector4(0.866f, 0.827f, 0.741f, 1.00f);
        var trailDustHover = new System.Numerics.Vector4(0.80f, 0.78f, 0.65f, 1.00f);
        var trailDustActive = new System.Numerics.Vector4(0.75f, 0.72f, 0.61f, 1.00f);
        var ropeBrown = new System.Numerics.Vector4(0.55f, 0.42f, 0.28f, 1.00f);
        var softRed = new System.Numerics.Vector4(0.75f, 0.6f, 0.5f, 1.00f);
        var scoutRed = new System.Numerics.Vector4(0.76f, 0.44f, 0.39f, 1.00f);
        var lightGreen = new System.Numerics.Vector4(0.318f, 0.569f, 0.384f, 1.0f);

        colors[(int)ImGuiCol.WindowBg] = canvasTan;
        colors[(int)ImGuiCol.Border] = logInk;
        colors[(int)ImGuiCol.TitleBg] = lightGreen;
        colors[(int)ImGuiCol.TitleBgActive] = lightGreen;
        colors[(int)ImGuiCol.Text] = logInk;
        colors[(int)ImGuiCol.TextDisabled] = ropeBrown;
        colors[(int)ImGuiCol.CheckMark] = sidebarGreen;
        colors[(int)ImGuiCol.FrameBg] = trailDust;
        colors[(int)ImGuiCol.FrameBgHovered] = trailDustHover;
        colors[(int)ImGuiCol.FrameBgActive] = trailDustActive;
        colors[(int)ImGuiCol.Border] = ropeBrown;
        colors[(int)ImGuiCol.PopupBg] = canvasTan;

        colors[(int)ImGuiCol.TableHeaderBg] = lightGreen;
        colors[(int)ImGuiCol.TableBorderStrong] = ropeBrown;
        colors[(int)ImGuiCol.TableBorderLight] = ropeBrown;

        colors[(int)ImGuiCol.ChildBg] = trailDust;
        colors[(int)ImGuiCol.Button] = trailDust;
        colors[(int)ImGuiCol.ButtonHovered] = trailDustHover;
        colors[(int)ImGuiCol.ButtonActive] = trailDustActive;

        colors[(int)ImGuiCol.Header] = trailDust;
        colors[(int)ImGuiCol.HeaderHovered] = trailDust;
        colors[(int)ImGuiCol.HeaderActive] = trailDust;

        colors[(int)ImGuiCol.Separator] = ropeBrown;

        colors[(int)ImGuiCol.ScrollbarBg] = badgeBrown;
        colors[(int)ImGuiCol.ScrollbarGrab] = sidebarGreen;
        colors[(int)ImGuiCol.ScrollbarGrabHovered] = new System.Numerics.Vector4(
            sidebarGreen.X + 0.1f,
            sidebarGreen.Y + 0.1f,
            sidebarGreen.Z + 0.1f,
            1.0f
        );
        colors[(int)ImGuiCol.ScrollbarGrabActive] = new System.Numerics.Vector4(
            sidebarGreen.X - 0.05f,
            sidebarGreen.Y - 0.05f,
            sidebarGreen.Z - 0.05f,
            1.0f
        );

        colors[(int)ImGuiCol.SliderGrab] = sidebarGreen;
        colors[(int)ImGuiCol.SliderGrabActive] = new System.Numerics.Vector4(
            sidebarGreen.X - 0.05f,
            sidebarGreen.Y - 0.05f,
            sidebarGreen.Z - 0.05f,
            1.0f
        );

        style.WindowRounding = 6f;
        style.FrameRounding = 4f;
        style.ChildRounding = 4f;
        style.FrameBorderSize = 1.0f;
        style.GrabRounding = 4f;
        style.WindowPadding = new System.Numerics.Vector2(4, 4);
        style.CellPadding = new System.Numerics.Vector2(4, 4);
        style.FrameBorderSize = 1.0f;
        style.ItemSpacing = new System.Numerics.Vector2(2, 4);
    }
    private void Awake()
    {
        Logger.LogInfo("Mod Initialized");
        this.gameObject.AddComponent<EventComponent>();
    }

    private void OnEnable()
    {
        Logger.LogInfo("[PEAK AIO] OnEnable called");

        Globals.itemSearchBuffers = new string[3] { "", "", "" };
        ConfigManager.Init(Config, Logger);
        DearImGuiInjection.DearImGuiInjection.Render += MyUI;

        // Initialize Harmony
        var harmony = new Harmony("com.onigremlin.peakaio");
        harmony.PatchAll();
        Logger.LogInfo("Harmony patches applied.");
    }

    private void OnDisable()
    {
        Logger.LogInfo("[PEAK AIO] OnDisable called");
        DearImGuiInjection.DearImGuiInjection.Render -= MyUI;
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(ConfigManager.MenuToggleKey.Value))
        {
            showMenu = !showMenu;
            cursorVisibleField?.SetValue(null, showMenu);
        }
    }

    void DrawCheckbox(ConfigEntry<bool> config, string label, Action<bool> mainThreadAction = null)
    {
        bool value = config.Value;
        if (ImGui.Checkbox(label, ref value))
        {
            config.Value = value;
            Logger.LogInfo($"[Menu] {label} toggled to {(value ? "ON" : "OFF")}");

            if (mainThreadAction != null)
            {
                UnityMainThreadDispatcher.Enqueue(() => mainThreadAction.Invoke(value));
            }
        }
    }

    void DrawSliderFloat(ConfigEntry<float> config, string label, float min, float max, string format = "%.2f")
    {
        float value = config.Value;
        if (ImGui.SliderFloat(label, ref value, min, max, format))
            config.Value = value;
    }

    bool DrawSearchableCombo(string label, ref int selectedIndex, List<string> items, ref string searchBuffer)
    {
        bool changed = false;

        // Draw input field
        string inputId = $"Search##{label}";
        ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X - 4);
        ImGui.InputText("##" + inputId, ref searchBuffer, 100);
        ImGui.PopItemWidth();

        // Draw custom placeholder if input is empty and not active
        if (string.IsNullOrEmpty(searchBuffer) && !ImGui.IsItemActive())
        {
            var pos = ImGui.GetItemRectMin();
            ImGui.SameLine();
            ImGui.SetCursorScreenPos(pos + new System.Numerics.Vector2(4, 2));
            ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(0.18f, 0.18f, 0.18f, 1.00f));
            ImGui.TextUnformatted(Localization.T("items.search"));
            ImGui.PopStyleColor();
        }

        if (ImGui.BeginCombo(label, selectedIndex >= 0 && selectedIndex < items.Count ? items[selectedIndex] : Localization.T("items.none")))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (!string.IsNullOrEmpty(searchBuffer) &&
                    !items[i].ToLower().Contains(searchBuffer.ToLower()))
                    continue;

                bool isSelected = (selectedIndex == i);
                if (ImGui.Selectable($"{items[i]}##{i}", isSelected))
                {
                    selectedIndex = i;
                    changed = true;
                }

                if (isSelected)
                    ImGui.SetItemDefaultFocus();
            }
            ImGui.EndCombo();
        }

        return changed;
    }

    void DrawToolTip(string text)
    {
        ImGui.SameLine();
        ImGui.TextDisabled("(?)");

        if (ImGui.IsItemHovered())
        {
            ImGui.PushStyleColor(ImGuiCol.PopupBg, new System.Numerics.Vector4(0.89f, 0.82f, 0.70f, 1.0f));
            ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(0.18f, 0.18f, 0.18f, 1.00f));
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);

            ImGui.BeginTooltip();
            ImGui.PushTextWrapPos(450.0f);
            ImGui.TextUnformatted(text);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();

            ImGui.PopStyleVar();
            ImGui.PopStyleColor(2);
        }
    }

    private void MyUI()
    {
        try
        {
            if (!showMenu)
                return;

            if (!styleApplied)
            {
                ApplyCustomStyle();
                styleApplied = true;
            }

            // Set window position and size
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(20, 20), ImGuiCond.Once);
            ImGui.SetNextWindowSize(new System.Numerics.Vector2(500, 300), ImGuiCond.Once);

            if (ImGui.Begin("PEAK AIO##Main", ImGuiWindowFlags.NoCollapse))
            {
                // Sidebar
                ImGui.BeginChild("Sidebar", new System.Numerics.Vector2(85, 0), true);
                ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                string[] sidebarKeys = { "tab.player", "tab.items", "tab.lobby", "tab.world", "tab.about", "tab.language" };
                for (int i = 0; i < sidebarKeys.Length; i++)
                {
                    bool isSelected = (selectedTab == i + 1);
                    string label = Localization.T(sidebarKeys[i]);

                    var textColor = isSelected
                        ? new System.Numerics.Vector4(0.318f, 0.569f, 0.384f, 1.0f)
                        : new System.Numerics.Vector4(0.18f, 0.18f, 0.18f, 1.00f);

                    ImGui.PushStyleColor(ImGuiCol.Text, textColor);

                    float textWidth = ImGui.CalcTextSize(label).X;
                    float availableWidth = ImGui.GetContentRegionAvail().X;
                    float offsetX = (availableWidth - textWidth) * 0.5f;
                    ImGui.SetCursorPosX(offsetX);

                    if (ImGui.Selectable(label + "##tab" + i, isSelected))
                        selectedTab = i + 1;

                    ImGui.PopStyleColor();
                }

                ImGui.EndChild();

                // Main content area
                ImGui.SameLine();
                ImGui.BeginChild("MainArea");

                // Player
                if (selectedTab == 1)
                {
                    float fullWidth = ImGui.GetContentRegionAvail().X;
                    float halfWidth = fullWidth / 2f;

                    ImGui.BeginChild("PlayerColumn", new System.Numerics.Vector2(halfWidth, 0), true);
                    ImGui.Indent(4.0f);
                    ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                    if (ImGui.CollapsingHeader(Localization.T("player.selfmods") + "##SelfMods", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        DrawCheckbox(ConfigManager.InfiniteStamina, Localization.T("player.infinite_stamina"), (val) =>
                        {
                            var character = GameHelpers.GetCharacterComponent();
                            var prop = ConstantFields.GetInfiniteStaminaProperty();
                            if (character != null && prop != null)
                                prop.SetValue(character, val);
                        });
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.infinite_stamina"));

                        DrawCheckbox(ConfigManager.LockStatus, Localization.T("player.freeze_afflictions"), (val) =>
                        {
                            var character = GameHelpers.GetCharacterComponent();
                            var prop = ConstantFields.GetStatusLockProperty();
                            if (character != null && prop != null)
                                prop.SetValue(character, val);
                        });
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.freeze_afflictions"));

                        DrawCheckbox(ConfigManager.NoWeight, Localization.T("player.no_weight"));
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.no_weight"));

                        DrawCheckbox(ConfigManager.SpeedMod, Localization.T("player.change_speed"), (val) =>
                        {
                            var movement = GameHelpers.GetMovementComponent();
                            var field = ConstantFields.GetMovementModifierField();
                            if (movement != null && field != null)
                                field.SetValue(movement, ConfigManager.SpeedAmount.Value);
                        });
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.change_speed"));

                        DrawCheckbox(ConfigManager.JumpMod, Localization.T("player.change_jump"), (val) =>
                        {
                            var movement = GameHelpers.GetMovementComponent();
                            var jumpField = ConstantFields.GetJumpGravityField();
                            var fallField = ConstantFields.GetFallDamageTimeField();
                            if (movement != null && jumpField != null)
                                jumpField.SetValue(movement, ConfigManager.JumpAmount.Value);
                            if (movement != null && fallField != null)
                                fallField.SetValue(movement, ConfigManager.NoFallDmg.Value ? 999f : 1.5f);
                        });
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.change_jump"));

                        DrawCheckbox(ConfigManager.ClimbMod, Localization.T("player.change_climb"), (val) =>
                        {
                            var climb = GameHelpers.GetClimbingComponent();
                            var field = ConstantFields.GetClimbSpeedModField();
                            if (climb != null && field != null)
                                field.SetValue(climb, ConfigManager.ClimbAmount.Value);
                        });
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.change_climb"));

                        DrawCheckbox(ConfigManager.VineClimbMod, Localization.T("player.change_vine_climb"), (val) =>
                        {
                            var vine = GameHelpers.GetVineClimbComponent();
                            var field = ConstantFields.GetVineClimbSpeedModField();
                            if (vine != null && field != null)
                                field.SetValue(vine, ConfigManager.VineClimbAmount.Value);
                        });
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.change_vine_climb"));

                        DrawCheckbox(ConfigManager.RopeClimbMod, Localization.T("player.change_rope_climb"), (val) =>
                        {
                            var rope = GameHelpers.GetRopeClimbComponent();
                            var field = ConstantFields.GetRopeClimbSpeedModField();
                            if (rope != null && field != null)
                                field.SetValue(rope, ConfigManager.RopeClimbAmount.Value);
                        });
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.change_rope_climb"));

                        DrawCheckbox(ConfigManager.TeleportToPing, Localization.T("player.teleport_to_ping"));
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.teleport_to_ping"));

                        DrawCheckbox(ConfigManager.FlyMod, Localization.T("player.fly_mode"), FlyPatch.SetFlying);
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.fly_mode"));
                    }
                    ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                    if (ImGui.CollapsingHeader(Localization.T("player.teleport") + "##PlayerTeleport", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.InputFloat("X", ref Globals.teleportX);
                        ImGui.InputFloat("Y", ref Globals.teleportY);
                        ImGui.InputFloat("Z", ref Globals.teleportZ);

                        if (ImGui.Button(Localization.T("player.teleport_to_coords")))
                        {
                            Logger.LogInfo($"[Teleport] Requested to X:{Globals.teleportX} Y:{Globals.teleportY} Z:{Globals.teleportZ}");
                            Utilities.TeleportToCoords(Globals.teleportX, Globals.teleportY, Globals.teleportZ);
                        }
                    }
                    ImGui.EndChild();
                    ImGui.Unindent();
                    ImGui.SameLine();
                    ImGui.BeginChild("PlayerDetailsColumn", new System.Numerics.Vector2(halfWidth - 10, 0), true);
                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                    if (ImGui.CollapsingHeader(Localization.T("player.details"), ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        if (ConfigManager.JumpMod.Value)
                        {
                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawCheckbox(ConfigManager.NoFallDmg, Localization.T("player.no_fall_dmg"));
                            DrawSliderFloat(ConfigManager.JumpAmount, "##jump_amt", 10.0f, 500.0f, Localization.T("player.jump_mult"));
                        }

                        if (ConfigManager.SpeedMod.Value)
                        {
                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawSliderFloat(ConfigManager.SpeedAmount, "##speed_amt", 1.0f, 20.0f, Localization.T("player.move_speed"));
                        }

                        if (ConfigManager.ClimbMod.Value)
                        {
                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawSliderFloat(ConfigManager.ClimbAmount, "##climb_amt", 1.0f, 20.0f, Localization.T("player.climb_speed"));
                        }

                        if (ConfigManager.VineClimbMod.Value)
                        {
                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawSliderFloat(ConfigManager.VineClimbAmount, "##vine_climb_amt", 1.0f, 20.0f, Localization.T("player.vine_speed"));
                        }

                        if (ConfigManager.RopeClimbMod.Value)
                        {
                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawSliderFloat(ConfigManager.RopeClimbAmount, "##rope_climb_amt", 1.0f, 20.0f, Localization.T("player.rope_speed"));
                        }
                        if (ConfigManager.FlyMod.Value)
                        {
                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawSliderFloat(ConfigManager.FlySpeed, "##fly_speed", 10f, 100f, Localization.T("player.fly_speed"));
                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawSliderFloat(ConfigManager.FlyAcceleration, "##fly_acceleration", 10f, 300f, Localization.T("player.fly_acceleration"));
                        }
                    }
                    ImGui.Unindent();
                    ImGui.EndChild();
                }
                // Items
                else if (selectedTab == 2)
                {
                    if (Globals.itemNames.Count == 0)
                    {
                        Utilities.UpdateItems();
                    }

                    List<(int slot, int itemIndex)> assignQueue = new List<(int slot, int itemIndex)>();

                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));

                    ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                    if (ImGui.BeginTable("InventorySlots", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                    {
                        ImGui.TableSetupColumn(Localization.T("items.slot") + " 1");
                        ImGui.TableSetupColumn(Localization.T("items.slot") + " 2");
                        ImGui.TableSetupColumn(Localization.T("items.slot") + " 3");
                        ImGui.TableHeadersRow();

                        ImGui.TableNextRow();

                        for (int slot = 0; slot < 3; slot++)
                        {
                            ImGui.TableSetColumnIndex(slot);
                            ImGui.PushID(slot); // Single PushID per slot

                            string currentItemName = Localization.T("items.none");

                            if (Player.localPlayer?.itemSlots != null &&
                                Player.localPlayer.itemSlots.Length > slot &&
                                Player.localPlayer.itemSlots[slot]?.prefab != null)
                            {
                                currentItemName = Player.localPlayer.itemSlots[slot].prefab.GetName();
                            }

                            ImGui.Text(Localization.T("items.item_n", slot + 1));
                            ImGui.SameLine();
                            ImGui.Text(currentItemName);
                            ImGui.Spacing();

                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);

                            // Detect if the value actually changed
                            int selected = Globals.selectedItems[slot];
                            if (DrawSearchableCombo($"##Combo{slot}", ref selected, Globals.itemNames, ref Globals.itemSearchBuffers[slot]))
                            {
                                Globals.selectedItems[slot] = selected;
                                assignQueue.Add((slot, selected));
                            }

                            ImGui.SameLine();
                            DrawToolTip(Localization.T("tip.item_search"));

                            ImGui.Spacing();

                            ConfigEntry<float> rechargeAmountConfig;
                            switch (slot)
                            {
                                case 0:
                                    rechargeAmountConfig = ConfigManager.RechargeAmountSlot1;
                                    break;
                                case 1:
                                    rechargeAmountConfig = ConfigManager.RechargeAmountSlot2;
                                    break;
                                case 2:
                                    rechargeAmountConfig = ConfigManager.RechargeAmountSlot3;
                                    break;
                                default:
                                    rechargeAmountConfig = ConfigManager.RechargeAmountSlot1;
                                    break;
                            }

                            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                            DrawSliderFloat(rechargeAmountConfig, $"##recharge_mount##{slot}", 0f, 100f, Localization.T("items.charge_format"));

                            if (ImGui.Button(Localization.T("items.recharge") + $"##{slot}"))
                            {
                                Utilities.RechargeInventorySlot(slot, rechargeAmountConfig.Value);
                            }
                            ImGui.SameLine();
                            DrawToolTip(Localization.T("tip.recharge"));

                            ImGui.PopID(); // Pop slot ID
                        }

                        ImGui.EndTable();
                    }

                    foreach (var (slot, itemIndex) in assignQueue)
                    {
                        Utilities.AssignInventoryItem(slot, itemIndex);
                    }

                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                    if (ImGui.Button(Localization.T("items.refresh")))
                        Utilities.UpdateItems();
                    ImGui.SameLine();
                    DrawToolTip(Localization.T("tip.refresh_items"));

                    ImGui.Unindent();
                }
                // Lobby
                else if (selectedTab == 3)
                {
                    float fullWidth = ImGui.GetContentRegionAvail().X;
                    float halfWidth = fullWidth / 2f;

                    if (Globals.allPlayers.Count == 0)
                    {
                        Utilities.RefreshPlayerList();
                    }

                    // Left: Player List
                    ImGui.BeginChild("Lobby_PlayerList", new System.Numerics.Vector2(halfWidth, 0), true);
                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                    if (ImGui.CollapsingHeader(Localization.T("lobby.players"), ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);
                        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);

                        if (ImGui.BeginCombo(Localization.T("lobby.select_player"), Globals.selectedPlayer >= 0 && Globals.selectedPlayer < Globals.playerNames.Count
                            ? Globals.playerNames[Globals.selectedPlayer]
                            : Localization.T("items.none")))
                        {
                            for (int i = 0; i < Globals.playerNames.Count; i++)
                            {
                                bool isSelected = (Globals.selectedPlayer == i);
                                if (ImGui.Selectable($"{Globals.playerNames[i]}##{i}", isSelected))
                                {
                                    Globals.selectedPlayer = i;
                                }

                                if (isSelected)
                                    ImGui.SetItemDefaultFocus();
                            }
                            ImGui.EndCombo();
                        }
                        ImGui.Dummy(new System.Numerics.Vector2(4, 4));
                        ImGui.Separator();
                        ImGui.Text(Localization.T("lobby.all_players"));

                        if (ImGui.Button(Localization.T("lobby.revive_all")))
                            Utilities.ReviveAllPlayers();

                        ImGui.SameLine();
                        if (ImGui.Button(Localization.T("lobby.kill_all")))
                        {
                            Utilities.KillAllPlayers();
                        }

                        bool excludeSelf = Globals.excludeSelfFromAllActions;
                        if (ImGui.Checkbox(Localization.T("lobby.exclude_self") + "##KillAll", ref excludeSelf))
                            Globals.excludeSelfFromAllActions = excludeSelf;

                        if (ImGui.Button(Localization.T("lobby.warp_all_to_me")))
                            Utilities.WarpAllPlayersToMe();
                    }

                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                    if (ImGui.Button(Localization.T("lobby.refresh_players")))
                        Utilities.RefreshPlayerList();
                    ImGui.SameLine();
                    DrawToolTip(Localization.T("tip.refresh_players"));

                    ImGui.Unindent();
                    ImGui.EndChild();

                    // Right: Player Actions
                    ImGui.SameLine();
                    ImGui.BeginChild("Lobby_PlayerActions", new System.Numerics.Vector2(halfWidth - 10, 0), true);
                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(0, 4));
                    if (ImGui.CollapsingHeader(Localization.T("lobby.actions"), ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        if (Globals.selectedPlayer >= 0 && Globals.selectedPlayer < Globals.allPlayers.Count)
                        {
                            if (ImGui.Button(Localization.T("lobby.revive")))
                                Utilities.ReviveSelectedPlayer();

                            ImGui.SameLine();
                            if (ImGui.Button(Localization.T("lobby.kill")))
                                Utilities.KillSelectedPlayer();

                            if (ImGui.Button(Localization.T("lobby.warp_to")))
                                Utilities.WarpToSelectedPlayer();

                            ImGui.SameLine();
                            if (ImGui.Button(Localization.T("lobby.warp_to_me")))
                                Utilities.WarpSelectedPlayerToMe();

                            ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                            ImGui.Separator();
                            ImGui.Text(Localization.T("lobby.special_actions"));

                            if (ImGui.Button(Localization.T("lobby.spawn_scoutmaster")))
                            {
                                Utilities.SpawnScoutmasterForPlayer(Globals.selectedPlayer);
                            }
                            ImGui.SameLine();
                            DrawToolTip(Localization.T("tip.spawn_scoutmaster"));
                        }
                        else
                        {
                            ImGui.Text(Localization.T("lobby.no_player_selected"));
                        }
                    }

                    ImGui.Unindent();
                    ImGui.EndChild();
                }
                // World
                else if (selectedTab == 4)
                {
                    float fullWidth = ImGui.GetContentRegionAvail().X;
                    float halfWidth = fullWidth / 2f;

                    Utilities.EnsureLuggageListInitialized();

                    // Left: Luggage List
                    ImGui.BeginChild("World_LuggageList", new System.Numerics.Vector2(halfWidth, 0), true);
                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));

                    if (ImGui.CollapsingHeader(Localization.T("world.containers"), ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 4);

                        // Always show the combo, even if the list is empty
                        string selectedLabel = Globals.selectedLuggageIndex >= 0 && Globals.selectedLuggageIndex < Globals.luggageLabels.Count
                            ? Globals.luggageLabels[Globals.selectedLuggageIndex]
                            : Localization.T("items.none");

                        if (ImGui.BeginCombo(Localization.T("world.select_container"), selectedLabel))
                        {
                            if (Globals.luggageLabels.Count > 0)
                            {
                                for (int i = 0; i < Globals.luggageLabels.Count; i++)
                                {
                                    bool isSelected = (Globals.selectedLuggageIndex == i);
                                    if (ImGui.Selectable($"{Globals.luggageLabels[i]}##{i}", isSelected))
                                    {
                                        Globals.selectedLuggageIndex = i;
                                    }

                                    if (isSelected)
                                        ImGui.SetItemDefaultFocus();
                                }
                            }
                            else
                            {
                                ImGui.TextDisabled(Localization.T("world.no_containers"));
                            }

                            ImGui.EndCombo();
                        }

                        ImGui.Dummy(new System.Numerics.Vector2(4, 2));
                        if (ImGui.Button(Localization.T("world.refresh_luggage")))
                        {
                            Utilities.hasInitializedLuggageList = false;
                            Utilities.RefreshLuggageList();
                        }
                        ImGui.SameLine();
                        DrawToolTip(Localization.T("tip.refresh_luggage"));

                        ImGui.Dummy(new System.Numerics.Vector2(4, 4));
                        ImGui.Separator();
                        ImGui.Text(Localization.T("world.all_nearby"));

                        if (ImGui.Button(Localization.T("world.open_all_nearby")))
                        {
                            Utilities.OpenAllNearbyLuggage();
                        }
                    }

                    ImGui.Unindent();
                    ImGui.EndChild();

                    // Right: Luggage Actions
                    ImGui.SameLine();
                    ImGui.BeginChild("World_LuggageActions", new System.Numerics.Vector2(halfWidth - 10, 0), true);
                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(0, 4));

                    if (ImGui.CollapsingHeader(Localization.T("lobby.actions") + "##WorldActions", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        if (Globals.selectedLuggageIndex >= 0 && Globals.selectedLuggageIndex < Globals.luggageLabels.Count)
                        {
                            string label = Globals.luggageLabels[Globals.selectedLuggageIndex];

                            if (ImGui.Button(Localization.T("world.warp_to_luggage")))
                            {
                                Logger.LogInfo($"[UI] Warp requested for index {Globals.selectedLuggageIndex} - {label}");
                                Vector3 luggageCoords = Globals.luggageObject[Globals.selectedLuggageIndex].Center();
                                luggageCoords.y += 1.5f;

                                Utilities.TeleportToCoords(luggageCoords.x, luggageCoords.y, luggageCoords.z);
                            }

                            if (ImGui.Button(Localization.T("world.open_luggage")))
                            {
                                Utilities.OpenLuggage(Globals.selectedLuggageIndex);
                            }
                        }
                        else
                        {
                            ImGui.Text(Localization.T("world.no_luggage_selected"));
                        }
                    }

                    ImGui.Unindent();
                    ImGui.EndChild();
                }
                // About
                else if (selectedTab == 5)
                {
                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));

                    ImGui.Text(Localization.T("about.title"));
                    ImGui.Separator();
                    ImGui.Text(Localization.T("about.version"));
                    ImGui.Text(Localization.T("about.author"));

                    ImGui.Spacing();
                    ImGui.TextWrapped(Localization.T("about.description"));

                    ImGui.Spacing();
                    ImGui.Text(Localization.T("about.key_features"));
                    ImGui.BulletText(Localization.T("about.feature1"));
                    ImGui.BulletText(Localization.T("about.feature2"));
                    ImGui.BulletText(Localization.T("about.feature3"));
                    ImGui.BulletText(Localization.T("about.feature4"));
                    ImGui.BulletText(Localization.T("about.feature5"));
                    ImGui.BulletText(Localization.T("about.feature6"));

                    ImGui.Spacing();
                    ImGui.Text(Localization.T("about.thanks"));
                    ImGui.BulletText(Localization.T("about.thanks1"));
                    ImGui.BulletText(Localization.T("about.thanks2"));
                    ImGui.BulletText(Localization.T("about.thanks3"));
                    ImGui.BulletText(Localization.T("about.thanks4"));

                    ImGui.Spacing();
                    ImGui.Separator();
                    ImGui.TextWrapped(Localization.T("about.disclaimer"));

                    ImGui.Unindent();
                }
                // Language
                else if (selectedTab == 6)
                {
                    ImGui.Indent(4.0f);
                    ImGui.Dummy(new System.Numerics.Vector2(4, 2));

                    ImGui.Text(Localization.T("lang.title"));
                    ImGui.Separator();
                    ImGui.Spacing();

                    ImGui.Text(Localization.T("lang.current"));
                    ImGui.Spacing();
                    ImGui.Text(Localization.T("lang.select"));
                    ImGui.Spacing();

                    for (int i = 0; i < Localization.LanguageNames.Length; i++)
                    {
                        bool isActive = ((int)Localization.CurrentLanguage == i);

                        if (isActive)
                        {
                            ImGui.PushStyleColor(ImGuiCol.Button, new System.Numerics.Vector4(0.318f, 0.569f, 0.384f, 1.0f));
                            ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(0.953f, 0.941f, 0.902f, 1.00f));
                        }

                        if (ImGui.Button(Localization.LanguageNames[i] + "##lang" + i, new System.Numerics.Vector2(ImGui.GetContentRegionAvail().X - 4, 28)))
                        {
                            Localization.SetLanguage(i);
                            ConfigManager.LanguageIndex.Value = i;
                        }

                        if (isActive)
                            ImGui.PopStyleColor(2);
                    }

                    ImGui.Unindent();
                }

                ImGui.EndChild();
            }

            ImGui.End();
        }
        catch (Exception ex)
        {
            ConfigManager.Logger.LogError("[UI ERROR] Exception in MyUI: " + ex);
        }
    }
}