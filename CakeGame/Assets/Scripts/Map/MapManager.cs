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
        string resourcePath = $"./Prefab/Choco";
        GameObject curCubePrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
        Instantiate(curCubePrefab, this.transform);
        //InitMap();
    }

    public void InitMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Debug.Log($"{i} {j}");

                AreaInfo newAreaInfo = new AreaInfo();
                mapInfo[i, j] = newAreaInfo;
                string resourcePath = $"./Prefab/{newAreaInfo.cubeType.ToString()}";
                GameObject curCubePrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
                Instantiate(curCubePrefab, this.transform);
            }
        }
    }
    
    
}
