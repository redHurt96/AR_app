using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] instancies = (T[])FindObjectsOfType(typeof(T));

                if (instancies.Length == 0)
                {
                    Debug.LogError($"Нет ни одного экземпляра {typeof(T)} на сцене!");
                    return null;
                }
                else if (instancies.Length > 1)
                {
                    Debug.LogError($"На сцене больше одного экземпляра {typeof(T)}");
                    return null;
                }
                else
                {
                    instance = instancies[0];
                    return instance;
                }
            }
            else return instance;
        }
    }

    private void Awake()
    {
        if (instance != this && instance != null) Destroy(this);
        else
        {
            T[] instancies = (T[])FindObjectsOfType(typeof(T));

            if (instancies.Length == 0)
                Debug.LogError($"Нет ни одного экземпляра {typeof(T)} на сцене!");
            else if (instancies.Length > 1)
                Debug.LogError($"На сцене больше одного экземпляра {typeof(T)}");
            else
                instance = instancies[0];
        }
    }
}
