using UnityEngine;

namespace TastyCore.Utils
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        // TODO - Rewrite
        
        private static T _instance;
        private static bool _isQuit;

        public void DontDestroyOnLoad()
        {
            DontDestroyOnLoad(this);
        }

        public static T Instance
        {
            get
            {
                if (_instance == null && !_isQuit)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance != null)
                        return _instance;

                    //var go = new GameObject(typeof(T).Name);
                    //_instance = go.AddComponent<T>();
                }

                return _instance;
            }
        }

        public static bool IsCreated
        {
            get { return _instance != null; }
        }

        protected virtual void Awake()
        {
            if (!IsCreated)
            {
                _isQuit = false;
                _instance = Instance;
            }
        }

        protected virtual void OnDestroy()
        {
            _isQuit = true;
            _instance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            _isQuit = true;
        }
    }
}