using System.Collections.Generic;
using BuildVerse;
using CreativerseMod.Helpers;
using UModFramework.API;

namespace CreativerseMod
{
    public class Loader
    {
        public static ProtoDatabase GetProtoDatabase =>
            (ProtoDatabase) Util.GetStaticField(typeof(ProtoDatabase), "Instance");

        [UMFHarmony(12)]
        public static void Start()
        {
            Log("Creativerse Mod v" + UMFMod.GetModVersion(), true);
        }

        [UMFConfig]
        public static void LoadConfig()
        {
            LoaderConfig.Instance.Load();
        }


        internal static void Log(string text, bool clean = false)
        {
            using (var umflog = new UMFLog())
            {
                umflog.Log(text, clean);
            }
        }

        public static Dictionary<string, ProtoCraft> GetGameCraftings()
        {
            return Util.GetInstanceField<Dictionary<string, ProtoCraft>>(GetProtoDatabase, "_protoCraftsByName");
        }

        public static Dictionary<string, ProtoItem> GetGameItems()
        {
            return Util.GetInstanceField<Dictionary<string, ProtoItem>>(GetProtoDatabase, "_protoItemsByName");
        }
    }
}