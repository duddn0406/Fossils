using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    private static bool _isInitialized = false;

    public static T instance
    {
        get
        {
            if (_isInitialized)
            {
                return _instance;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }

                    _isInitialized = true;
                    return _instance;
                }
            }

            return _instance;
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _isInitialized = false;
        }
    }
}

