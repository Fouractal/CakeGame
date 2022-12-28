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
            MapManager.Instance.SpwanFork(targetArea.rowIndex,targetArea.columnIndex);

            
            
            float spawnDelay = Random.Range(GameManager.instance.balancingSO.forkSpawnDelayMin, GameManager.instance.balancingSO.forkSpawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);   
        }
        
    }
}