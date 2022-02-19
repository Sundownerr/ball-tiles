using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Controls : MonoBehaviour
    {
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private GroundChecker groundChecker;

        private void Start()
        {
            groundChecker.StartFalling += OnStartFalling;
        }

        private void OnStartFalling()
        {
            playerMover.StartFalling();
            groundChecker.StartFalling -= OnStartFalling;
        }

        private void Update()
        {
            if (!groundChecker.enabled)
                return;
            
            if (Input.GetMouseButtonDown(0))
                playerMover.ChangeMoveDirection();
        }
    }
}