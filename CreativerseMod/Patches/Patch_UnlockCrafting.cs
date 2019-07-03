using BuildVerse;
using CreativerseMod.Helpers;
using HarmonyLib;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(DragDropCraftingPanel))]
    [HarmonyPatch("UpdateCraftButton")]
    public class Patch_UnlockCrafting
    {
        private static bool Prefix(DragDropCraftingPanel __instance)
        {
            if (LoaderConfig.Instance.FreeCrafting)
            {
                var protoCraft = Util.GetInstanceField<ProtoCraft>(__instance, "_currentCraft");
                if (Player.Local.Crafting.IsUnlocked(protoCraft))
                {
                    __instance.CraftButton.isEnabled = true;
                    return false;
                }

                return true;
            }

            return true;
        }
    }
}