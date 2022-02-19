using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers
{
    public class CameraController : Manager
    {
        [SerializeField] private Transform camera;
        [SerializeField] private PlayerManager playerManager;
        private Transform playerCameraPoint;
        
        public override void Initialize()
        {
            playerManager.PlayerSpawned += OnPlayerSpawned;
        }

        private void Update()
        {
            if (playerCameraPoint == null)
                return;

            camera.transform.position = playerCameraPoint.position;
            camera.transform.rotation = playerCameraPoint.rotation;
        }

        private void OnPlayerSpawned(Player player)
        {
            playerCameraPoint = player.CameraPoint;
        }
    }
}