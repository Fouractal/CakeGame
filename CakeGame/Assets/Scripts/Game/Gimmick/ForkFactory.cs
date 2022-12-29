using System.Collections;
using UnityEngine;

public class ForkFactory : SingletonMonoBehaviour<ForkFactory>
{
    // 포크의 생성을 관리하는 로직
    public void StartForkSpawnRoutine()
    {
        StartCoroutine(ForkSpawnRoutine());
    }

    public void PauseForkSpawnRoutine()
    {
        
    }
    
    public IEnumerator ForkSpawnRoutine()
    {
        for (int i = 0; i < GameManager.Instance.balancingSO.totalSpawnCount; i++)
        {
            AreaInfo targetArea = MapManager.Instance.GetRandomAvailableArea();
            // Spawn 포크
            MapManager.Instance.SpwanFork(targetArea.rowIndex,targetArea.columnIndex);
            
            // destroyList에 추가 //TODO Fork에서 처리한 후 추가되어야함
            MapManager.Instance.destroyedAreaList.Add(targetArea);
            
            // AavailableList에서 제거
            

            float spawnDelay = Random.Range(GameManager.Instance.balancingSO.forkSpawnDelayMin, GameManager.Instance.balancingSO.forkSpawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);   
        }
        
    }
}