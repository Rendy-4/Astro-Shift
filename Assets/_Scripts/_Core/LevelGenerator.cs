using UnityEngine;

namespace AstroShift.Core
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _levelPartStart;
        [SerializeField] private Transform _LevelPart1;
        private void Awake() {
            Transform lastLevelPartTransform;
            lastLevelPartTransform = SpawnLevelPart(_levelPartStart.Find("End Position").position);
            lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("End Position").position);
            lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("End Position").position);
            lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("End Position").position);
            lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("End Position").position);
        }

        private Transform SpawnLevelPart(Vector3 spawnposition)
        {
            Transform levelPartTransform = Instantiate(_LevelPart1, spawnposition, Quaternion.identity);
            return levelPartTransform;
        }
    }
}
