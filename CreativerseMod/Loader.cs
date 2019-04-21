using System.Collections.Generic;
using BuildVerse;
using CreativerseMod.Helpers;
using UModFramework.API;

namespace CreativerseMod
{
    public class Loader
    {
        [UMFHarmony(10, false)]
        public static void Start()
        {
            Loader.Log("My First Mod v" + UMFMod.GetModVersion().ToString(), true);
        }

        [UMFConfig]
        public static void LoadConfig()
        {
            LoaderConfig.Instance.Load();
        }


        internal static void Log(string text, bool clean = false)
        {
            using (UMFLog umflog = new UMFLog())
            {
                umflog.Log(text, clean);
            }
        }

        public static ProtoDatabase GetProtoDatabase
        {
            get
            {
                return (ProtoDatabase)Util.GetStaticField(typeof(ProtoDatabase), "Instance");
            }
        }

        public static Dictionary<string, ProtoCraft> GetGameCraftings()
        {
            return Util.GetInstanceField<Dictionary<string, ProtoCraft>>(Loader.GetProtoDatabase, "_protoCraftsByName");
        }

        public static Dictionary<string, ProtoItem> GetGameItems()
        {
            return Util.GetInstanceField<Dictionary<string, ProtoItem>>(Loader.GetProtoDatabase, "_protoItemsByName");
        }
    }
}
