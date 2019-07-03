using BuildVerse;
using HarmonyLib;

namespace CreativerseMod.Patches
{
    //private float GetMiningDuration(Vector3i? location, out Builder.NoMiningAllowedReason reason, out int tierNeeded)
    [HarmonyPatch(typeof(Builder), "GetMiningDuration")]
    public class Patch_InstantMining
    {
        private static bool Prefix(ref float __result)
        {
            if (!LoaderConfig.Instance.InstantMining) return true;
            __result = 0f;
            return false;
        }
    }
}