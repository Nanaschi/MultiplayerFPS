using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SpawnManager: MonoBehaviour
    {

        public static SpawnManager Instance;
        [SerializeField]
        private GameObject[] _spawnPoints;

        private void Awake()
        {
            Instance = this;
        }

        public Transform GetRandomSpawnPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform;
        }
    }
}