using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Scene : MonoBehaviour
{
    private void Awake()
    {
        MapManager.Instance.InitMap();
    }
}
