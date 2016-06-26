using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.utils
{
    internal class Utils
    {
        public static T loadResource<T>(string name) where T : Object
        {
            return (T) Object.Instantiate(Resources.Load<T>(name));
        }

        public static IEnumerable<T> getEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T Values<T>(string enumName) where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum) return default(T); // or throw exception

            var enums = Enum.GetValues(type).Cast<T>().ToList();
            return enums.FirstOrDefault(e => e.ToString() == enumName);
        }
    }
}