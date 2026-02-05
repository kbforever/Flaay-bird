using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    GameManager GameManager;

    private void Awake()
    {
        GameManager = GameManager.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        //GameManager GameManager = GameManager.GetComponentInChildren<GameManager>();
        //if(GameManager == null)
        //{
        //    GameObject gameManagerObj = new GameObject(nameof(GameManager));
        //    gameManagerObj.AddComponent<GameManager>();
        //    gameManagerObj.transform.SetParent(GameManager.transform, false);
        //}
    }

}
