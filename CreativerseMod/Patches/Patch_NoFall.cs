using HarmonyLib;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(CharacterCollider), "Fell")]
    public class Patch_NoFall
    {
        private static bool Prefix()
        {
            return !LoaderConfig.Instance.NoFall;
        }
    }
}