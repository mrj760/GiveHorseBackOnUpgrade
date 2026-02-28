using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace horse
{
    public class Fix : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            // apply harmony patches
            var harmony = new Harmony("horse");
            harmony.PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            if (game.GameType is TaleWorlds.CampaignSystem.Campaign)
            {
                var msg = new InformationMessage("Don't Kill My Horse (GiveHorseBackOnUpgrade) - Success");
                InformationManager.DisplayMessage(msg);
            }
        }
    }
}