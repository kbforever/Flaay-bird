using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameCoreManager : MonoBehaviour 
{
    //Camera camera;
    GameObject BgPrefab;
    GameObject ColumnPrefab;

    int spaceY = 2;

    private void Awake()
    {
        //if(camera == null)
        //{
        //    camera = Camera.main;
        //}
    }

    public void Init()
    {
        Debug.Log("≥ı ºªØ”Œœ∑");

        

        if(BgPrefab == null)
        {

            BgPrefab = Resources.Load<GameObject>("Prefabs/Bg");
            //var bg = Instantiate(BgPrefab,Vector3.zero,Quaternion.identity);
            
        }
        if(ColumnPrefab == null)
        {
            ColumnPrefab = Resources.Load<GameObject>("Prefabs/Column");
           
        }

        CreateOneMap(Vector3.zero);

    }
    private void Start()
    {
        
    }

    private void CreateOneMap(Vector3 pos)
    {
        GameObject Bg = Instantiate(BgPrefab,pos,Quaternion.identity);

        float spaceX = Bg.transform.localScale.x / 4;

        float HeightPadding = .5f;
        


        float ColWidth = ColumnPrefab.transform.localScale.x;
        float spacePosY = Random.Range(-Bg.transform.localScale.y + HeightPadding, Bg.transform.localScale.y - HeightPadding);
        float spacePosX = ColWidth / 2;
        float posUpY = (Bg.transform.localScale.y - (spacePosY + spaceY / 2)) / 2;
        float posDownY = ((spacePosY - spaceY / 2) - (-Bg.transform.localScale.y)) / 2;




        GameObject ColUp = Instantiate(ColumnPrefab);

        Vector3 ColUpPos = Bg.transform.position;
        ColUpPos.x = 0;
        ColUpPos.y = posUpY;
        ColUp.transform.localPosition = ColUpPos;

        var temp = ColUp.transform.localScale;
        temp.y = (Bg.transform.lossyScale.y - posUpY) / 2f;
        ColUp.transform.localScale = temp;


        GameObject ColDown = Instantiate(ColumnPrefab);

        Vector3 ColDownPos = Bg.transform.position;
        ColDownPos.x = 0;
        ColDownPos.y = posDownY;
        ColDown.transform.localPosition = ColDownPos;
    }




}
