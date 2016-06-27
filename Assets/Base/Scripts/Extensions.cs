using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Assets.Shared.Scripts
{
    public static class Extensions
    {
        public static GameObject Instantiate(this GameObject gameObject)
        {
            return Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        }

        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
        {
            var x = Mathf.Clamp(vector.x, min.x, max.x);
            var y = Mathf.Clamp(vector.y, min.y, max.y);
            var z = Mathf.Clamp(vector.z, min.z, max.z);
            return new Vector3(x, y, z);
        }

        public static Vector3 ClampNegative(this Vector3 vector)
        {
            var x = clampNegative(vector.x);
            var y = clampNegative(vector.y);
            var z = clampNegative(vector.z);
            return new Vector3(x, y, z);
        }

        public static Vector3 FloorNegative(this Vector3 vector)
        {
            var x = FloorNegative(vector.x);
            var y = FloorNegative(vector.y);
            var z = FloorNegative(vector.z);
            return new Vector3(x, y, z);
        }

        public static void SetLocalX(this Transform transform, float x)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        public static void SetLocalY(this Transform transform, float y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        public static void SetLocalZ(this Transform transform, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }

        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public static void SetZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        public static Vector3 SetX(this Vector3 vector3, float x)
        {
            return new Vector3(x, vector3.y, vector3.z);
        }

        public static Vector3 SetY(this Vector3 vector3, float y)
        {
            return new Vector3(vector3.x, y, vector3.z);
        }

        public static Vector2 SetX(this Vector2 vector2, float x)
        {
            return new Vector2(x, vector2.y);
        }

        public static Vector2 SetY(this Vector2 vector2, float y)
        {
            return new Vector2(vector2.x, y);
        }

        public static Vector3 SetZ(this Vector3 vector3, float z)
        {
            return new Vector3(vector3.x, vector3.y, z);
        }

        public static void AddX(this Transform transform, float x)
        {
            transform.position += new Vector3(x, 0, 0);
        }

        public static void AddY(this Transform transform, float y)
        {
            transform.position += new Vector3(0, y, 0);
        }

        public static void AddZ(this Transform transform, float z)
        {
            transform.position += new Vector3(0, 0, z);
        }

        public static Vector3 Divide(this Vector3 vectorA, Vector3 vectorB)
        {
            return new Vector3(vectorA.x/vectorB.x, vectorA.y/vectorB.y, vectorA.z/vectorB.z);
        }

        public static Vector3 Multiply(this Vector3 vectorA, Vector3 vectorB)
        {
            return Vector3.Scale(vectorA, vectorB);
        }

        public static Vector2 Multiply(this Vector2 vectorA, Vector2 vectorB)
        {
            return Vector2.Scale(vectorA, vectorB);
        }

        public static float Distance(this Vector3 vectorA, Vector3 vectorB)
        {
            return Vector3.Distance(vectorA, vectorB);
        }

        public static Vector3 Min(this Vector3 vectorA, Vector3 vectorB)
        {
            return Vector3.Min(vectorA, vectorB);
        }

        public static Vector3 Max(this Vector3 vectorA, Vector3 vectorB)
        {
            return Vector3.Min(vectorA, vectorB);
        }

        public static Vector2 Min(this Vector2 vectorA, Vector2 vectorB)
        {
            return Vector2.Min(vectorA, vectorB);
        }

        public static Vector2 Max(this Vector2 vectorA, Vector2 vectorB)
        {
            return Vector2.Min(vectorA, vectorB);
        }

        public static Vector2 FromXY(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector2 FromXZ(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        public static Vector2 FromYZ(this Vector3 vector)
        {
            return new Vector2(vector.y, vector.z);
        }

        public static Vector3 ToXy(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }

        public static Vector3 ToXZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static Vector3 ToYZ(this Vector2 vector)
        {
            return new Vector3(0, vector.x, vector.y);
        }

        public static Vector2 Normal(this Vector2 vector)
        {
            return new Vector2(-vector.y, vector.x);
        }

        public static Vector3 LockToAxis(this Vector3 vector, Vector3 lockAxis)
        {
            return (vector.Multiply(lockAxis)).normalized;
        }
        public static Vector3 HalfWay(this Vector3 vector, Vector3 anotherVector)
        {
            return Vector3.Lerp(vector, anotherVector, 0.5f);
        }

        private static float clampNegative(float value)
        {
            return value >= 0 ? Mathf.Ceil(value) : Mathf.Floor(value);
        }

        private static float FloorNegative(float value)
        {
            return value >= 0 ? Mathf.Floor(value) : Mathf.Ceil(value);
        }

        /// <summary>
        /// ForEach extension for all <link>IEnumerable</link>
        /// </summary>
        /// <typeparam name="T">The type of objects to enumerate.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
        /// <param name="source">Source IEnumerable that will be traversed</param>
        /// <param name="action">Action that will be invoked on each item</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.ThrowIfNull("source");
            action.ThrowIfNull("action");
            foreach (var element in source)
            {
                action(element);
            }
        }

        /// <summary>
        /// Check an object whether it is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">An object that will be checked</param>
        /// <param name="paramName">Custom message that will thrown as exception in case the object is null</param>
        public static void ThrowIfNull<T>(this T o, string paramName) where T : class
        {
            if (o == null)
                throw new ArgumentNullException(paramName);
        }

        public static string Join(this IEnumerable<String> enumerable, string delimiter)
        {
            return string.Join(delimiter, enumerable.ToArray());
            //Since unity does not support full SetLocal of C# 4.0 features, we can't use String.Join Method (String, IEnumerable<String>)
        }

        public static Color ToColor(this int rgb)
        {
            return ((uint) (rgb << 8) | 0xff).ToColor();
        }

        public static Color ToColor(this uint rgba)
        {
            return new Color32(
                (byte) ((rgba & 0xff000000) >> 24),
                (byte) ((rgba & 0xff0000) >> 16),
                (byte) ((rgba & 0xff00) >> 8),
                (byte) ((rgba & 0xff) >> 0)
                );
        }

        public static uint ToUint(this Color color)
        {
            return (uint)(
                ((byte)(color.a * 255) << 24) |
                ((byte)(color.b * 255) << 16) |
                ((byte)(color.g * 255) << 8) |
                ((byte)(color.r * 255) << 0)
                );
        }

        public static int ToInt(this Color color)
        {
            return ((byte)(color.b * 255) << 16) |
                   ((byte)(color.g * 255) << 8) |
                   ((byte)(color.r * 255) << 0);
        }

        public static Color TransformHSV(
            this Color color, // color to transform
            float H, // hue shift (in degrees)
            float S, // saturation multiplier (scalar)
            float V // value multiplier (scalar)
            )
        {
            var VSU = V*S*Math.Cos(H*Mathf.PI/180);
            var VSW = V*S*Math.Sin(H*Mathf.PI/180);

            return new Color
            {
                r = (float) ((.299*V + .701*VSU + .168*VSW)*color.r
                             + (.587*V - .587*VSU + .330*VSW)*color.g
                             + (.114*V - .114*VSU - .497*VSW)*color.b),
                g = (float) ((.299*V - .299*VSU - .328*VSW)*color.r
                             + (.587*V + .413*VSU + .035*VSW)*color.g
                             + (.114*V - .114*VSU + .292*VSW)*color.b),
                b = (float) ((.299*V - .3*VSU + 1.25*VSW)*color.r
                             + (.587*V - .588*VSU - 1.05*VSW)*color.g
                             + (.114*V + .886*VSU - .203*VSW)*color.b),
                a = 1
            };
        }

        public static void RemoveAllChildren(this Transform transform)
        {
#if UNITY_EDITOR
            while (transform.childCount > 0)  Object.DestroyImmediate(transform.GetChild(0).gameObject);
#else
            foreach (Transform child in transform) Object.Destroy(child.gameObject);
#endif
        }

        public static IEnumerable<Transform> Children(this Transform transform)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                yield return transform.GetChild(i);
            }
        }

        public static void Submit(this GameObject gameObject)
        {
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.submitHandler);
        }

        public static bool Empty<T>(this List<T> list)
        {
            return list.Count == 0;
        }

        public static Vector3? PointOnGround(this Camera camera, Vector2 screenPosition, Plane ground)
        {
            var ray = camera.ScreenPointToRay(screenPosition);
            float distance;
            if (ground.Raycast(ray, out distance))
            {
                return ray.GetPoint(distance);
            }
            return null;
        }

        public static Vector3 Center(this IEnumerable<Vector3> points)
        {
            return points.Any() ? points.Aggregate((a, b) => a + b) / points.Count() : Vector3.zero;
        }

        public static Rect ToUvRect(this Sprite sprite)
        {
            var uv = sprite.rect;
            uv.x /= sprite.texture.width;
            uv.width /= sprite.texture.width;
            uv.y /= sprite.texture.height;
            uv.height /= sprite.texture.height;
            return uv;
        }

        public static Vector2[] ToUvs(this Sprite sprite)
        {
            var rect = sprite.ToUvRect();
            return new[]
            {
                new Vector2(rect.xMin, rect.yMin),
                new Vector2(rect.xMax, rect.yMin),
                new Vector2(rect.xMax, rect.yMax),
                new Vector2(rect.xMin, rect.yMax)
            };
        }

        public static void ResetTransform(this Transform child)
        {
            child.localScale = Vector3.one;
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// Wraps this object instance into an IEnumerable&lt;T&gt;
        /// consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}