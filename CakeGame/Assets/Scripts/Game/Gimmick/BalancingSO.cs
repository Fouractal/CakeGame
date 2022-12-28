using UnityEngine;

namespace Game.Gimmick
{
    [CreateAssetMenu(fileName = "BalancingSO", menuName = "ScripatableObject/Balnancing", order = 0)]
    public class BalancingSO : ScriptableObject
    {
        public float forkSpawnDelayMin;
        public float forkSpawnDelayMax;
        public float attackDelayMin;
        public float attackDelayMax;

        public int forkCountMax;
    }
}