using BuildVerse;
using HarmonyLib;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(Stats), "GetStatValue", typeof(StatType))]
    internal class Patch_CombatStats
    {
        public static bool Prefix(Stats __instance, StatType st, float __result)
        {
            return !LoaderConfig.Instance.DisableCorruptionDamage ||
                   st != StatType.Corruption && st != StatType.CorruptionDamageFactor || !__instance.Owner.IsPlayer ||
                   !__instance.Owner.locallyOwned;
        }
    }
}