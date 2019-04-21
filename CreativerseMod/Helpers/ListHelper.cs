using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativerseMod.Helpers
{
    public static class ListHelper
    {
        public static T PickRandom<T>(this List<T> List)
        {
            Random random = new Random();
            return List[random.Next(List.Count)];
        }
    }
}
