using UnityEngine;
namespace TV.Core
{
    /// <summary>
    /// Base class of all singleton. Where T is Type Parameter, means class name.
    /// public class Myclass : Singleton<Myclass>{}
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _instanceLock = new object();
        private static bool _quitting = false;
        public static T Instance
        {
            get
            {

                if (_instance == null && !_quitting)
                {
                    lock (_instanceLock)
                    {
                        Debug.Log("here1");
                        _instance = GameObject.FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            GameObject go = new GameObject(typeof(T).ToString());
                            _instance = go.AddComponent<T>();
                            DontDestroyOnLoad(_instance.gameObject);
                            Debug.Log("here2");
                        }
                    }
                   
                }
                return _instance;
            }
        }
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = gameObject.GetComponent<T>();
                Debug.Log("here3");
            }
            else if (_instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
                throw new System.Exception(string.Format("Instance of {0} already exists, removing {1}", GetType().FullName, ToString()));
            }
        }
        protected virtual void OnApplicationQuit()
        {
            _quitting = true;
        }
    }
}
