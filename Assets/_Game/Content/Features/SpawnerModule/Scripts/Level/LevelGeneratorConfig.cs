using UnityEngine;

namespace _Game.Content.Features.SpawnerModule.Scripts.Level
{
    [CreateAssetMenu(fileName = "LevelGeneratorConfig", menuName = "Configs/LevelGeneratorConfig")]
    public class LevelGeneratorConfig : ScriptableObject
    {
        [Header("Vertical Spacing")]
        public float minStepY;
        public float maxStepY;
        public float startY;

        [Header("Horizontal Constraints")]
        public float sidePadding;
        
        [SerializeField] public int initialSpawn;
    }
}