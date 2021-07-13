using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shuttingDown = false;
    private static T instance = null;
    private static object locker = new object();
    public static T Instance
    {
        get
        {

            if (shuttingDown)
            {
                Debug.LogWarning("[MonoSingleton] Instance" + typeof(T) + "already destroyed. Returning null");
                return null;
            }

            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    GameObject temp = new GameObject(typeof(T).ToString());
                    instance = temp.AddComponent<T>();
                    //DontDestroyOnLoad(instance);
                }
            }

            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }

    private void OnDestroy()
    {
        shuttingDown = true;
    }
}
