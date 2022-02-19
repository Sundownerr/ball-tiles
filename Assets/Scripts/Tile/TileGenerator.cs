using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tiles
{
    public class TileGenerator : MonoBehaviour
    {
        [Serializable]
        public enum Difficulty
        {
            Easy = 3,
            Medium = 2,
            Hard = 1
        }

        private const int startTilesCount = 9;

        [SerializeField] private Difficulty difficulty;
        [SerializeField] private float tileSpawnDistance;
        [SerializeField] private int maxSpawnedTiles;
        [SerializeField] private Transform tileParent;
        [SerializeField] private GameObject tilePrefab;

        private readonly Queue<Transform> tiles = new Queue<Transform>();
        private Direction direction;
        private Transform lastMainTile;
        private Transform player;

        private void Start()
        {
            maxSpawnedTiles *= (int) difficulty;

            for (var i = -1; i < 2; i++)
            {
                var zOffset = transform.forward * tilePrefab.transform.localScale.z * i;

                for (var j = -1; j < 2; j++)
                {
                    var xOffset = transform.right * tilePrefab.transform.localScale.x * j;
                    GenerateMainTile(transform.position + zOffset + xOffset);
                }
            }
        }

        private void Update()
        {
            var distance = Vector3.Distance(player.position, lastMainTile.position);

            if (distance < tileSpawnDistance)
            {
                GenerateMainTile(lastMainTile.position + GetNextTilePosition(lastMainTile));
                GenerateSideTiles(difficulty);
            }
        }

        public event Action<Transform> TileSpawned;

        public void SetPlayer(Transform player)
        {
            this.player = player;
        }

        private Transform GetTile()
        {
            var tile = tiles.Count >= maxSpawnedTiles ? tiles.Dequeue() : Instantiate(tilePrefab, tileParent).transform;

            tiles.Enqueue(tile);

            if (tiles.Count > startTilesCount + 1)
                TileSpawned?.Invoke(tile);

            return tile;
        }

        private void GenerateMainTile(Vector3 position)
        {
            lastMainTile = GetTile();
            lastMainTile.position = position;
        }

        private void GenerateSideTiles(Difficulty difficulty)
        {
            if (difficulty == Difficulty.Hard)
                return;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    GenerateSideTile(GenerateSideTile(lastMainTile));
                    break;
                case Difficulty.Medium:
                    GenerateSideTile(lastMainTile);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Transform GenerateSideTile(Transform mainTile)
        {
            var tile = GetTile();

            var offset = direction switch
            {
                Direction.Forward => -mainTile.right * mainTile.localScale.x,
                Direction.Right => -mainTile.forward * mainTile.localScale.z,
                _ => throw new ArgumentOutOfRangeException()
            };

            tile.position = mainTile.position + offset;

            return tile;
        }

        private Vector3 GetNextTilePosition(Transform previousTile)
        {
            direction = (Direction) Random.Range(0, 2);

            return direction switch
            {
                Direction.Forward => previousTile.forward * previousTile.localScale.z,
                Direction.Right => previousTile.right * previousTile.localScale.x,
                _ => Vector3.zero
            };
        }

        private enum Direction
        {
            Forward = 0,
            Right = 1
        }
    }
}