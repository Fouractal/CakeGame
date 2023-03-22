using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    // Map에 대한 Data들을 관리하고 Return 합니다.
    public GameObject gameMap;

    public static int row = 10;
    public static int col = 10;
    public AreaInfo[,] mapInfo = new AreaInfo[10, 10];

    [SerializeField]
    public AreaInfo newAreaInfo;
    [SerializeField]
    public List<AreaInfo> availableAreaList = new List<AreaInfo>(); // 활성화된 발판
    [SerializeField]
    public List<AreaInfo> destroyedAreaList = new List<AreaInfo>(); // 제거된 발판

    public float totalDuration;
    
    
    #region Return Map Data
    public List<AreaInfo> GetAvailableAreaList()
    {
        return availableAreaList;
    }

    public AreaInfo GetRandomAvailableArea()
    {
        int randomIndex = Random.Range(0, availableAreaList.Count);

        return availableAreaList[randomIndex];
    }
    
    #endregion

    #region Manage Map Data

    public void InitMap()
    {
        gameMap = GameObject.Find("GameMap");
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                newAreaInfo = new AreaInfo();
                
                // rowIndex, columnIndex 지정
                newAreaInfo.rowIndex = i;
                newAreaInfo.columnIndex = j;
                
                // CubeType 랜덤 지정
                newAreaInfo.cubeType = (Define.CubeType)(UnityEngine.Random.Range(0, System.Enum.GetValues(enumType: typeof(Define.CubeType)).Length));
                
                string resourcePath = $"Prefab/Cake/{newAreaInfo.cubeType.ToString()}";
                GameObject curCubePrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
                GameObject curObject = Instantiate(curCubePrefab,new Vector3(i * Define.STANDARD_DISTANCE, 0, j * Define.STANDARD_DISTANCE),Quaternion.identity, gameMap.transform);
                newAreaInfo.cube = curObject.GetComponentInChildren<Cube>();

                // 전체 맵에 대한 정보 입력
                mapInfo[i, j] = newAreaInfo;
                // 사용가능한 맵에 정보 입력
                availableAreaList.Add(newAreaInfo);
            }
        }
        gameMap.transform.Rotate(0, -60, 0);
        Debug.Log("맵생성완료");
    }

    public IEnumerator RandomAiming()
    {
        while (GameManager.Instance.onPlay)
        {
            if (!GameManager.Instance.onPlay)
            {
                Debug.Log("StopCoroutine");
                StopCoroutine(RandomAiming());
            }

            Debug.Log("InCoroutine");

            // mapInfo[i, j] 중 랜덤 선택
            int i = Random.Range(0, row);
            int j = Random.Range(0, col);
            //mapInfo[i, j].cube.cubeState = Define.CubeState.beAimed;
            
            Debug.Log(mapInfo[i, j].cube.cubeState);

            mapInfo[i, j].isAimed = true;

            yield return new WaitForSeconds(Random.Range(4f, 7f));
            mapInfo[i, j].cube.cubeState = Define.CubeState.Idle;
            //mapInfo[i, j].cube.ChangeOwnColor();
        }
        //yield return new WaitForSeconds(Random.Range(2f, 4f));

        // 시간 지나면 cubeState를 Idle, destroyed로 바꿔야 함
    }

    public void SpwanFork(int i, int j)
    {
        // 플레이 - ForkFactory.ForkSpawnRoutine - SpawnFork(targetArea)
        mapInfo[i,j].cube.BlinkAll();
        Cube targetCube = mapInfo[i, j].cube;
        
        string resourcePath = $"Prefab/Enemy/Fork";
        GameObject forkPrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
        Fork fork = Instantiate(forkPrefab, targetCube.transform.position + Vector3.up * 6,
            Quaternion.identity, GameObject.Find("Object").transform).GetComponentInChildren<Fork>();
        fork._pickedCake = targetCube.parent.transform; // 케이크 위치를 Fork로 넘겨줌
        fork.totalDuration = totalDuration;
        fork.pickedCakeIndexI = i;
        fork.pickedCakeIndexJ = j;


        // 포크 작업은 포크 팩토리에서!
        //포크 스폰 -> 일정 시간 뒤 찍음 -> 일정 시간 대기 -> 케이크 뽑아감 -> 빈 케이크 리스트에 추가

    }

    public void CakeFadeOut(int i, int j)
    {
        //Debug.Log($"CakeFadeOuti : {i}, j : {j}");
        mapInfo[i, j].cube.FadeOutAll();
    }

    public void RefillCake(int i, int j)
    {
        mapInfo[i,j].cube.SetActiveTrue();
        mapInfo[i, j].cube.cubeState = Define.CubeState.Idle;
        mapInfo[i,j].cube.FadeInAll();
    }
    public void RemoveFromAvailableList(AreaInfo areaInfo)
    {
        availableAreaList.Remove(areaInfo);
    }

    public void MarkFriendInfo(int i, int j, Define.FriendType targetType)
    {
        AreaInfo targetArea = mapInfo[i, j];
        targetArea.friendType = targetType;
    }

    #endregion


}
