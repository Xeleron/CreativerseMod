using BuildVerse;
using HarmonyLib;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(CorruptionChunkSimulation), "HasToxicity", typeof(BlockData))]
    internal class Patch_CorruptionChunkSimulation
    {
        private static bool Prefix(CorruptionChunkSimulation __instance, BlockData bd, ref bool __result)
        {
            if (!LoaderConfig.Instance.DisableCorruptionDamage) return true;
            if (bd.IsFluid() || bd.IsFire() || bd.ProtoBlock.DefaultToxicity > 0)
            {
                __result = false;
                return false;
            }

            return true;
        }
    }
}