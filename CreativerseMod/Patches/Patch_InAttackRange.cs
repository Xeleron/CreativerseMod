using System;
using BehaviorDesigner.Runtime.Tasks;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(InAttackRange))]
    [HarmonyPatch("OnUpdate")]
    internal class Patch_InAttackRange
    {
        private static bool Prefix(InAttackRange __instance, ref TaskStatus __result)
        {
            if (LoaderConfig.Instance.MobsCantHurtYou)
            {
                __result = TaskStatus.Failure;
                return false;
            }
            return true;
        }
    }
}