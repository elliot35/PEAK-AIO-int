using System.Collections.Generic;

public enum Language
{
    English,
    SimplifiedChinese,
    Japanese,
    Korean,
    Italian
}

public static class Localization
{
    public static Language CurrentLanguage = Language.English;

    private static readonly Dictionary<string, Dictionary<Language, string>> Strings = new Dictionary<string, Dictionary<Language, string>>
    {
        // Sidebar
        { "tab.player", new Dictionary<Language, string> {
            { Language.English, "PLAYER" },
            { Language.SimplifiedChinese, "玩家" },
            { Language.Japanese, "プレイヤー" },
            { Language.Korean, "플레이어" },
            {  Language.Italian, "GIOCATORE" }
        }},
        { "tab.items", new Dictionary<Language, string> {
            { Language.English, "ITEMS" },
            { Language.SimplifiedChinese, "物品" },
            { Language.Japanese, "アイテム" },
            { Language.Korean, "아이템" },
            {  Language.Italian, "OGGETTI" }
        }},
        { "tab.lobby", new Dictionary<Language, string> {
            { Language.English, "LOBBY" },
            { Language.SimplifiedChinese, "大厅" },
            { Language.Japanese, "ロビー" },
            { Language.Korean, "로비" },
            { Language.Italian, "LOBBY" }
        }},
        { "tab.world", new Dictionary<Language, string> {
            { Language.English, "WORLD" },
            { Language.SimplifiedChinese, "世界" },
            { Language.Japanese, "ワールド" },
            { Language.Korean, "월드" },
            { Language.Italian, "MONDO" }
        }},
        { "tab.about", new Dictionary<Language, string> {
            { Language.English, "ABOUT" },
            { Language.SimplifiedChinese, "关于" },
            { Language.Japanese, "情報" },
            { Language.Korean, "정보" },
            { Language.Italian, "INFO" }
        }},
        { "tab.language", new Dictionary<Language, string> {
            { Language.English, "LANG" },
            { Language.SimplifiedChinese, "语言" },
            { Language.Japanese, "言語" },
            { Language.Korean, "언어" },
            { Language.Italian, "LINGUA" }
        }},

        // Player tab - Self Mods
        { "player.selfmods", new Dictionary<Language, string> {
            { Language.English, "Self Mods" },
            { Language.SimplifiedChinese, "自身模组" },
            { Language.Japanese, "セルフMOD" },
            { Language.Korean, "셀프 모드" },
            { Language.Italian, "Mod Personali" }
        }},
        { "player.infinite_stamina", new Dictionary<Language, string> {
            { Language.English, "Infinite Stamina" },
            { Language.SimplifiedChinese, "无限体力" },
            { Language.Japanese, "無限スタミナ" },
            { Language.Korean, "무한 스태미나" },
            { Language.Italian, "Stamina Infinita" }
        }},
        { "player.freeze_afflictions", new Dictionary<Language, string> {
            { Language.English, "Freeze Afflictions" },
            { Language.SimplifiedChinese, "冻结状态异常" },
            { Language.Japanese, "状態異常凍結" },
            { Language.Korean, "상태이상 동결" },
            { Language.Italian, "Blocca Afflizioni" }
        }},
        { "player.no_weight", new Dictionary<Language, string> {
            { Language.English, "No Weight" },
            { Language.SimplifiedChinese, "无重量" },
            { Language.Japanese, "重量無効" },
            { Language.Korean, "무게 무시" },
            { Language.Italian, "Nessun Peso" }
        }},
        { "player.change_speed", new Dictionary<Language, string> {
            { Language.English, "Change Speed" },
            { Language.SimplifiedChinese, "修改速度" },
            { Language.Japanese, "速度変更" },
            { Language.Korean, "속도 변경" },
            { Language.Italian, "Modifica Velocità" }
        }},
        { "player.change_jump", new Dictionary<Language, string> {
            { Language.English, "Change Jump" },
            { Language.SimplifiedChinese, "修改跳跃" },
            { Language.Japanese, "ジャンプ変更" },
            { Language.Korean, "점프 변경" },
            { Language.Italian, "Modifica Salto" }
        }},
        { "player.change_climb", new Dictionary<Language, string> {
            { Language.English, "Change Climb" },
            { Language.SimplifiedChinese, "修改攀爬" },
            { Language.Japanese, "登攀速度変更" },
            { Language.Korean, "등반 변경" },
            { Language.Italian, "Modifica Arrampicata" }
        }},
        { "player.change_vine_climb", new Dictionary<Language, string> {
            { Language.English, "Change Vine Climb" },
            { Language.SimplifiedChinese, "修改藤蔓攀爬" },
            { Language.Japanese, "ツル登攀変更" },
            { Language.Korean, "덩굴 등반 변경" },
            { Language.Italian, "Modifica Arrampicata su Liane" }
        }},
        { "player.change_rope_climb", new Dictionary<Language, string> {
            { Language.English, "Change Rope Climb" },
            { Language.SimplifiedChinese, "修改绳索攀爬" },
            { Language.Japanese, "ロープ登攀変更" },
            { Language.Korean, "로프 등반 변경" },
            { Language.Italian, "Modifica Arrampicata su Corda" }
        }},
        { "player.teleport_to_ping", new Dictionary<Language, string> {
            { Language.English, "Teleport to Ping" },
            { Language.SimplifiedChinese, "传送到标记点" },
            { Language.Japanese, "ピンに移動" },
            { Language.Korean, "핑으로 텔레포트" },
            { Language.Italian, "Teletrasporto al Ping" }
        }},
        { "player.fly_mode", new Dictionary<Language, string> {
            { Language.English, "Fly Mode" },
            { Language.SimplifiedChinese, "飞行模式" },
            { Language.Japanese, "飛行モード" },
            { Language.Korean, "비행 모드" },
            { Language.Italian, "Modalità Volo" }
        }},
        { "player.no_fall_dmg", new Dictionary<Language, string> {
            { Language.English, "No Fall Dmg" },
            { Language.SimplifiedChinese, "无坠落伤害" },
            { Language.Japanese, "落下ダメージ無効" },
            { Language.Korean, "낙하 피해 없음" },
            { Language.Italian, "Nessun Danno da Caduta" }
        }},

        // Player tab - Teleport
        { "player.teleport", new Dictionary<Language, string> {
            { Language.English, "Teleport" },
            { Language.SimplifiedChinese, "传送" },
            { Language.Japanese, "テレポート" },
            { Language.Korean, "텔레포트" },
            { Language.Italian, "Teletrasporto" }
        }},
        { "player.teleport_to_coords", new Dictionary<Language, string> {
            { Language.English, "Teleport to coords" },
            { Language.SimplifiedChinese, "传送到坐标" },
            { Language.Japanese, "座標にテレポート" },
            { Language.Korean, "좌표로 텔레포트" },
            { Language.Italian, "Teletrasporto alle coordinate" }
        }},

        // Player tab - Details
        { "player.details", new Dictionary<Language, string> {
            { Language.English, "Details" },
            { Language.SimplifiedChinese, "详细设置" },
            { Language.Japanese, "詳細設定" },
            { Language.Korean, "상세 설정" },
            { Language.Italian, "Dettagli" }
        }},
        { "player.jump_mult", new Dictionary<Language, string> {
            { Language.English, "Jump Mult: %.2f" },
            { Language.SimplifiedChinese, "跳跃倍率: %.2f" },
            { Language.Japanese, "ジャンプ倍率: %.2f" },
            { Language.Korean, "점프 배율: %.2f" },
            { Language.Italian, "Moltiplicatore Salto: %.2f" }
        }},
        { "player.move_speed", new Dictionary<Language, string> {
            { Language.English, "Move Speed: %.2f" },
            { Language.SimplifiedChinese, "移动速度: %.2f" },
            { Language.Japanese, "移動速度: %.2f" },
            { Language.Korean, "이동 속도: %.2f" },
            { Language.Italian, "Velocità Movimento: %.2f" }
        }},
        { "player.climb_speed", new Dictionary<Language, string> {
            { Language.English, "Climb Speed: %.2f" },
            { Language.SimplifiedChinese, "攀爬速度: %.2f" },
            { Language.Japanese, "登攀速度: %.2f" },
            { Language.Korean, "등반 속도: %.2f" },
            { Language.Italian, "Velocità Arrampicata: %.2f" }
        }},
        { "player.vine_speed", new Dictionary<Language, string> {
            { Language.English, "Vine Speed: %.2f" },
            { Language.SimplifiedChinese, "藤蔓速度: %.2f" },
            { Language.Japanese, "ツル速度: %.2f" },
            { Language.Korean, "덩굴 속도: %.2f" },
            { Language.Italian, "Velocità Liane: %.2f" }
        }},
        { "player.rope_speed", new Dictionary<Language, string> {
            { Language.English, "Rope Speed: %.2f" },
            { Language.SimplifiedChinese, "绳索速度: %.2f" },
            { Language.Japanese, "ロープ速度: %.2f" },
            { Language.Korean, "로프 속도: %.2f" },
            { Language.Italian, "Velocità Corda: %.2f" }
        }},
        { "player.fly_speed", new Dictionary<Language, string> {
            { Language.English, "Fly Speed: %.2f" },
            { Language.SimplifiedChinese, "飞行速度: %.2f" },
            { Language.Japanese, "飛行速度: %.2f" },
            { Language.Korean, "비행 속도: %.2f" },
            { Language.Italian, "Velocità Volo: %.2f" }
        }},
        { "player.fly_acceleration", new Dictionary<Language, string> {
            { Language.English, "Fly Acceleration: %.2f" },
            { Language.SimplifiedChinese, "飞行加速度: %.2f" },
            { Language.Japanese, "飛行加速度: %.2f" },
            { Language.Korean, "비행 가속도: %.2f" },
            { Language.Italian, "Accelerazione Volo: %.2f" }
        }},

        // Tooltips - Player
        { "tip.infinite_stamina", new Dictionary<Language, string> {
            { Language.English, "Prevents stamina from decreasing, allowing unlimited sprinting and actions." },
            { Language.SimplifiedChinese, "防止体力下降，允许无限冲刺和执行动作。" },
            { Language.Japanese, "スタミナの減少を防ぎ、無制限のダッシュとアクションを可能にします。" },
            { Language.Korean, "스태미나 감소를 방지하여 무제한 달리기와 행동이 가능합니다." },
            { Language.Italian, "Impedisce alla stamina di diminuire, permettendo scatti e azioni illimitate." }
        }},
        { "tip.freeze_afflictions", new Dictionary<Language, string> {
            { Language.English, "Prevents your statuses from changing." },
            { Language.SimplifiedChinese, "防止你的状态发生变化。" },
            { Language.Japanese, "ステータスの変化を防ぎます。" },
            { Language.Korean, "상태 변화를 방지합니다." },
            { Language.Italian, "Impedisce ai tuoi stati di cambiare." }
        }},
        { "tip.no_weight", new Dictionary<Language, string> {
            { Language.English, "Disables weight penalties from carried items and backpack." },
            { Language.SimplifiedChinese, "禁用携带物品和背包的重量惩罚。" },
            { Language.Japanese, "所持アイテムやバックパックの重量ペナルティを無効にします。" },
            { Language.Korean, "소지품 및 배낭의 무게 패널티를 비활성화합니다." },
            { Language.Italian, "Disabilita le penalità di peso dagli oggetti trasportati e dallo zaino." }
        }},
        { "tip.change_speed", new Dictionary<Language, string> {
            { Language.English, "Overrides your character's movement speed with a custom multiplier." },
            { Language.SimplifiedChinese, "使用自定义倍率覆盖角色的移动速度。" },
            { Language.Japanese, "キャラクターの移動速度をカスタム倍率で上書きします。" },
            { Language.Korean, "캐릭터의 이동 속도를 사용자 정의 배율로 변경합니다." },
            { Language.Italian, "Sostituisce la velocità di movimento del personaggio con un moltiplicatore personalizzato." }
        }},
        { "tip.change_jump", new Dictionary<Language, string> {
            { Language.English, "Modifies jump height, allowing higher or lower jumps depending on your settings." },
            { Language.SimplifiedChinese, "修改跳跃高度，根据设置允许更高或更低的跳跃。" },
            { Language.Japanese, "ジャンプの高さを変更し、設定に応じて高くまたは低くジャンプできます。" },
            { Language.Korean, "점프 높이를 수정하여 설정에 따라 더 높거나 낮게 점프할 수 있습니다." },
            { Language.Italian, "Modifica l'altezza del salto, permettendo salti più alti o più bassi in base alle impostazioni." }
        }},
        { "tip.change_climb", new Dictionary<Language, string> {
            { Language.English, "Adjusts the speed at which you climb ladders and surfaces." },
            { Language.SimplifiedChinese, "调整攀爬梯子和表面的速度。" },
            { Language.Japanese, "はしごや壁を登る速度を調整します。" },
            { Language.Korean, "사다리와 표면을 오르는 속도를 조정합니다." },
            { Language.Italian, "Regola la velocità con cui ti arrampichi su scale e superfici." }
        }},
        { "tip.change_vine_climb", new Dictionary<Language, string> {
            { Language.English, "Changes climbing speed specifically for vines." },
            { Language.SimplifiedChinese, "专门修改藤蔓的攀爬速度。" },
            { Language.Japanese, "ツルの登攀速度を変更します。" },
            { Language.Korean, "덩굴 등반 속도를 변경합니다." },
            { Language.Italian, "Modifica la velocità di arrampicata per le liane." }
        }},
        { "tip.change_rope_climb", new Dictionary<Language, string> {
            { Language.English, "Modifies climbing speed when using ropes or rope-based obstacles." },
            { Language.SimplifiedChinese, "修改使用绳索或绳索障碍物时的攀爬速度。" },
            { Language.Japanese, "ロープやロープ系障害物での登攀速度を変更します。" },
            { Language.Korean, "로프 또는 로프 장애물 사용 시 등반 속도를 수정합니다." },
            { Language.Italian, "Modifica la velocità di arrampicata quando si usano corde o ostacoli basati su corde." }
        }},
        { "tip.teleport_to_ping", new Dictionary<Language, string> {
            { Language.English, "Teleports your character to the pinged location on the map." },
            { Language.SimplifiedChinese, "将角色传送到地图上的标记位置。" },
            { Language.Japanese, "マップ上のピンの位置にキャラクターをテレポートします。" },
            { Language.Korean, "캐릭터를 맵에서 핑한 위치로 텔레포트합니다." },
            { Language.Italian, "Teletrasporta il tuo personaggio alla posizione del ping sulla mappa." }
        }},
        { "tip.fly_mode", new Dictionary<Language, string> {
            { Language.English, "Allows free movement in all directions while ignoring gravity." },
            { Language.SimplifiedChinese, "允许忽略重力在所有方向自由移动。" },
            { Language.Japanese, "重力を無視して全方向に自由移動できます。" },
            { Language.Korean, "중력을 무시하고 모든 방향으로 자유 이동이 가능합니다." },
            { Language.Italian, "Permette il movimento libero in tutte le direzioni ignorando la gravità." }
        }},

        // Items tab
        { "items.slot", new Dictionary<Language, string> {
            { Language.English, "Slot" },
            { Language.SimplifiedChinese, "槽位" },
            { Language.Japanese, "スロット" },
            { Language.Korean, "슬롯" },
            { Language.Italian, "Slot" }
        }},
        { "items.item_n", new Dictionary<Language, string> {
            { Language.English, "Item {0}:" },
            { Language.SimplifiedChinese, "物品 {0}:" },
            { Language.Japanese, "アイテム {0}:" },
            { Language.Korean, "아이템 {0}:" },
            { Language.Italian, "Oggetto {0}:" }
        }},
        { "items.none", new Dictionary<Language, string> {
            { Language.English, "None" },
            { Language.SimplifiedChinese, "无" },
            { Language.Japanese, "なし" },
            { Language.Korean, "없음" },
            { Language.Italian, "Nessuno" }
        }},
        { "items.search", new Dictionary<Language, string> {
            { Language.English, "Search items..." },
            { Language.SimplifiedChinese, "搜索物品..." },
            { Language.Japanese, "アイテム検索..." },
            { Language.Korean, "아이템 검색..." },
            { Language.Italian, "Cerca oggetti..." }
        }},
        { "items.charge_format", new Dictionary<Language, string> {
            { Language.English, "Charge: %.1f" },
            { Language.SimplifiedChinese, "充能: %.1f" },
            { Language.Japanese, "チャージ: %.1f" },
            { Language.Korean, "충전: %.1f" },
            { Language.Italian, "Carica: %.1f" }
        }},
        { "items.recharge", new Dictionary<Language, string> {
            { Language.English, "Recharge" },
            { Language.SimplifiedChinese, "充能" },
            { Language.Japanese, "チャージ" },
            { Language.Korean, "충전" },
            { Language.Italian, "Ricarica" }
        }},
        { "items.refresh", new Dictionary<Language, string> {
            { Language.English, "Refresh Item List" },
            { Language.SimplifiedChinese, "刷新物品列表" },
            { Language.Japanese, "アイテムリスト更新" },
            { Language.Korean, "아이템 목록 새로고침" },
            { Language.Italian, "Aggiorna Lista Oggetti" }
        }},
        { "tip.item_search", new Dictionary<Language, string> {
            { Language.English, "Search and assign any available item to this slot." },
            { Language.SimplifiedChinese, "搜索并分配任何可用物品到此槽位。" },
            { Language.Japanese, "利用可能なアイテムを検索してこのスロットに割り当てます。" },
            { Language.Korean, "사용 가능한 아이템을 검색하여 이 슬롯에 할당합니다." },
            { Language.Italian, "Cerca e assegna qualsiasi oggetto disponibile a questo slot." }
        }},
        { "tip.recharge", new Dictionary<Language, string> {
            { Language.English, "Set how much to recharge the item's charges when clicking 'Recharge'." },
            { Language.SimplifiedChinese, "设置点击「充能」时为物品充能的数量。" },
            { Language.Japanese, "「チャージ」クリック時のチャージ量を設定します。" },
            { Language.Korean, "'충전' 클릭 시 아이템 충전량을 설정합니다." },
            { Language.Italian, "Imposta quanta carica aggiungere all'oggetto quando clicchi 'Ricarica'." }
        }},
        { "tip.refresh_items", new Dictionary<Language, string> {
            { Language.English, "Reloads the list of available items in case something was missed or updated." },
            { Language.SimplifiedChinese, "重新加载可用物品列表，以防遗漏或更新。" },
            { Language.Japanese, "見落としや更新に備え、利用可能なアイテムリストを再読み込みします。" },
            { Language.Korean, "누락되거나 업데이트된 경우 사용 가능한 아이템 목록을 다시 불러옵니다." },
            { Language.Italian, "Ricarica l'elenco degli oggetti disponibili nel caso qualcosa sia stato perso o aggiornato." }
        }},

        // Lobby tab
        { "lobby.players", new Dictionary<Language, string> {
            { Language.English, "Lobby Players" },
            { Language.SimplifiedChinese, "大厅玩家" },
            { Language.Japanese, "ロビープレイヤー" },
            { Language.Korean, "로비 플레이어" },
            { Language.Italian, "Giocatori nella Lobby" }
        }},
        { "lobby.select_player", new Dictionary<Language, string> {
            { Language.English, "Select Player" },
            { Language.SimplifiedChinese, "选择玩家" },
            { Language.Japanese, "プレイヤー選択" },
            { Language.Korean, "플레이어 선택" },
            { Language.Italian, "Seleziona Giocatore" }
        }},
        { "lobby.all_players", new Dictionary<Language, string> {
            { Language.English, "All Players" },
            { Language.SimplifiedChinese, "所有玩家" },
            { Language.Japanese, "全プレイヤー" },
            { Language.Korean, "모든 플레이어" },
            { Language.Italian, "Tutti i Giocatori" }
        }},
        { "lobby.revive_all", new Dictionary<Language, string> {
            { Language.English, "Revive All" },
            { Language.SimplifiedChinese, "复活全部" },
            { Language.Japanese, "全員復活" },
            { Language.Korean, "전원 부활" },
            { Language.Italian, "Rianima Tutti" }
        }},
        { "lobby.kill_all", new Dictionary<Language, string> {
            { Language.English, "Kill All" },
            { Language.SimplifiedChinese, "击杀全部" },
            { Language.Japanese, "全員キル" },
            { Language.Korean, "전원 처치" },
            { Language.Italian, "Uccidi Tutti" }
        }},
        { "lobby.exclude_self", new Dictionary<Language, string> {
            { Language.English, "Exclude Self from Kill All" },
            { Language.SimplifiedChinese, "击杀全部时排除自己" },
            { Language.Japanese, "全員キルから自分を除外" },
            { Language.Korean, "전원 처치에서 자신 제외" },
            { Language.Italian, "Escludi Te Stesso da Uccidi Tutti" }
        }},
        { "lobby.warp_all_to_me", new Dictionary<Language, string> {
            { Language.English, "Warp All To Me" },
            { Language.SimplifiedChinese, "将所有人传送到我身边" },
            { Language.Japanese, "全員を自分の元へ" },
            { Language.Korean, "전원 내게 워프" },
            { Language.Italian, "Teletrasporta Tutti da Me" }
        }},
        { "lobby.refresh_players", new Dictionary<Language, string> {
            { Language.English, "Refresh Players List" },
            { Language.SimplifiedChinese, "刷新玩家列表" },
            { Language.Japanese, "プレイヤーリスト更新" },
            { Language.Korean, "플레이어 목록 새로고침" },
            { Language.Italian, "Aggiorna Lista Giocatori" }
        }},
        { "tip.refresh_players", new Dictionary<Language, string> {
            { Language.English, "Manually reloads the list of players in case it didn't update automatically." },
            { Language.SimplifiedChinese, "手动重新加载玩家列表，以防未自动更新。" },
            { Language.Japanese, "自動更新されなかった場合に手動でプレイヤーリストを再読み込みします。" },
            { Language.Korean, "자동 업데이트되지 않은 경우 수동으로 플레이어 목록을 다시 불러옵니다." },
            { Language.Italian, "Ricarica manualmente l'elenco dei giocatori nel caso non si sia aggiornato automaticamente." }
        }},
        { "lobby.actions", new Dictionary<Language, string> {
            { Language.English, "Actions" },
            { Language.SimplifiedChinese, "操作" },
            { Language.Japanese, "アクション" },
            { Language.Korean, "행동" },
            { Language.Italian, "Azioni" }
        }},
        { "lobby.revive", new Dictionary<Language, string> {
            { Language.English, "Revive" },
            { Language.SimplifiedChinese, "复活" },
            { Language.Japanese, "復活" },
            { Language.Korean, "부활" },
            { Language.Italian, "Rianima" }
        }},
        { "lobby.kill", new Dictionary<Language, string> {
            { Language.English, "Kill" },
            { Language.SimplifiedChinese, "击杀" },
            { Language.Japanese, "キル" },
            { Language.Korean, "처치" },
            { Language.Italian, "Uccidi" }
        }},
        { "lobby.warp_to", new Dictionary<Language, string> {
            { Language.English, "Warp To" },
            { Language.SimplifiedChinese, "传送至" },
            { Language.Japanese, "ワープ" },
            { Language.Korean, "워프" },
            { Language.Italian, "Teletrasporta a" }
        }},
        { "lobby.warp_to_me", new Dictionary<Language, string> {
            { Language.English, "Warp To Me" },
            { Language.SimplifiedChinese, "传送到我身边" },
            { Language.Japanese, "自分の元へ" },
            { Language.Korean, "내게 워프" },
            { Language.Italian, "Teletrasporta da Me" }
        }},
        { "lobby.special_actions", new Dictionary<Language, string> {
            { Language.English, "Special Actions" },
            { Language.SimplifiedChinese, "特殊操作" },
            { Language.Japanese, "特殊アクション" },
            { Language.Korean, "특수 행동" },
            { Language.Italian, "Azioni Speciali" }
        }},
        { "lobby.spawn_scoutmaster", new Dictionary<Language, string> {
            { Language.English, "Spawn Scoutmaster" },
            { Language.SimplifiedChinese, "生成Scoutmaster" },
            { Language.Japanese, "スカウトマスター召喚" },
            { Language.Korean, "스카우트마스터 소환" },
            { Language.Italian, "Genera Scoutmaster" }
        }},
        { "tip.spawn_scoutmaster", new Dictionary<Language, string> {
            { Language.English, "Spawns a Scoutmaster near the selected player. Only works for host. Forces aggro." },
            { Language.SimplifiedChinese, "在选定玩家附近生成Scoutmaster。仅限房主使用。强制仇恨。" },
            { Language.Japanese, "選択したプレイヤーの近くにスカウトマスターを召喚します。ホスト専用。アグロ強制。" },
            { Language.Korean, "선택한 플레이어 근처에 스카우트마스터를 소환합니다. 호스트 전용. 어그로 강제." },
            { Language.Italian, "Genera uno Scoutmaster vicino al giocatore selezionato. Funziona solo per l'host. Forza l'aggro." }
        }},
        { "lobby.no_player_selected", new Dictionary<Language, string> {
            { Language.English, "No player selected." },
            { Language.SimplifiedChinese, "未选择玩家。" },
            { Language.Japanese, "プレイヤーが選択されていません。" },
            { Language.Korean, "플레이어가 선택되지 않았습니다." },
            { Language.Italian, "Nessun giocatore selezionato." }
        }},

        // World tab
        { "world.containers", new Dictionary<Language, string> {
            { Language.English, "Containers" },
            { Language.SimplifiedChinese, "容器" },
            { Language.Japanese, "コンテナ" },
            { Language.Korean, "컨테이너" },
            { Language.Italian, "Contenitori" }
        }},
        { "world.select_container", new Dictionary<Language, string> {
            { Language.English, "Select Container" },
            { Language.SimplifiedChinese, "选择容器" },
            { Language.Japanese, "コンテナ選択" },
            { Language.Korean, "컨테이너 선택" },
            { Language.Italian, "Seleziona Contenitore" }
        }},
        { "world.no_containers", new Dictionary<Language, string> {
            { Language.English, "No containers found." },
            { Language.SimplifiedChinese, "未找到容器。" },
            { Language.Japanese, "コンテナが見つかりません。" },
            { Language.Korean, "컨테이너를 찾을 수 없습니다." },
            { Language.Italian, "Nessun contenitore trovato." }
        }},
        { "world.refresh_luggage", new Dictionary<Language, string> {
            { Language.English, "Refresh Luggage List" },
            { Language.SimplifiedChinese, "刷新行李列表" },
            { Language.Japanese, "荷物リスト更新" },
            { Language.Korean, "수하물 목록 새로고침" },
            { Language.Italian, "Aggiorna Lista Bagagli" }
        }},
        { "tip.refresh_luggage", new Dictionary<Language, string> {
            { Language.English, "Reloads the list of luggage within 300m of your position." },
            { Language.SimplifiedChinese, "重新加载你位置300米内的行李列表。" },
            { Language.Japanese, "現在地から300m以内の荷物リストを再読み込みします。" },
            { Language.Korean, "현재 위치에서 300m 이내의 수하물 목록을 다시 불러옵니다." },
            { Language.Italian, "Ricarica l'elenco dei bagagli entro 300m dalla tua posizione." }
        }},
        { "world.all_nearby", new Dictionary<Language, string> {
            { Language.English, "All Nearby Containers" },
            { Language.SimplifiedChinese, "附近所有容器" },
            { Language.Japanese, "近くの全コンテナ" },
            { Language.Korean, "주변 모든 컨테이너" },
            { Language.Italian, "Tutti i Contenitori Vicini" }
        }},
        { "world.open_all_nearby", new Dictionary<Language, string> {
            { Language.English, "Open All Nearby" },
            { Language.SimplifiedChinese, "打开附近所有" },
            { Language.Japanese, "近くを全て開く" },
            { Language.Korean, "주변 모두 열기" },
            { Language.Italian, "Apri Tutti i Vicini" }
        }},
        { "world.warp_to_luggage", new Dictionary<Language, string> {
            { Language.English, "Warp To Luggage" },
            { Language.SimplifiedChinese, "传送到行李" },
            { Language.Japanese, "荷物にワープ" },
            { Language.Korean, "수하물로 워프" },
            { Language.Italian, "Teletrasporta al Bagaglio" }
        }},
        { "world.open_luggage", new Dictionary<Language, string> {
            { Language.English, "Open Luggage" },
            { Language.SimplifiedChinese, "打开行李" },
            { Language.Japanese, "荷物を開く" },
            { Language.Korean, "수하물 열기" },
            { Language.Italian, "Apri Bagaglio" }
        }},
        { "world.no_luggage_selected", new Dictionary<Language, string> {
            { Language.English, "No luggage selected." },
            { Language.SimplifiedChinese, "未选择行李。" },
            { Language.Japanese, "荷物が選択されていません。" },
            { Language.Korean, "수하물이 선택되지 않았습니다." },
            { Language.Italian, "Nessun bagaglio selezionato." }
        }},

        // About tab
        { "about.title", new Dictionary<Language, string> {
            { Language.English, "PEAK AIO Mod" },
            { Language.SimplifiedChinese, "PEAK AIO 模组" },
            { Language.Japanese, "PEAK AIO MOD" },
            { Language.Korean, "PEAK AIO 모드" },
            { Language.Italian, "PEAK AIO Mod" }
        }},
        { "about.version", new Dictionary<Language, string> {
            { Language.English, "Version: 1.0.12" },
            { Language.SimplifiedChinese, "版本: 1.0.12" },
            { Language.Japanese, "バージョン: 1.0.12" },
            { Language.Korean, "버전: 1.0.12" },
            { Language.Italian, "Versione: 1.0.12" }
        }},
        { "about.author", new Dictionary<Language, string> {
            { Language.English, "Author: K1rito (Inspired by OniGremlin)" },
            { Language.SimplifiedChinese, "作者: K1rito (启发于OniGremlin)" },
            { Language.Japanese, "作者: K1rito (OniGremlin からインスパイア)" },
            { Language.Korean, "제작자: K1rito (OniGremlin 에서 영감)" },
            { Language.Italian, "Autore: K1rito (Ispirato da OniGremlin)" }
        }},
        { "about.description", new Dictionary<Language, string> {
            { Language.English, "PEAK AIO is a quality-of-life and utility mod designed for the game PEAK. It brings together a wide range of player enhancements, inventory tools, world manipulation, and lobby control features in one sleek ImGui-powered interface." },
            { Language.SimplifiedChinese, "PEAK AIO 是为游戏 PEAK 设计的生活质量和实用模组。它将广泛的玩家增强、物品工具、世界操控和大厅控制功能集成在一个简洁的 ImGui 界面中。" },
            { Language.Japanese, "PEAK AIO はゲーム PEAK 向けに設計されたQOL・ユーティリティMODです。プレイヤー強化、インベントリツール、ワールド操作、ロビー制御機能を、洗練されたImGuiインターフェースにまとめています。" },
            { Language.Korean, "PEAK AIO는 게임 PEAK를 위해 설계된 편의성 및 유틸리티 모드입니다. 플레이어 강화, 인벤토리 도구, 월드 조작, 로비 제어 기능을 세련된 ImGui 인터페이스에 통합했습니다." },
            { Language.Italian, "PEAK AIO è una mod di qualità di vita e utilità progettata per il gioco PEAK. Riunisce una vasta gamma di miglioramenti per il giocatore, strumenti per l'inventario, manipolazione del mondo e funzionalità di controllo della lobby in un'elegante interfaccia basata su ImGui." }
        }},
        { "about.key_features", new Dictionary<Language, string> {
            { Language.English, "Key Features:" },
            { Language.SimplifiedChinese, "主要功能:" },
            { Language.Japanese, "主な機能:" },
            { Language.Korean, "주요 기능:" },
            { Language.Italian, "Caratteristiche Principali:" }
        }},
        { "about.feature1", new Dictionary<Language, string> {
            { Language.English, "Infinite stamina and affliction immunity" },
            { Language.SimplifiedChinese, "无限体力和状态异常免疫" },
            { Language.Japanese, "無限スタミナと状態異常免疫" },
            { Language.Korean, "무한 스태미나 및 상태이상 면역" },
            { Language.Italian, "Stamina infinita e immunità alle afflizioni" }
        }},
        { "about.feature2", new Dictionary<Language, string> {
            { Language.English, "Adjustable movement: speed, jump, and climb mods" },
            { Language.SimplifiedChinese, "可调节移动: 速度、跳跃和攀爬模组" },
            { Language.Japanese, "速度・ジャンプ・登攀の調整可能なMOD" },
            { Language.Korean, "조정 가능한 이동: 속도, 점프, 등반 모드" },
            { Language.Italian, "Movimento regolabile: mod di velocità, salto e arrampicata" }
        }},
        { "about.feature3", new Dictionary<Language, string> {
            { Language.English, "Real-time inventory editing and recharge" },
            { Language.SimplifiedChinese, "实时物品编辑和充能" },
            { Language.Japanese, "リアルタイムインベントリ編集とチャージ" },
            { Language.Korean, "실시간 인벤토리 편집 및 충전" },
            { Language.Italian, "Modifica e ricarica dell'inventario in tempo reale" }
        }},
        { "about.feature4", new Dictionary<Language, string> {
            { Language.English, "Player-to-player warp, revive, and kill tools" },
            { Language.SimplifiedChinese, "玩家间传送、复活和击杀工具" },
            { Language.Japanese, "プレイヤー間ワープ・復活・キルツール" },
            { Language.Korean, "플레이어 간 워프, 부활, 처치 도구" },
            { Language.Italian, "Strumenti di teletrasporto, rianimazione e uccisione tra giocatori" }
        }},
        { "about.feature5", new Dictionary<Language, string> {
            { Language.English, "Custom teleportation and ping-based movement" },
            { Language.SimplifiedChinese, "自定义传送和基于标记的移动" },
            { Language.Japanese, "カスタムテレポートとピングベース移動" },
            { Language.Korean, "사용자 정의 텔레포트 및 핑 기반 이동" },
            { Language.Italian, "Teletrasporto personalizzato e movimento basato su ping" }
        }},
        { "about.feature6", new Dictionary<Language, string> {
            { Language.English, "Stylized UI with tabbed interface" },
            { Language.SimplifiedChinese, "风格化的标签页界面" },
            { Language.Japanese, "タブ付きスタイリッシュUI" },
            { Language.Korean, "탭 인터페이스를 갖춘 스타일리시 UI" },
            { Language.Italian, "Interfaccia stilizzata con schede" }
        }},
        { "about.thanks", new Dictionary<Language, string> {
            { Language.English, "Special Thanks:" },
            { Language.SimplifiedChinese, "特别感谢:" },
            { Language.Japanese, "スペシャルサンクス:" },
            { Language.Korean, "특별 감사:" },
            { Language.Italian, "Ringraziamenti Speciali:" }
        }},
        { "about.thanks1", new Dictionary<Language, string> {
            { Language.English, "Penswer for insight, and guidance" },
            { Language.SimplifiedChinese, "Penswer 提供的见解和指导" },
            { Language.Japanese, "Penswer 氏の洞察とガイダンス" },
            { Language.Korean, "Penswer의 통찰과 안내" },
            { Language.Italian, "Penswer per intuizione e guida" }
        }},
        { "about.thanks2", new Dictionary<Language, string> {
            { Language.English, "BepInEx team for the modding framework" },
            { Language.SimplifiedChinese, "BepInEx 团队提供的模组框架" },
            { Language.Japanese, "BepInEx チームのMODフレームワーク" },
            { Language.Korean, "BepInEx 팀의 모딩 프레임워크" },
            { Language.Italian, "Team BepInEx per il framework di modding" }
        }},
        { "about.thanks3", new Dictionary<Language, string> {
            { Language.English, "DearImGuiInjection for seamless UI integration" },
            { Language.SimplifiedChinese, "DearImGuiInjection 提供的无缝UI集成" },
            { Language.Japanese, "DearImGuiInjection のシームレスなUI統合" },
            { Language.Korean, "DearImGuiInjection의 원활한 UI 통합" },
            { Language.Italian, "DearImGuiInjection per l'integrazione UI senza soluzione di continuità" }
        }},
        { "about.thanks4", new Dictionary<Language, string> {
            { Language.English, "HarmonyX for runtime patching support" },
            { Language.SimplifiedChinese, "HarmonyX 提供的运行时补丁支持" },
            { Language.Japanese, "HarmonyX のランタイムパッチサポート" },
            { Language.Korean, "HarmonyX의 런타임 패치 지원" },
            { Language.Italian, "HarmonyX per il supporto al patching runtime" }
        }},
        { "about.disclaimer", new Dictionary<Language, string> {
            { Language.English, "This mod is provided as-is for educational and personal use. Not affiliated with or endorsed by the developers of PEAK. Use responsibly." },
            { Language.SimplifiedChinese, "此模组按原样提供，仅供教育和个人使用。与PEAK的开发者无关，也未获得其认可。请负责任地使用。" },
            { Language.Japanese, "このMODは教育および個人使用を目的として現状のまま提供されます。PEAKの開発者とは無関係であり、推奨もされていません。責任を持ってご使用ください。" },
            { Language.Korean, "이 모드는 교육 및 개인 용도로 있는 그대로 제공됩니다. PEAK 개발자와 관련이 없으며 승인을 받지 않았습니다. 책임감 있게 사용하세요." },
            { Language.Italian, "Questa mod è fornita così com'è per uso educativo e personale. Non affiliata o approvata dagli sviluppatori di PEAK. Usare responsabilmente." }
        }},

        // Language tab
        { "lang.title", new Dictionary<Language, string> {
            { Language.English, "Language Settings" },
            { Language.SimplifiedChinese, "语言设置" },
            { Language.Japanese, "言語設定" },
            { Language.Korean, "언어 설정" },
            { Language.Italian, "Impostazioni Lingua" }
        }},
        { "lang.select", new Dictionary<Language, string> {
            { Language.English, "Select Language:" },
            { Language.SimplifiedChinese, "选择语言:" },
            { Language.Japanese, "言語を選択:" },
            { Language.Korean, "언어 선택:" },
            { Language.Italian, "Seleziona Lingua:" }
        }},
        { "lang.current", new Dictionary<Language, string> {
            { Language.English, "Current Language: English" },
            { Language.SimplifiedChinese, "当前语言: 简体中文" },
            { Language.Japanese, "現在の言語: 日本語" },
            { Language.Korean, "현재 언어: 한국어" },
            { Language.Italian, "Lingua Corrente: Italiano" }
        }},
    };

    public static readonly string[] LanguageNames = { "English", "简体中文", "日本語", "한국어", "Italiano" };

    public static string T(string key)
    {
        if (Strings.TryGetValue(key, out var translations))
        {
            if (translations.TryGetValue(CurrentLanguage, out var text))
                return text;
            if (translations.TryGetValue(Language.English, out var fallback))
                return fallback;
        }
        return key;
    }

    public static string T(string key, params object[] args)
    {
        return string.Format(T(key), args);
    }

    public static void SetLanguage(Language lang)
    {
        CurrentLanguage = lang;
    }

    public static void SetLanguage(int index)
    {
        if (index >= 0 && index < LanguageNames.Length)
            CurrentLanguage = (Language)index;
    }
}
