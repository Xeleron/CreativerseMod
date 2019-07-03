using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace CreativerseMod.Helpers
{
    public static class Util
    {
        public static object GetInstanceField(Type type, object instance, string fieldName)
        {
            var bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                              BindingFlags.NonPublic;
            return type.GetField(fieldName, bindingAttr).GetValue(instance);
        }

        public static T GetInstanceField<T>(object instance, string fieldName)
        {
            return (T) GetInstanceField(instance, fieldName);
        }

        public static object GetInstanceField(object instance, string fieldName)
        {
            return GetInstanceField(instance.GetType(), instance, fieldName);
        }

        public static T GetInstanceField<T>(Type type, object instance, string fieldName)
        {
            return (T) GetInstanceField(type, instance, fieldName);
        }

        public static void SetInstanceField(Type type, object instance, string fieldName, object value)
        {
            var bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                              BindingFlags.NonPublic;
            type.GetField(fieldName, bindingAttr).SetValue(instance, value);
        }

        public static object GetStaticField(Type type, string fieldName)
        {
            var bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return type.GetField(fieldName, bindingAttr).GetValue(null);
        }

        public static void SetStaticField(Type type, string fieldName, object value)
        {
            var bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            type.GetField(fieldName, bindingAttr).SetValue(null, value);
        }

        public static void CallStaticMethod(Type type, string name, object[] args)
        {
            var bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            type.GetMethod(name, bindingAttr).Invoke(null, args);
        }

        public static void CallInstanceMethod(Type type, object instance, string name, object[] args)
        {
            var bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                              BindingFlags.NonPublic;
            type.GetMethod(name, bindingAttr).Invoke(instance, args);
        }

        public static T CallInstanceMethod<T>(Type type, object instance, string name, object[] args)
        {
            var bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                              BindingFlags.NonPublic;
            return (T) type.GetMethod(name, bindingAttr).Invoke(instance, args);
        }

        public static void SetProperty(object instance, string propertyName, object newValue)
        {
            instance.GetType().BaseType.GetProperty(propertyName).SetValue(instance, newValue, null);
        }

        public static void WrapMethod(object instance, MethodInfo m, string MethodName)
        {
            var ilasByteArray = instance.GetType().GetMethod(MethodName).GetMethodBody().GetILAsByteArray();
            var rgIL = GCHandle.Alloc(ilasByteArray, GCHandleType.Pinned).AddrOfPinnedObject();
            var methodSize = ilasByteArray.Length;
            MethodRental.SwapMethodBody(instance.GetType(), m.MetadataToken, rgIL, methodSize, 1);
        }

        private static bool HasSameSignature(MethodInfo a, MethodInfo b)
        {
            var flag = !a.GetParameters().Any(x => !b.GetParameters().Any(y => x == y));
            var flag2 = a.ReturnType == b.ReturnType;
            return flag && flag2;
        }

        public static void ForEach<T>(this T[] list, Action<T> action)
        {
            list.ToList().ForEach(action);
        }
    }
}