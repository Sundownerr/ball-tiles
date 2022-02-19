using Game.Tiles;
using UnityEngine;

namespace Game
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private TileGenerator tileGenerator;
        [SerializeField] private TileAnimator tileAnimator;

        public Transform PlayerSpawnPoint => playerSpawnPoint;
        public TileGenerator TileGenerator => tileGenerator;
        public TileAnimator TileAnimator => tileAnimator;
    }
}