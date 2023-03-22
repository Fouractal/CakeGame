using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AreaInfo
{
    public int rowIndex = 0;
    public int columnIndex = 0;

    public Define.FriendType friendType = Define.FriendType.None;
    
    public Define.CubeType cubeType = Define.CubeType.Basic;
    public Cube cube;
    
    public bool isAimed = false;
    public bool isPlayerJoin = false;
    
}
