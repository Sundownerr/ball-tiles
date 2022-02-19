using Game.Managers;
using UnityEngine;

namespace Game
{
    public class PlayerScoreController : MonoBehaviour
    {
        [SerializeField] private PlayerScoreView view;
        [SerializeField] private PlayerManager playerManager;

        private readonly PlayerScoreModel model = new PlayerScoreModel();

        private void Start()
        {
            playerManager.PlayerSpawned += OnPlayerSpawned;
        }

        private void OnPlayerSpawned(Player player)
        {
            player.CollectableTrigger.Collected += OnCollected;
            player.Dead += OnDead;

            void OnDead()
            {
                player.CollectableTrigger.Collected -= OnCollected;
                player.Dead -= OnDead;
            }
        }

        private void OnCollected(int score)
        {
            model.Score += score;
            view.SetScore(model.Score);
        }
    }
}