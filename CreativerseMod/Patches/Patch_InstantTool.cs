using HarmonyLib;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(ChanneledTool), "GetChannelDuration", null)]
    public class Patch_InstantTool
    {
        public static bool Prefix(ChanneledTool __instance, float __result)
        {
            return !LoaderConfig.Instance.InstantTool;
        }
    }
}