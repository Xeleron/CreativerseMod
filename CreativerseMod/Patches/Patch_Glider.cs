using Harmony;
using BuildVerse;
using System;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(User), nameof(User.IsProUser))]
    class Patch_Glider
    {
        public static bool Prefix(User __instance, bool __result)
        {
            if(LoaderConfig.Instance.EnableGlider)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}