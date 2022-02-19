using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers
{
    public class PlayerManager : Manager
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private UnityEvent onPlayerSpawned;
        [SerializeField] private UnityEvent onPlayerDead;

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
            onPlayerSpawned?.Invoke();

            player.Dead += OnDead;
        }

        private void OnDead()
        {
            player.Dead -= OnDead;
            onPlayerDead?.Invoke();
        }

        public override void Initialize()
        {
            levelManager.LevelSpawned += OnLevelSpawned;
        }
    }
}