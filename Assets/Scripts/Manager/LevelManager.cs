using System;
using System.Collections;
using UnityEngine;

namespace Game.Managers
{
    public class LevelManager : Manager
    {
        [SerializeField] private GameObject levelPrefab;
        [SerializeField] private PlayerManager playerManager;
        private Level level;
        public event Action<Level> LevelSpawned;

        public override void Initialize()
        {
            playerManager.PlayerSpawned += OnPlayerSpawned;
            SpawnLevel();
        }

        private void OnPlayerSpawned(Player player)
        {
            player.Dead += OnDead;

            level.TileAnimator.SetPlayer(player.transform);
            level.TileGenerator.SetPlayer(player.transform);

            void OnDead()
            {
                StartCoroutine(WaitForClick());
                player.Dead -= OnDead;
            }
        }

        private IEnumerator WaitForClick()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            SpawnLevel();
        }

        public void SpawnLevel()
        {
            if (level)
                Destroy(level.gameObject);

            level = Instantiate(levelPrefab).GetComponent<Level>();
            LevelSpawned?.Invoke(level);
        }
    }
}