using System;
using Game.Collectables;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CollectableTrigger collectableTrigger;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private Transform cameraPoint;

        public Transform CameraPoint => cameraPoint;
        public CollectableTrigger CollectableTrigger => collectableTrigger;

        private void Start()
        {
            groundChecker.StartFalling += OnStartFalling;
        }

        public event Action Dead;

        private void OnStartFalling()
        {
            Dead?.Invoke();
            groundChecker.StartFalling -= OnStartFalling;
        }
    }
}