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
            if(LoaderConfig.Instance.EnableGlider)
            {
                if(msg.GetType() == typeof(EquipmentEnableFeature))
                {
                    EquipmentEnableFeature _msg = (EquipmentEnableFeature)msg;

                    switch(_msg.Feature)
                    {
                        case EquipmentFeature.Glider:
                        {
                            __instance.GliderOn = true;
                            break;
                        }
                        case EquipmentFeature.Light:
                        {
                            __instance.LightOn = true;
                            break;
                        }
                    }
                    return false;
                }
                return true;
            }  
            return true;
        }

        public static void Postfix(User __instance)
        {
            
        }
    }
}  