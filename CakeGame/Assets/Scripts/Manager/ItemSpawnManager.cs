using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawnManager : SingletonMonoBehaviour<ItemSpawnManager>
{
    public GameObject creamPrefab;
    
    private void Awake()
    {
        string resourcePath = $"Prefab/Cake/Cream";
        creamPrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
    }
/*
    private void Update()
    {
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 마지막 생성 시간 갱신
            lastSpawnTime = Time.time;
            // 생성 주기 변경
            timeBetSpawn = UnityEngine.Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            // 아이템 생성
            Spawn();
        }
    }
*/

    /*
    private void Spawn()
    {
        List<AreaInfo> availableList = MapManager.Instance.GetAvailableAreaList();
        Vector3 spawnPosition = availableList[Random.Range(0, availableList.Count)].cube.gameObject.transform.position;
        GameObject cream = Instantiate(creamPrefab, spawnPosition + Vector3.up * 1.5f, Quaternion.identity);
        
        Destroy(cream,10f);
    }
    */

    public IEnumerator CreamSpawnRoutine()
    {
        while (true)
        {
            AreaInfo targetArea = MapManager.Instance.GetRandomAvailableArea();
            Vector3 spawnPosition = targetArea.cube.gameObject.transform.position + Vector3.up * 1.5f;
            GameObject creamInstance = Instantiate(creamPrefab, spawnPosition, Quaternion.identity);

            creamInstance.transform.DORotate(Vector3.right, 1f, RotateMode.Fast).SetLoops(-1);
            Destroy(creamInstance,10f);

            float spawnDelay = Random.Range(GameManager.Instance.balancingSO.creamSpawnDelayMin, GameManager.Instance.balancingSO.creamSpawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);   
        }
    }

}
