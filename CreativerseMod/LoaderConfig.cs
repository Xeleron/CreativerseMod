using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UModFramework.API;

namespace CreativerseMod
{
    class LoaderConfig
    {
        public void Load()
        {
            Loader.Log("Loading Settings...", false);
            try
            {
                using (UMFConfig umfconfig = new UMFConfig())
                {
                    string a = umfconfig.Read<string>("ConfigVersion", new UMFConfigString("", "", false, false, Array.Empty<string>()), Array.Empty<string>());
                    if (a != string.Empty && a != LoaderConfig.configVersion)
                    {
                        umfconfig.DeleteConfig();
                        Loader.Log("The config file was outdated and has been deleted. A new config will be generated.", false);
                    }
                    umfconfig.Read<bool>("Enabled", new UMFConfigBool(new bool?(true), null, false), Array.Empty<string>());
                    umfconfig.Read<string>("LoadPriority", new UMFConfigString("Normal", "", false, false, Array.Empty<string>()), Array.Empty<string>());
                    umfconfig.Write<string>("MinVersion", new UMFConfigString("0.45", "", false, false, Array.Empty<string>()), Array.Empty<string>());
                    umfconfig.Write<string>("MaxVersion", new UMFConfigString("0.49.99999.99999", "", false, false, Array.Empty<string>()), Array.Empty<string>());
                    umfconfig.Write<string>("ConfigVersion", new UMFConfigString(LoaderConfig.configVersion, "", false, false, Array.Empty<string>()), Array.Empty<string>());
                    Loader.Log("Finished UMF Settings.", false);
                    this.MobsCantHurtYou = umfconfig.Read<bool>("MobsCantHurtYou", new UMFConfigBool(new bool?(true), new bool?(false), false), new string[]
                    {
                        "Mobs cant see you"
                    });
                    this.FreeCrafting = umfconfig.Read<bool>("FreeCrafting", new UMFConfigBool(new bool?(true), new bool?(false), false), new string[]
                    {
                        "Craft any item in Crafting"
                    });
                    this.DisableCorruptionDamage = umfconfig.Read<bool>("DisableCorruptionDamage", new UMFConfigBool(new bool?(true), new bool?(false), false), new string[]
                    {
                        "Disable Corruption Damage?"
                    });
                    this.InstantKill = umfconfig.Read<bool>("InstantKill", new UMFConfigBool(new bool?(true), new bool?(false), false), new string[]
                    {
                        "Instant Kill?"
                    });
                    this.InstantTool = umfconfig.Read<bool>("Instant Tool", new UMFConfigBool(new bool?(false), new bool?(false), false), new string[]
                    {
                        "Instant Tool?"
                    });
                    this.InfiniteItem = umfconfig.Read<bool>("InfiniteItem", new UMFConfigBool(new bool?(false), new bool?(false), false), new string[]
                    {
                        "Infinite Item?"
                    });
                    this.AvoidCraftNotice = umfconfig.Read<bool>("AvoidCraftNotice", new UMFConfigBool(new bool?(false), new bool?(false), false), new string[]
                    {
                        "Remove Crafting Notice"
                    });
                    this.EnableGlider = umfconfig.Read<bool>("Enable Glider", new UMFConfigBool(true, false, false), new string[]
                    {
                        "Glider Feature"
                    });
                    Loader.Log("Finished loading settings.", false);
                }
            }
            catch (Exception ex)
            {
                Loader.Log(string.Concat(new string[]
                {
                    "Error loading mod settings: ",
                    ex.Message,
                    " (",
                    ex.InnerException.Message,
                    ")"
                }), false);
            }
        }

        public static LoaderConfig Instance { get; } = new LoaderConfig();

        public static readonly string configVersion = "1.0";

        public bool MobsCantHurtYou;

        public bool FreeCrafting;

        public bool AvoidCraftNotice;

        public bool DisableCorruptionDamage;

        public bool InstantKill;

        public bool InstantTool;

        public bool InfiniteItem;

        public bool EnableGlider;
    }
}
