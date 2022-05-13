using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{

    public static class Utility
    {
        public static void RemoveAtSwap<T>(this List<T> list, int index)
        {
            list[index] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }
    }

    [DisallowMultipleComponent]
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static Singleton<T> instance;
        public static T Instance => IsCreated ? (T)instance : new GameObject().AddComponent<T>();
        public static bool IsCreated => instance != null;

        protected virtual void Awake()
        {
            name = typeof(T).ToString();

            if (instance == this)
            {
                name += " (Singleton)";
            }
            else
            {
                name += " (Duplicated Singleton)";
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        protected Singleton()
        {
            if (instance != null)
            {
                Debug.LogWarning($"シングルトンのインスタンスが複数生成された: {typeof(T).ToString()}");
            }

            instance = this;
        }

        public void Destroy()
        {
            Destroy(gameObject);
            instance = null;
        }

    }

    public class Timer
    {
        protected float interval;
        protected float elapsed;

        public float Interval
        {
            get => interval;
            set
            {
                interval = value;
                UpdateProgress();
            }
        }
        public float Elapsed
        {
            get => elapsed;
            set
            {
                elapsed = value;
                UpdateProgress();
            }
        }
        public float Progress { get; protected set; }

        public bool IsComplete => Progress >= 1.0f;

        public bool IsStepEvenWhenPause { get; set; } = false;

        public Timer(float interval = 1.0f)
        {
            this.interval = interval;
        }

        public void Step()
        {
            Elapsed += IsStepEvenWhenPause ? Time.unscaledDeltaTime : Time.deltaTime;
        }

        public void Step(float speedScale)
        {
            Elapsed += (IsStepEvenWhenPause ? Time.unscaledDeltaTime : Time.deltaTime) * speedScale;
        }

        public void Reset()
        {
            Elapsed = 0.0f;
        }

        public void Reset(float interval)
        {
            this.interval = interval;
            Elapsed = 0.0f;
        }

        virtual protected void UpdateProgress()
        {
            Progress = elapsed / interval;
        }
        public static Timer operator ++(Timer timer)
        {
            timer.Step();
            return timer;
        }

        public static implicit operator float(Timer timer)
        {
            return timer.elapsed;
        }

        public static implicit operator bool(Timer timer)
        {
            return timer.IsComplete;
        }

    }

    public class Pool<T> : MonoBehaviour where T : Component, IPooledObject
    {
        protected GameObject prefab;

        protected int maxCount;

        protected Transform attachTransform;

        private List<T> list;

        protected virtual void Awake()
        {
            Transform parent = attachTransform ? attachTransform : transform;
            list = new List<T>(maxCount);
            for (var i = 0; i < maxCount; i++)
            {
                var t = Instantiate(prefab, parent).GetComponent<T>();
                if (t)
                {
                    t.IsUsing = false;
                    list.Add(t);
                }
            }
        }

        public T GetAvailablePoolObject()
        {
            foreach (var obj in list)
            {
                if (!obj.IsUsing)
                {
                    obj.IsUsing = true;
                    return obj;
                }
            }

            return null;
        }

        public T GetAvailablePoolObject(Transform parent)
        {
            var obj = GetAvailablePoolObject();
            if (obj != null)
            {
                obj.transform.parent = parent;
            }
            return obj;
        }

        public T GetAvailablePoolObject(Vector3 position, Quaternion rotation)
        {
            var obj = GetAvailablePoolObject();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
            }
            return obj;

        }

        public T GetAvailablePoolObject(Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = GetAvailablePoolObject();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.transform.parent = parent;
            }
            return obj;
        }

        public void DisableAllPoolObject()
        {
            foreach(var obj in list)
            {
                obj.IsUsing = false;
            }
        }
    }

    public interface IPooledObject
    {
        bool IsUsing { get; set; }
    }

    public class PooledMonoBehavior : MonoBehaviour, IPooledObject
    {
        private bool isUsing = true;
        public virtual bool IsUsing
        {
            get => isUsing;
            set
            {
                if (isUsing == value)
                {
                    return;
                }

                if (value)
                {
                    OnEnabled();
                }
                else
                {
                    OnDisabled();
                }

                gameObject.SetActive(value);
                isUsing = value;
            }
        }

        private void OnDestroy()
        {
            if (!IsUsing)
            {
                OnDisabled();
            }
        }

        public virtual void OnEnabled() { }

        public virtual void OnDisabled() { }
    }

    public struct Int2
    {
        public int x;
        public int y;

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Int2(Vector2 v)
        {
            x = (int)v.x;
            y = (int)v.y;
        }

        public static implicit operator Vector2(Int2 int2)
        {
            return new Vector2(int2.x, int2.y);
        }

        public static explicit operator Int2(Vector2 v)
        {
            return new Int2(v);
        }

        public static Int2 operator +(Int2 a, Int2 b)
        {
            return new Int2(a.x + b.x, a.y + b.y);
        }

        public static Int2 operator -(Int2 int2)
        {
            return new Int2(-int2.x, -int2.y);
        }

        public static Int2 operator -(Int2 a, Int2 b)
        {
            return a + (-b);
        }

        public static Int2 operator *(Int2 int2, int i)
        {
            return new Int2(int2.x * i, int2.y * i);
        }

        public static Vector2 operator *(Int2 int2, float f)
        {
            return new Vector2(int2.x * f, int2.y * f);
        }

        public static Int2 operator *(int i, Int2 int2)
        {
            return int2 * i;
        }

        public static Vector2 operator *(float f, Int2 int2)
        {
            return int2 * f;
        }

        public static Int2 operator /(Int2 a, int d)
        {
            return new Int2(a.x / d, a.y / d);
        }

        public static Vector2 operator /(Int2 a, float d)
        {
            return new Vector2(a.x / d, a.y / d);
        }

        public static bool operator ==(Int2 lhs, Int2 rhs)
        {
            return lhs.x == rhs.x && lhs.x == rhs.y;
        }

        public static bool operator !=(Int2 lhs, Int2 rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return (Int2)obj == this;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return (x << 2) ^ y;
        }

    }
}
