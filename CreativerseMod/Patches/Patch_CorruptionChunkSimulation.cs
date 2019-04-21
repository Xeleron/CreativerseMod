using System;
using BuildVerse;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(CorruptionChunkSimulation), "HasToxicity", new Type[]
    {
        typeof(BlockData)
    })]
    internal class Patch_CorruptionChunkSimulation
    {
        private static bool Prefix(CorruptionChunkSimulation __instance, BlockData bd, ref bool __result)
        {
            if (!LoaderConfig.Instance.DisableCorruptionDamage)
            {
                return true;
            }
            if (BlockDataFluidExtension.IsFluid(bd) || BlockDataFireExtension.IsFire(bd) || bd.ProtoBlock.DefaultToxicity > 0)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
