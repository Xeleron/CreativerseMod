using System;
using BuildVerse;
using CreativerseMod.Helpers;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(DragDropCraftingPanel))]
    [HarmonyPatch("CraftItem")]
    public class Patch_FreeCrafting
    {
        private static bool Prefix(DragDropCraftingPanel __instance)
        {
            if (!LoaderConfig.Instance.FreeCrafting)
            {
                return true;
            }
            ProtoCraft instanceField = Util.GetInstanceField<ProtoCraft>(__instance, "_currentCraft");
            int _craftMultiplier = Util.GetInstanceField<int>(__instance, "_craftMultiplier");
            if (instanceField != null)
            {
                if (LoaderConfig.Instance.AvoidCraftNotice)
                {
                    GameInventoryHelper.AddItem(instanceField.Result, instanceField.ResultCount * _craftMultiplier, false, false);
                }
                else
                {
                    instanceField.Components.ForEach(delegate (CraftComponent comp)
                    {
                        ProtoItem item = comp.ValidItems.PickRandom<ProtoItem>();
                        int num = comp.RequiredCount * _craftMultiplier;
                        if (!GameInventoryHelper.ContainsItem(item, num, false))
                        {
                            GameInventoryHelper.AddItem(item, num, false, false);
                        }
                    });
                }
                return false;
            }
            return true;
        }
    }
}