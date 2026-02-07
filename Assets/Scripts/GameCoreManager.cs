using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameCoreManager : MonoBehaviour 
{
    MapManager mapManager;
    

    private void Awake()
    {
        if(mapManager == null)
        {
            mapManager = transform.GetComponentInChildren<MapManager>();
            if (mapManager == null)
            {
                GameObject obj = new GameObject(nameof(MapManager));
                mapManager = obj.AddComponent<MapManager>();
                mapManager.transform.parent = transform;
            }
        }
       
    }


    private void Start()
    {
        
    }

    public void Init()
    {
        mapManager.Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.IsStartGame?.Invoke(true);
        }
    }

    private void LateUpdate()
    {
        
    }



}
