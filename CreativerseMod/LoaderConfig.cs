using System;
using UModFramework.API;

namespace CreativerseMod
{
    internal class LoaderConfig
    {
        private const string ConfigVersion = "1.0";

        public bool AvoidCraftNotice;

        public bool DisableCorruptionDamage;

        public bool FreeCrafting;

        public bool InfiniteItem;

        public bool InstantKill;

        public bool InstantMining;

        public bool InstantTool;

        public bool MobsCantHurtYou;

        public bool NoFall;

        public static LoaderConfig Instance { get; } = new LoaderConfig();

        public void Load()
        {
            Loader.Log("Loading Settings...");
            try
            {
                using (var umfconfig = new UMFConfig())
                {
                    var a = umfconfig.Read("ConfigVersion",
                        new UMFConfigString("", "", false, false, Array.Empty<string>()), Array.Empty<string>());
                    if (a != string.Empty && a != ConfigVersion)
                    {
                        umfconfig.DeleteConfig();
                        Loader.Log(
                            "The config file was outdated and has been deleted. A new config will be generated.");
                    }

                    umfconfig.Read("Enabled", new UMFConfigBool(true, null), Array.Empty<string>());
                    umfconfig.Read("LoadPriority",
                        new UMFConfigString("Normal", "", false, false, Array.Empty<string>()), Array.Empty<string>());
                    umfconfig.Write("MinVersion", new UMFConfigString("0.45", "", false, false, Array.Empty<string>()),
                        Array.Empty<string>());
                    umfconfig.Write("MaxVersion", new UMFConfigString("0.53", "", false, false, Array.Empty<string>()),
                        Array.Empty<string>());
                    umfconfig.Write("ConfigVersion",
                        new UMFConfigString(ConfigVersion, "", false, false, Array.Empty<string>()),
                        Array.Empty<string>());
                    Loader.Log("Finished UMF Settings.");
                    MobsCantHurtYou = umfconfig.Read("MobsCantHurtYou", new UMFConfigBool(true, false),
                        "Mobs cant see you");
                    FreeCrafting = umfconfig.Read("FreeCrafting", new UMFConfigBool(true, false),
                        "Craft any item in Crafting");
                    DisableCorruptionDamage = umfconfig.Read("DisableCorruptionDamage", new UMFConfigBool(true, false),
                        "Disable Corruption Damage?");
                    InstantKill = umfconfig.Read("InstantKill", new UMFConfigBool(true, false), "Instant Kill?");
                    InstantTool = umfconfig.Read("Instant Tool", new UMFConfigBool(true, false), "Instant Tool?");
                    InfiniteItem = umfconfig.Read("InfiniteItem", new UMFConfigBool(true, false), "Infinite Item?");
                    AvoidCraftNotice = umfconfig.Read("AvoidCraftNotice", new UMFConfigBool(true, false),
                        "Remove Crafting Notice");
                    InstantMining = umfconfig.Read("InstantMining", new UMFConfigBool(true, false), "InstantMining");
                    NoFall = umfconfig.Read("NoFall", new UMFConfigBool(true, false), "NoFall");
                    Loader.Log("Finished loading settings.");
                }
            }
            catch (Exception ex)
            {
                Loader.Log(string.Concat("Error loading mod settings: ", ex.Message, " (", ex.InnerException?.Message,
                    ")"));
            }
        }
    }
}