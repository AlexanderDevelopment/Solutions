using UnityEngine;

namespace _src.Scripts.Utils
{
    public static class RoutineWork
    {
        public static void InitializeComponentFromGameObject<T>(GameObject gameObject, ref T component) where T : Component
        {
            if (component != null)
            {
                Debug.LogWarning($"Component of type {typeof(T)} already received");
            }

            component = gameObject.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"Component of type {typeof(T)} not found in {gameObject.name}", gameObject);
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

        public static bool CheckForNull<T>(ref T component) where T : Component
        {
            if (component)
                return true;
            Debug.LogError($"{component} is null");
            return false;
        }
    }
}
