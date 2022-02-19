using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tiles
{
    public class TileGenerator : MonoBehaviour
    {
        private const int startTilesCount = 9;
        [SerializeField] private float tileSpawnDistance;
        [SerializeField] private int maxSpawnedTiles;
        [SerializeField] private Transform tileParent;
        [SerializeField] private GameObject tilePrefab;

        private readonly Queue<Transform> tiles = new Queue<Transform>();
        private Difficulty difficulty;
        private Direction direction;
        private Transform lastMainTile;
        private Transform player;

        private void Update()
        {
            if (tiles.Count < startTilesCount)
                return;

            var distance = Vector3.Distance(player.position, lastMainTile.position);

            if (distance < tileSpawnDistance)
            {
                GenerateMainTile(lastMainTile.position + GetNextTilePosition(lastMainTile));
                GenerateSideTiles(difficulty);
            }
        }

        public void StartGenerating(Difficulty difficulty)
        {
            this.difficulty = difficulty;

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

        public event Action<Transform> TileSpawned;

        public void SetPlayer(Transform player)
        {
            this.player = player;
        }

        private Transform GetTile()
        {
            var tile = tiles.Count >= maxSpawnedTiles * (int) difficulty
                ? tiles.Dequeue()
                : Instantiate(tilePrefab, tileParent).transform;

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