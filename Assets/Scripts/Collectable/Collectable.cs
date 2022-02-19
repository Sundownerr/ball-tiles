using UnityEngine;

namespace Game.Collectables
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private int score;
        public int Score => score;

        public void HandleCollected()
        {
            Destroy(gameObject);
        }
    }
}