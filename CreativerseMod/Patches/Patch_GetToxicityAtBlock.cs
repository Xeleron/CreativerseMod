using System;
using BuildVerse;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(CorruptionChunkSimulation), "GetToxicityAtBlock", new Type[]
    {
        typeof(Vector3i),
        typeof(int),
        typeof(int),
        typeof(Vector3i)
    })]
    internal class Patch_GetToxicityAtBlock
    {
        private static bool Prefix(Vector3i worldPos, int resistance, int distance, Vector3i dir, ref int __result)
        {
            if (LoaderConfig.Instance.DisableCorruptionDamage)
            {
                __result = 0;
                return false;
            }
            return true;
        }
    }
}
