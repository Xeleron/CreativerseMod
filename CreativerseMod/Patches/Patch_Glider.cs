using System;
using BuildVerse;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(Equipment))]
    [HarmonyPatch(nameof(Equipment.GliderOn), PropertyMethod.Getter)]
    public class Patch_Glider
    {
        public static bool Prefix(Equipment __instance, bool __result)
        {
            return false;
        }
    }
}