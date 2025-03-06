using UnityEngine;

namespace StairwayTest.Utilities
{
    public class SingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = GetComponent<T>();
            else
                Destroy(Instance);
        }
    }
}
