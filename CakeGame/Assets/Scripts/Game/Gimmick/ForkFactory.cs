using System.Collections;
using UnityEngine;

public class ForkFactory : SingletonMonoBehaviour<ForkFactory>
{
    private void Awake()
    {
        
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        yield return ForkSpawnRoutine();
    }
    public void StartForkSpawnRoutine()
    {
        StartCoroutine(ForkSpawnRoutine());
    }

    public void PauseForkSpawnRoutine()
    {
        
    }
    
    public IEnumerator ForkSpawnRoutine()
    {
        for (int i = 0; i < GameManager.instance.balancingSO.totalSpawnCount; i++)
        {
            AreaInfo targetArea = MapManager.Instance.GetRandomAvailableArea();
            Debug.Log(targetArea);
            // Spawn 포크
            MapManager.Instance.SpwanFork(targetArea.rowIndex,targetArea.columnIndex);
            // destroyList에 추가
            MapManager.Instance.destrotyedAreaList.Add(targetArea);

            // 케이크 비활성화
            MapManager.Instance.mapInfo[targetArea.rowIndex, targetArea.columnIndex].cube.DestroyCake();
            
            float spawnDelay = Random.Range(GameManager.instance.balancingSO.forkSpawnDelayMin, GameManager.instance.balancingSO.forkSpawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);   
        }
        
    }
}