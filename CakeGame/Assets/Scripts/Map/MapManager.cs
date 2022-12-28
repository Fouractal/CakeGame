using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    public GameObject gameMap;
    
    public static int row = 10;
    public static int col = 10;
    public AreaInfo[,] mapInfo = new AreaInfo[10, 10];

    public void Awake()
    {
        gameMap = GameObject.Find("GameMap");
        InitMap();
        StartCoroutine(RandomAiming());
    }

    private void Update()
    {
        
    }

    public void InitMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                AreaInfo newAreaInfo = new AreaInfo();
                mapInfo[i, j] = newAreaInfo;
                
                // CubeType에 랜덤 지정
                newAreaInfo.cubeType = (Define.CubeType)(UnityEngine.Random.Range(0, System.Enum.GetValues(enumType: typeof(Define.CubeType)).Length));
                
                string resourcePath = $"Prefab/Cake/{newAreaInfo.cubeType.ToString()}";
                GameObject curCubePrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);

                GameObject curObject = Instantiate(curCubePrefab,new Vector3(i * Define.STANDARD_DISTANCE, 0, j * Define.STANDARD_DISTANCE),Quaternion.identity, gameMap.transform);
                newAreaInfo.cube = curObject.GetComponent<Cube>();

            }
        }
        
        gameMap.transform.Rotate(0, -60, 0);
    }

    public IEnumerator RandomAiming()
    {
        while (GameManager.instance.onPlay)
        {
            if (!GameManager.instance.onPlay)
            {
                Debug.Log("StopCoroutine");
                StopCoroutine(RandomAiming());
            }
            
            Debug.Log("InCoroutine");
            
            // mapInfo[i, j] 중 랜덤 선택
            int i = Random.Range(0, row);
            int j = Random.Range(0, col);
            Debug.Log($"{i}, {j}");
            Debug.Log(mapInfo[i, j].cubeType);
            Debug.Log(mapInfo[i, j].cube);
            Debug.Log(mapInfo[i, j].cube.cubeState);
            mapInfo[i, j].cube.cubeState = Define.CubeState.beAimed;
            Debug.Log(mapInfo[i, j].cube.cubeState);
            
            mapInfo[i, j].isAimed = true;
            
            yield return new WaitForSeconds(Random.Range(4f,7f));
            mapInfo[i, j].cube.cubeState = Define.CubeState.Idle;
            mapInfo[i, j].cube.ChangeOwnColor();
        }
        //yield return new WaitForSeconds(Random.Range(2f, 4f));
        
        // 시간 지나면 cubeState를 Idle, destroyed로 바꿔야 함

    }
}
