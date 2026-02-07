using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameCoreManager coreManager;

    public Action<bool> IsStartGame;

    

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

    public void GameOver()
    {
        Debug.LogError("Game Over");
        Time.timeScale = 0f;
    }
}
