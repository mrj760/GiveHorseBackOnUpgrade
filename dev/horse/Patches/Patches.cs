using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

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
            if (!upgradeFromTroop.HasMount() || !upgradeToTroop.HasMount()
                || !upgradeToTroop.UpgradeRequiresItemFromCategory.IsAnimal)
                return;

            var horse = upgradeFromTroop.Equipment.Horse.Item;
            if (horse.Equals(upgradeToTroop.Equipment.Horse.Item))
                return;

            // add the From's horse back to the player's inventory
            var party = MobileParty.MainParty;
            var inv = party.ItemRoster;
            var item = new ItemRosterElement(horse, number);
            inv.Add(item);
            var msg = "Horse" + (number > 1 ? "s" : "") + " Retrived: ";
            msg += (number > 1 ? number + " " : "") + item.EquipmentElement.GetModifiedItemName();
            MBInformationManager.AddQuickInformation(new TaleWorlds.Localization.TextObject(msg));
        }
    }
}


