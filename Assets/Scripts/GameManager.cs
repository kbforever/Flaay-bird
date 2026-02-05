using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameCoreManager coreManager;

    private void Awake()
    {
        if (coreManager == null)
        {
            coreManager = transform.GetComponentInChildren<GameCoreManager>();
            if (coreManager == null)
            {
                GameObject obj = new GameObject(nameof(GameCoreManager));
                coreManager = obj.AddComponent<GameCoreManager>();
                coreManager.transform.parent = transform;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coreManager.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
