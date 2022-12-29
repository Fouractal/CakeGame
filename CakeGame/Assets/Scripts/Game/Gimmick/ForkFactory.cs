using System.Collections;
using UnityEngine;

public class ForkFactory : SingletonMonoBehaviour<ForkFactory>
{
    // 포크의 생성을 관리하는 로직
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
            
            // destroyList에 추가 //TODO Fork에서 처리한 후 추가되어야함
            MapManager.Instance.destroyedAreaList.Add(targetArea);
            
            // AavailableList에서 제거
            

            float spawnDelay = Random.Range(GameManager.instance.balancingSO.forkSpawnDelayMin, GameManager.instance.balancingSO.forkSpawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);   
        }
        
    }
}