using UnityEngine;
using System.Collections.Generic;

namespace AstroShift.Core
{
    public class LevelGenerator : MonoBehaviour
    {
        private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
        [SerializeField] private Transform _levelPartStart;
        [SerializeField] private List<Transform> _listPartLevel;
        [SerializeField] private Transform _playerTransform;

        private Vector3 _lastEndPosition;
        private int stratingLevelParts = 5;
        private void Awake() {
            _lastEndPosition = _levelPartStart.Find("End Position").position;
            for (int i = 0; i < stratingLevelParts; i++)
            {
                SpawnLevelPart();
            }
        }

        private void Update() {
            if (Vector3.Distance(_playerTransform.position, _lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnLevelPart();
            }
        }

        private void SpawnLevelPart()
        {
            Transform randomLevelPart = _listPartLevel[Random.Range(0, _listPartLevel.Count)];

            Transform lastLevelPartTransform = CreatLevelPart(randomLevelPart, _lastEndPosition);
            _lastEndPosition = lastLevelPartTransform.Find("End Position").position;
        }
        private Transform CreatLevelPart(Transform LevelPart, Vector3 spawnposition)
        {
            Transform levelPartTransform = Instantiate(LevelPart, spawnposition, Quaternion.identity);
            return levelPartTransform;
        }
    }
}
