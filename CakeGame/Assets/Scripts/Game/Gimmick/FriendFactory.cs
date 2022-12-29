using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FriendFactory : SingletonMonoBehaviour<FriendFactory>
{
    public int fieldFriendCount = 0;

    public Define.FriendType SelectRandomFriendType()
    {
        Define.FriendType friendType = (Define.FriendType)(Random.Range(1, System.Enum.GetValues(enumType: typeof(Define.FriendType)).Length));
        return friendType;
    }
    
    public IEnumerator ForkSpawnRoutine()
    {
        while(true)
        {
            List<AreaInfo> curAreaInfoList = MapManager.Instance.GetAvailableAreaList();
            List<AreaInfo> spawnableAreaList = new List<AreaInfo>();

            for (int i = 0; i < curAreaInfoList.Count; i++)
            {
                if (curAreaInfoList[i].friendType == Define.FriendType.None)
                    spawnableAreaList.Add(curAreaInfoList[i]);
            }

            int randomIndex = Random.Range(0, spawnableAreaList.Count);
            AreaInfo targetArea = spawnableAreaList[randomIndex];

            Define.FriendType randomFriendType = SelectRandomFriendType();
            
            MapManager.Instance.MarkFriendInfo(targetArea.rowIndex,targetArea.columnIndex, randomFriendType);

            string resourcePath = $"Prefab/Friends/{randomFriendType.ToString()}";
            GameObject curFriendPrefab = ResourceManager.LoadAsset<GameObject>(resourcePath);
            GameObject curFriendInstance = Instantiate(curFriendPrefab, Vector3.zero,Quaternion.identity, targetArea.cube.transform.parent);

            Debug.Log(targetArea.cube.transform.parent.position);    
            
            float spawnDelay = Random.Range(GameManager.Instance.balancingSO.friendSpawnDelayMin, GameManager.Instance.balancingSO.friendSpawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);   
        }
    }
}
