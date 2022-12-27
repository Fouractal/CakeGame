using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public ResourceManager() {}

    public static T LoadAsset<T>(string resourcePath) where T : Object
    {
        // Resources 경로에서 탐색
        T target = Resources.Load<T>($"{resourcePath}");
        return target;
    }
}
