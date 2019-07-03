using System.Collections.Generic;
using System.Linq;
using BuildVerse;

namespace CreativerseMod.Helpers
{
    public static class GameItemList
    {
        public static Dictionary<int, ProtoItem> _Items = new Dictionary<int, ProtoItem>();

        static GameItemList()
        {
            var instanceField =
                Util.GetInstanceField<Dictionary<string, ProtoItem>>(Loader.GetProtoDatabase, "_protoItemsByName");
            for (var i = 0; i < instanceField.Count; i++) _Items.Add(i, instanceField.Values.ToList()[i]);
        }

        public static ProtoItem GetProtoItemById(int id)
        {
            if (_Items.TryGetValue(id, out var result)) return result;
            return null;
        }
    }
}