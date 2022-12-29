using UnityEngine;

namespace Game.Gimmick
{
    [CreateAssetMenu(fileName = "BalancingSO", menuName = "ScripatableObject/Balnancing", order = 0)]
    public class BalancingSO : ScriptableObject
    {
        [Header("Fork")]
        public int totalSpawnCount;
        
        public float forkSpawnDelayMin;
        public float forkSpawnDelayMax;
        public float attackDelayMin; // attackDelay는 다음 공격까지의 시간? 케이크가 조준되고 깜빡이는 시간?
        public float attackDelayMax;

        public int forkCountMax;

        [Header("Time")]
        public float Timer;
        
        [Header("Cream")]
        public float creamSpawnDelayMin;
        public float creamSpawnDelayMax;
        public int blockCreateCost;
    }
}