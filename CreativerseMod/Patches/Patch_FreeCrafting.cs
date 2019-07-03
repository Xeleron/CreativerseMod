using BuildVerse;
using CreativerseMod.Helpers;
using HarmonyLib;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(DragDropCraftingPanel))]
    [HarmonyPatch("CraftItem")]
    public class Patch_FreeCrafting
    {
        private static bool Prefix(DragDropCraftingPanel __instance)
        {
            if (!LoaderConfig.Instance.FreeCrafting) return true;
            var instanceField = Util.GetInstanceField<ProtoCraft>(__instance, "_currentCraft");
            var _craftMultiplier = Util.GetInstanceField<int>(__instance, "_craftMultiplier");
            if (instanceField != null)
            {
                if (LoaderConfig.Instance.AvoidCraftNotice)
                    GameInventoryHelper.AddItem(instanceField.Result, instanceField.ResultCount * _craftMultiplier);
                else
                    instanceField.Components.ForEach(delegate(CraftComponent comp)
                    {
                        var item = comp.ValidItems.PickRandom();
                        var num = comp.RequiredCount * _craftMultiplier;
                        if (!GameInventoryHelper.ContainsItem(item, num)) GameInventoryHelper.AddItem(item, num);
                    });
                return false;
            }

            return true;
        }
    }
}