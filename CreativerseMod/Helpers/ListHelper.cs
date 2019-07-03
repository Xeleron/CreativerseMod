using System;
using System.Collections.Generic;

namespace CreativerseMod.Helpers
{
    public static class ListHelper
    {
        public static T PickRandom<T>(this List<T> List)
        {
            var random = new Random();
            return List[random.Next(List.Count)];
        }
    }
}