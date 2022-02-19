using System;
using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Collectables
{
    public class CollectableSpawner : MonoBehaviour
    {
        [Serializable]
        public enum SpawnType
        {
            Ordered,
            Random
        }

        [SerializeField] private int tilesGeneratedToSpawnCollectable = 5;
        [SerializeField] private SpawnType spawnType;
        [SerializeField] private GameObject collectablePrefab;
        [SerializeField] private Transform collectableParent;
        [SerializeField] private TileGenerator tileGenerator;

        private readonly List<Transform> tiles = new List<Transform>();
        private int spawnOrder;

        private void OnEnable()
        {
            tileGenerator.TileSpawned += OnTileSpawned;
        }

        private void OnDestroy()
        {
            tileGenerator.TileSpawned -= OnTileSpawned;
        }

        private void OnTileSpawned(Transform tile)
        {
            tiles.Add(tile);

            if (tiles.Count < tilesGeneratedToSpawnCollectable)
                return;

            CreateCollectable(GetSpawnPosition());

            tiles.Clear();
        }

        private Vector3 GetSpawnPosition()
        {
            return spawnType switch
            {
                SpawnType.Ordered => GetSpawnPositionOrdered(),
                SpawnType.Random => GetSpawnPositionRandom(),
                _ => Vector3.zero
            };
        }

        private Vector3 GetSpawnPositionOrdered()
        {
            var pos = tiles[spawnOrder].position;

            spawnOrder++;

            if (spawnOrder >= tilesGeneratedToSpawnCollectable)
                spawnOrder = 0;

            return pos;
        }

        private Vector3 GetSpawnPositionRandom()
        {
            return tiles[Random.Range(0, tiles.Count)].position;
        }

        private void CreateCollectable(Vector3 position)
        {
            Instantiate(collectablePrefab, position, Quaternion.identity, collectableParent);
        }
    }
}