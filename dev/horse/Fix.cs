using HarmonyLib;
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

    }
}
