using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Singleton<T>
{
    private static T instance;

    private static bool applicationIsQuitting = false;

    public static T Instance 
    {
        get
        {
            if (instance == null)
            {
                if (applicationIsQuitting) return instance;
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }

            }
            return instance;
        }
        private set { }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;

            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
