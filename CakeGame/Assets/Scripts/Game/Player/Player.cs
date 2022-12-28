using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static void InitPlayer()
    {
        string resourcePath = $"Prefab/Player/Player";
        GameObject playerPrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
        Object.Instantiate(playerPrefab, Vector3.up * 3, Quaternion.identity, GameObject.Find("Object").transform);
    }
}
