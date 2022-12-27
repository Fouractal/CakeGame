using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    public static int row = 10;
    public static int col = 10;
    
    public AreaInfo[,] mapInfo = new AreaInfo[10, 10];

    public void Awake()
    {
        InitMap();
    }

    public void InitMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                AreaInfo newAreaInfo = new AreaInfo();
                // CubeType에 랜덤 지정
                newAreaInfo.cubeType = (Define.CubeType)(UnityEngine.Random.Range(0,
                    System.Enum.GetValues(enumType: typeof(Define.CubeType)).Length));
                Debug.Log(newAreaInfo.cubeType);
                mapInfo[i, j] = newAreaInfo;
                string resourcePath = $"Prefab/{newAreaInfo.cubeType.ToString()}";
                GameObject curCubePrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
                Instantiate(curCubePrefab,new Vector3(i * curCubePrefab.transform.localScale.x, 0,
                    j * curCubePrefab.transform.localScale.x),Quaternion.identity);
            }
        }
    }
    
    
}
