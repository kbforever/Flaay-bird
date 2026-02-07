using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{



    bool isPlayingGame;
    
    
    //Prefabs
    GameObject BgPrefab; //背景预制体
    GameObject ColumnPrefab; // 管道预制体
    GameObject BirdPrefab; // 鸟预制体


    List<GameObject> BgList;

    float spaceY = 2.5f; // 中间空洞的长度


    int BgNum = 2; // 地图初始化的数量

    Vector3 BgSpaceX; // Bg之间的间隔距离

    Vector3 movedir = Vector3.left;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float moveLimitX;

    private void Awake()
    {
        BgList = new List<GameObject>();
        GameManager.Instance.IsStartGame += SetIsPlayingGame;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsStartGame -= SetIsPlayingGame;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    public void Init()
    {
        Debug.Log("初始化游戏");



        if (BgPrefab == null)
        {

            BgPrefab = Resources.Load<GameObject>($"Prefabs/{nameof(BgPrefab)}");
            //var bg = Instantiate(BgPrefab,Vector3.zero,Quaternion.identity);

        }
        if (ColumnPrefab == null)
        {
            ColumnPrefab = Resources.Load<GameObject>($"Prefabs/{nameof(ColumnPrefab)}");

        }

        if (BirdPrefab == null)
        {
            BirdPrefab = Resources.Load<GameObject>($"Prefabs/{nameof(BirdPrefab)}");
        }


        moveLimitX = -BgPrefab.transform.localScale.x;

        BgSpaceX = new Vector3(BgPrefab.transform.localScale.x, 0, 0);

        GameObject BirdObj = Instantiate(BirdPrefab, Vector3.zero, Quaternion.identity);
        BirdObj.AddComponent<Bird>();


        Vector3 originPos = Vector3.zero;
        for (int i = 0; i < BgNum; i++)
        {
            BgList.Add(CreateOneMap(originPos + i * BgSpaceX,i==0));
        }
        isPlayingGame = false;
    }

    private GameObject CreateOneMap(Vector3 pos,bool IsFirst=false)
    {
        GameObject Bg = Instantiate(BgPrefab, pos, Quaternion.identity);
        Bg.transform.SetParent(this.transform);
        if (IsFirst) return Bg;

        int Num = 2;
        float padding = 2f;
        float spaceX = (Bg.transform.localScale.x - ColumnPrefab.transform.localScale.x - padding * 2) / (Num - 1);
        float originX = -Bg.transform.localScale.x / 2 + ColumnPrefab.transform.localScale.x / 2 + padding;
        for (int i = 0; i < Num; i++)
        {
            InitColumn(Bg, i * spaceX + originX);
        }

        

        return Bg;
    }

    private void InitColumn(GameObject Bg, float x)
    {



        float HeightPadding = 1.5f;

        float ColWidth = ColumnPrefab.transform.localScale.x;
        float spacePosY = Random.Range(-Bg.transform.localScale.y / 2 + HeightPadding, Bg.transform.localScale.y / 2 - HeightPadding);

        float spacePosX = ColWidth / 2;
        float posUpY = (Bg.transform.localScale.y / 2 - (spacePosY + spaceY / 2)) / 2 + spacePosY + spaceY / 2;
        float posDownY = spacePosY - spaceY / 2 - (spacePosY - (-Bg.transform.localScale.y / 2) - spaceY / 2) / 2;



        GameObject ColUp = Instantiate(ColumnPrefab);
        float posUpX = -Bg.transform.localScale.x / 2 + ColUp.transform.localScale.x / 2;




        Vector3 ColUpPos = Bg.transform.position;
        ColUpPos.x += x;
        ColUpPos.y += posUpY;
        ColUp.transform.localPosition = ColUpPos;

        var temp = ColUp.transform.localScale;
        temp.y = (Bg.transform.localScale.y / 2 - ColUpPos.y) * 2;
        ColUp.transform.localScale = temp;

        //ColUp.transform.SetParent(Bg.transform);

        GameObject ColDown = Instantiate(ColumnPrefab);
        float posDownX = -Bg.transform.localScale.x / 2 + ColDown.transform.localScale.x / 2;
        Vector3 ColDownPos = Bg.transform.position;
        ColDownPos.x += x;
        ColDownPos.y += posDownY;
        ColDown.transform.localPosition = ColDownPos;

        temp = ColDown.transform.localScale;
        temp.y = (ColDownPos.y - (-Bg.transform.localScale.y / 2)) * 2;
        ColDown.transform.localScale = temp;

        ColUp.transform.SetParent(Bg.transform);
        ColDown.transform.SetParent(Bg.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayingGame) return;


        MoveMaps();

        CheckAndDestory();
    }


    // 检测是否超出，超出删除物体，并创立新的物体
    void CheckAndDestory()
    {
        var obj = BgList[0];
        if (obj.transform.position.x < moveLimitX)
        {
            BgList.RemoveAt(0);
            Destroy(obj);
            var objNew = BgList[BgList.Count - 1];
            BgList.Add(CreateOneMap(objNew.transform.position + BgSpaceX));
        }
    }

    // 移动所有地图块
    void MoveMaps()
    {
        foreach (var obj in BgList)
        {

            obj.transform.Translate(movedir * Time.deltaTime * moveSpeed);
        }
    }


    void SetIsPlayingGame(bool isStartGame)
    {
        isPlayingGame = isStartGame; 
    }
}
