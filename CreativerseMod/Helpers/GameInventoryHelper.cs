using System;
using System.Collections.Generic;
using System.Linq;
using BuildVerse;

namespace CreativerseMod.Helpers
{
    public static class GameInventoryHelper
    {
        public static List<ProtoCraft> getLockedItems()
        {
            List<ProtoCraft> list = new List<ProtoCraft>();
            if (Player.Local == null && !Player.Local.IsConnected)
            {
                return list;
            }
            foreach (ProtoCraft protoCraft in Loader.GetGameCraftings().Values)
            {
                if (!Player.Local.Crafting.IsUnlocked(protoCraft))
                {
                    list.Add(protoCraft);
                }
            }
            return list;
        }

        public static ProtoCraft getProtoCraft(ProtoItem item)
        {
            ProtoCraft result = null;
            foreach (ProtoCraft protoCraft in Loader.GetGameCraftings().Values)
            {
                if (protoCraft.Result == item)
                {
                    result = protoCraft;
                    break;
                }
            }
            return result;
        }

        public static EntityComponentMessage AddItem(ProtoItem item, int Amount, bool isQuickSlot = false, bool simulateCraft = false)
        {
            if (Player.Local == null && !Player.Local.IsConnected)
            {
                return null;
            }
            Inventory inventory = Player.Local.Inventory;
            int num = ItemStack.MaxStackCountForProtoItem(item);
            int num2 = (Amount >= num) ? num : Amount;
            ItemStack itemStack = new ItemStack(item, num2, null);
            if (!GameInventoryHelper.ContainsItem(item, isQuickSlot))
            {
                short num3 = isQuickSlot ? inventory.FirstEmptyQuickSlot() : inventory.GetFirstEmptySlot(false);
                InventoryAdd inventoryAdd = new InventoryAdd(inventory, num3, itemStack, isQuickSlot);
                inventory.SendToServer(inventoryAdd);
                inventory.OnComponentMessage(inventoryAdd);
                if (simulateCraft)
                {
                    AudioController.Play(Player.Local.Crafting.SfxCraft);
                    NotificationCenter.Default.PostNotification(Player.CraftedSomethingEvent, GameInventoryHelper.getProtoCraft(item));
                }
                if (inventoryAdd.Item.ProtoItem is ProtoItemEquipment)
                {
                    ProtoItemEquipment protoItemEquipment = (ProtoItemEquipment)inventoryAdd.Item.ProtoItem;
                    InventoryEquip inventoryEquip = new InventoryEquip(inventory, true, inventoryAdd.Slot, protoItemEquipment.Slot, inventoryAdd.SlotIsQuickSlot);
                    inventory.SendToServer(inventoryEquip);
                    inventory.OnComponentMessage(inventoryEquip);
                }
                return inventoryAdd;
            }
            short num4;
            if (tryGetItemIndexEnough(itemStack.ProtoItem, inventory, out num4, isQuickSlot))
            {
                InventoryModified inventoryModified = new InventoryModified(inventory, num4, (short)num2, isQuickSlot);
                inventory.SendToServer(inventoryModified);
                inventory.OnComponentMessage(inventoryModified);
                if (simulateCraft)
                {
                    AudioController.Play(Player.Local.Crafting.SfxCraft);
                    NotificationCenter.Default.PostNotification(Player.CraftedSomethingEvent, getProtoCraft(itemStack.ProtoItem));
                }
                return inventoryModified;
            }
            if (!tryGetItemIndexEnough(itemStack.ProtoItem, inventory, out num4, isQuickSlot))
            {
                InventoryAdd inventoryAdd2 = new InventoryAdd(inventory, num4, itemStack, isQuickSlot);
                inventory.OnComponentMessage(inventoryAdd2);
                inventory.SendToServer(inventoryAdd2);
                if (simulateCraft)
                {
                    AudioController.Play(Player.Local.Crafting.SfxCraft);
                    NotificationCenter.Default.PostNotification(Player.CraftedSomethingEvent, getProtoCraft(itemStack.ProtoItem));
                }
                if (inventoryAdd2.Item.ProtoItem is ProtoItemEquipment)
                {
                    ProtoItemEquipment protoItemEquipment2 = (ProtoItemEquipment)inventoryAdd2.Item.ProtoItem;
                    InventoryEquip inventoryEquip2 = new InventoryEquip(inventory, true, inventoryAdd2.Slot, protoItemEquipment2.Slot, inventoryAdd2.SlotIsQuickSlot);
                    inventory.SendToServer(inventoryEquip2);
                    inventory.OnComponentMessage(inventoryEquip2);
                }
                return inventoryAdd2;
            }
            return null;
        }

        public static bool tryGetItemIndexEnough(ProtoItem protoItem, Inventory inv, out short slot, bool isQuickSlot)
        {
            for (int i = 0; i < (isQuickSlot ? getInventoryQuickSlotItems().Count : getInventoryItems().Count); i++)
            {
                ItemStack itemStack = isQuickSlot ? getInventoryQuickSlotItems()[i] : getInventoryItems()[i];
                int num = (itemStack != null) ? ItemStack.MaxStackCountForProtoItem(itemStack.ProtoItem) : 0;
                if (itemStack.ProtoItem != null && (itemStack.ProtoItem == protoItem && itemStack.StackSize < num && num != 0))
                {
                    slot = (short)i;
                    return true;
                }
            }
            slot = (isQuickSlot ? inv.FirstEmptyQuickSlot() : inv.GetFirstEmptySlot(false));
            return false;
        }

        public static List<ItemStack> getInventoryItems()
        {
            return (List<ItemStack>)Util.GetInstanceField(typeof(Inventory), Player.Local.Inventory, "_items");
        }

        public static List<ItemStack> getInventoryQuickSlotItems()
        {
            return ((ItemStack[])Util.GetInstanceField(typeof(Inventory), Player.Local.Inventory, "_quickSlotItems")).ToList<ItemStack>();
        }

        public static bool ContainsItem(ProtoItem Item, int RequiredCount, bool quickSlot = false)
        {
            bool result = false;
            checked
            {
                for (int i = 0; i < ((!quickSlot) ? getInventoryItems().Count : getInventoryQuickSlotItems().Count); i++)
                {
                    ItemStack itemStack = (!quickSlot) ? getInventoryItems()[i] : getInventoryQuickSlotItems()[i];
                    int num = (itemStack != null) ? ItemStack.MaxStackCountForProtoItem(itemStack.ProtoItem) : 0;
                    if (itemStack != null && (itemStack.ProtoItem == Item && itemStack.StackSize <= RequiredCount && num != 0))
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            }
        }

        public static bool ContainsItem(ProtoItem Item, bool quickSlot = false)
        {
            bool result = false;
            checked
            {
                for (int i = 0; i < ((!quickSlot) ? getInventoryItems().Count : getInventoryQuickSlotItems().Count); i++)
                {
                    ItemStack itemStack = (!quickSlot) ? getInventoryItems()[i] : getInventoryQuickSlotItems()[i];
                    int num = (itemStack != null) ? ItemStack.MaxStackCountForProtoItem(itemStack.ProtoItem) : 0;
                    if (itemStack != null && (itemStack.ProtoItem == Item && itemStack.StackSize < num && num != 0))
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            }
        }
    }
}