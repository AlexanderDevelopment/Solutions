using UnityEngine;

namespace _src.Scripts.Utils
{
    public static class RoutineWork
    {
        public static void InitializeComponentFromGameObject<T>(GameObject parent, ref T component) where T : Component
        {
            if (component != null)
            {
                Debug.LogWarning($"Component of type {typeof(T)} already received");
            }

            component = parent.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"Component of type {typeof(T)} not found in {parent.name}", parent);
            }
        }

        public static void InitializeComponentFromChildren<T>(GameObject parent, ref T component) where T : Component
        {
            if (component != null)
            {
                Debug.LogWarning($"Component of type {typeof(T)} already received");
            }

            component = parent.GetComponentInChildren<T>();
            if (component == null)
            {
                Debug.LogError($"Component of type {typeof(T)} not found in children of {parent.name}", parent);
            }
        }

        public static void CheckForNull<T>(ref T component) where T : Component
        {
            if (component)
                return;
            Debug.LogError($"{component} is null");
        }
    }
}
