using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawnManager : MonoBehaviour
{
    public List<AreaInfo> availableList;
    public GameObject creamPrefab;
    public float timeBetSpawnMax = 4f;
    public float timeBetSpawnMin = 2f;
    private float timeBetSpawn;

    private float lastSpawnTime;
    

    private void Start()
    {
        string resourcePath = $"Prefab/Cake/Cream";
        creamPrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
        
        lastSpawnTime = 0;
        timeBetSpawn = UnityEngine.Random.Range(timeBetSpawnMin, timeBetSpawnMax);

    }

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

    private void Spawn()
    {
        availableList = MapManager.Instance.GetAvailableAreaList();
        Vector3 spawnPosition = availableList[Random.Range(0, availableList.Count)].cube.gameObject.transform.position;
        GameObject cream = Instantiate(creamPrefab, spawnPosition + Vector3.up * 1.5f, Quaternion.identity);
        
        Destroy(cream,10f);
    }

}
