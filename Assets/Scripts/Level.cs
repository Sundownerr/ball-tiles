using System;
using Game.Tiles;
using UnityEngine;

namespace Game
{
    [Serializable]
    public enum Difficulty
    {
        Easy = 3,
        Medium = 2,
        Hard = 1
    }

    public class Level : MonoBehaviour
    {
        [SerializeField] private Difficulty difficulty;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private TileGenerator tileGenerator;
        [SerializeField] private TileAnimator tileAnimator;

        public Transform PlayerSpawnPoint => playerSpawnPoint;
        public TileGenerator TileGenerator => tileGenerator;
        public TileAnimator TileAnimator => tileAnimator;

        private void Start()
        {
            tileGenerator.StartGenerating(difficulty);
        }
    }
}