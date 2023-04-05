using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //UnityChan取得
    private GameObject unitychan;
    //Zの位置保存
    private float uPositionZ;
    //アイテムの出現範囲(50m先)
    private float itemGenerationDistance = 50f;
    //アイテムの出現位置
    private float nextItemGenerationPos;

    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
        //Unityちゃんのz一の情報を取得
        this.uPositionZ = unitychan.transform.position.z;
        //次のアイテムの出現位置をユニティちゃんの位置＋itemGenerationDistance分の位置にする
        this.nextItemGenerationPos = uPositionZ + itemGenerationDistance;

        // 最初のアイテムを生成
        for (int i = startPos; i < uPositionZ + itemGenerationDistance; i += 15)
        {
            GenerateItems(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        uPositionZ = unitychan.transform.position.z;

        if (uPositionZ > nextItemGenerationPos - itemGenerationDistance)
        {
            GenerateItems(nextItemGenerationPos);
            nextItemGenerationPos += 15;
        }

        RemoveItemsBehindUnityChan();

    }


    void GenerateItems(float zPosition)
    {
        //どのアイテムを出すのかをランダムに設定
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            for (float j = -1; j <= 1; j += 0.4f)
            {
                //コーンをx軸方向に一直線に生成
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, zPosition);
            }
        }
        else
        {
            //レーンごとにアイテムを生成
            for (int j = -1; j <= 1; j++)
            {
                //アイテムの種類を決める
                int item = Random.Range(1, 11);
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (1 <= item && item <= 6)
                {
                    //コインを生成
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, zPosition + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    //車を生成
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, zPosition + offsetZ);
                }
            }
        }
    }

    void RemoveItemsBehindUnityChan()
    {
        float unityChanZPosition = unitychan.transform.position.z;
        // Unityちゃんよりも後ろの距離
        float removeDistance = 10f; 

        // シーン内のそれぞれのタグつきオブジェクトを全て取得
        GameObject[] cars = GameObject.FindGameObjectsWithTag("CarTag");
        GameObject[] coins = GameObject.FindGameObjectsWithTag("CoinTag");
        GameObject[] cones = GameObject.FindGameObjectsWithTag("TrafficConeTag");

        // CarTagのオブジェクトを削除
        foreach (GameObject car in cars)
        {
            if (car.transform.position.z < unityChanZPosition - removeDistance)
            {
                Destroy(car);
            }
        }

        // CoinTagのオブジェクトを削除
        foreach (GameObject coin in coins)
        {
            if (coin.transform.position.z < unityChanZPosition - removeDistance)
            {
                Destroy(coin);
            }
        }

        // TrafficConeTagのオブジェクトを削除
        foreach (GameObject cone in cones)
        {
            if (cone.transform.position.z < unityChanZPosition - removeDistance)
            {
                Destroy(cone);
            }
        }
    }




}