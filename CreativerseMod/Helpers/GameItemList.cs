using System.Collections.Generic;
using System.Linq;
using BuildVerse;

namespace CreativerseMod.Helpers
{
    public static class GameItemList
    {
        static GameItemList()
        {
            Dictionary<string, ProtoItem> instanceField = Util.GetInstanceField<Dictionary<string, ProtoItem>>(Loader.GetProtoDatabase, "_protoItemsByName");
            for (int i = 0; i < instanceField.Count; i++)
            {
                GameItemList._Items.Add(i, instanceField.Values.ToList<ProtoItem>()[i]);
            }
        }

        public static ProtoItem GetProtoItemById(int id)
        {
            if (GameItemList._Items.TryGetValue(id, out ProtoItem result))
            {
                return result;
            }
            return null;
        }

        public static Dictionary<int, ProtoItem> _Items = new Dictionary<int, ProtoItem>();
    }
}