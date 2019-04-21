using System;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(TamingCollar), "GetChannelDuration", null)]
    internal class Patch_InstantTame
    {
        public static bool Prefix(TamingCollar __instance, float __result)
        {
            if(LoaderConfig.Instance.InstantTool)
            {
                __result = 0.1f;
                return false;
            }
            return true;
        }
    }
}