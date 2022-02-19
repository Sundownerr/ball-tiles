using System;
using UnityEngine;

namespace Game.Collectables
{
    public class CollectableTrigger : MonoBehaviour
    {
        [SerializeField] private string collectableTag;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(collectableTag))
                return;

            var collectable = other.GetComponent<Collectable>();
            Collected?.Invoke(collectable.Score);

            collectable.HandleCollected();
        }

        public event Action<int> Collected;
    }
}