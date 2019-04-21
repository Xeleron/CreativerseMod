using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CreativerseMod.Helpers
{
    public static class Util
    {
        public static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return type.GetField(fieldName, bindingAttr).GetValue(instance);
        }

        public static T GetInstanceField<T>(object instance, string fieldName)
        {
            return (T)((object)Util.GetInstanceField(instance, fieldName));
        }

        public static object GetInstanceField(object instance, string fieldName)
        {
            return Util.GetInstanceField(instance.GetType(), instance, fieldName);
        }

        public static T GetInstanceField<T>(Type type, object instance, string fieldName)
        {
            return (T)((object)Util.GetInstanceField(type, instance, fieldName));
        }

        public static void SetInstanceField(Type type, object instance, string fieldName, object value)
        {
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            type.GetField(fieldName, bindingAttr).SetValue(instance, value);
        }

        public static object GetStaticField(Type type, string fieldName)
        {
            BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return type.GetField(fieldName, bindingAttr).GetValue(null);
        }

        public static void SetStaticField(Type type, string fieldName, object value)
        {
            BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            type.GetField(fieldName, bindingAttr).SetValue(null, value);
        }

        public static void CallStaticMethod(Type type, string name, object[] args)
        {
            BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            type.GetMethod(name, bindingAttr).Invoke(null, args);
        }

        public static void CallInstanceMethod(Type type, object instance, string name, object[] args)
        {
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            type.GetMethod(name, bindingAttr).Invoke(instance, args);
        }

        public static T CallInstanceMethod<T>(Type type, object instance, string name, object[] args)
        {
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return (T)((object)type.GetMethod(name, bindingAttr).Invoke(instance, args));
        }

        public static void SetProperty(object instance, string propertyName, object newValue)
        {
            instance.GetType().BaseType.GetProperty(propertyName).SetValue(instance, newValue, null);
        }

        public static void WrapMethod(object instance, MethodInfo m, string MethodName)
        {
            byte[] ilasByteArray = instance.GetType().GetMethod(MethodName).GetMethodBody().GetILAsByteArray();
            IntPtr rgIL = GCHandle.Alloc(ilasByteArray, GCHandleType.Pinned).AddrOfPinnedObject();
            int methodSize = ilasByteArray.Length;
            MethodRental.SwapMethodBody(instance.GetType(), m.MetadataToken, rgIL, methodSize, 1);
        }

        private static bool HasSameSignature(MethodInfo a, MethodInfo b)
        {
            bool flag = !a.GetParameters().Any((ParameterInfo x) => !b.GetParameters().Any((ParameterInfo y) => x == y));
            bool flag2 = a.ReturnType == b.ReturnType;
            return flag && flag2;
        }

        public static void ForEach<T>(this T[] list, Action<T> action)
        {
            list.ToList<T>().ForEach(action);
        }
    }
}
