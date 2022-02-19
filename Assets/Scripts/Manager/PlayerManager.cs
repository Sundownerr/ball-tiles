using System;
using UnityEngine;

namespace Game.Managers
{
    public class PlayerManager : Manager
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private LevelManager levelManager;

        private Player player;
        public event Action<Player> PlayerSpawned;

        private void OnLevelSpawned(Level level)
        {
            var playerGO = Instantiate(
                playerPrefab,
                level.PlayerSpawnPoint.position,
                Quaternion.identity,
                level.transform);

            player = playerGO.GetComponent<Player>();

            PlayerSpawned?.Invoke(player);
        }

        public override void Initialize()
        {
            levelManager.LevelSpawned += OnLevelSpawned;
        }
    }
}