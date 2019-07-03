using System;
using BuildVerse;
using CreativerseMod.Helpers;
using HarmonyLib;
using I2.Loc.SimpleJSON;
using UModFramework.API;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(Builder), "Build", null)]
    internal class Patch_InfiniteItem
    {
        public static void Postfix(Builder __instance, string protoItemName, Vector3i location, Matrix3i rotation,
            int quickSlotUsed = -1, JSONClass uniqueData = null, int _buildCount = 0)
        {
            if (__instance == Player.Local.Builder && LoaderConfig.Instance.InfiniteItem)
            {
                UMFGUI.AddConsoleText("Placed: " + protoItemName);
                var protoItemByName = ProtoDatabase.GetProtoItemByName(protoItemName, Array.Empty<FindOptions>());
                var protoItemBlock = protoItemByName as ProtoItemBlock;
                if (!protoItemName.Contains("hearthstone_white_item"))
                {
                    protoItemBlock.GetProtoBlock();
                    GameInventoryHelper.AddItem(protoItemByName, _buildCount != 0 ? _buildCount : 1, true);
                }
            }
        }
    }
}