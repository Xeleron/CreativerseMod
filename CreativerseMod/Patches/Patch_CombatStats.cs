using System;
using BuildVerse;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(Stats), "GetStatValue", new Type[]
    { 
        typeof(StatType)
    })]
    internal class Patch_CombatStats
    {
        public static bool Prefix(Stats __instance, StatType st, float __result)
        {
            return !LoaderConfig.Instance.DisableCorruptionDamage || (st != StatType.Corruption && st != StatType.CorruptionDamageFactor) || !__instance.Owner.IsPlayer || !__instance.Owner.locallyOwned;
        }
    }
}
