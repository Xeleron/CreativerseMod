using Harmony;
using BuildVerse;
using System;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(Equipment), "InternalComponentMessage")]
    class Patch_Glider
    {
        public static bool Prefix(Equipment __instance, EntityComponentMessage msg)
        {
            
        }
    }
}  