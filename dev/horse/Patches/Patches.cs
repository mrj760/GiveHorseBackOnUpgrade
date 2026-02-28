using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace horse.Patches
{
    [HarmonyPatch]
    public class GiveHorseBackOnUpgrade
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CampaignEvents), "OnPlayerUpgradedTroops")]
        public static void AddHorseBackToInventoryPatch(CharacterObject upgradeFromTroop,
            CharacterObject upgradeToTroop, int number)
        {
            if (!upgradeFromTroop.HasMount() || !upgradeToTroop.HasMount()) return;

            var horse = upgradeFromTroop.Equipment?.Horse.Item;
            if (horse == null) return;

            var upgradeReq = upgradeToTroop.UpgradeRequiresItemFromCategory;
            if (upgradeReq == null || upgradeReq.StringId != "war_horse") return;

            // add the From's horse back to the player's inventory
            var item = new ItemRosterElement(horse, number);
            MobileParty.MainParty.ItemRoster.Add(item);
            var msg = "Horse" + (number > 1 ? "s" : "") + " Retrieved: ";
            var name = item.EquipmentElement.GetModifiedItemName();
            name = TextObject.IsNullOrEmpty(name) ? new TextObject("...") : name;

            msg += (number > 1 ? number + " " : "") + name;
            MBInformationManager.AddQuickInformation(new TextObject(msg));
        }
    }
}