using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GroundChecker : MonoBehaviour
    {
        public event Action StartFalling;
        [SerializeField] private Transform target;
       
        private void Update()
        {
            if (!CheckIsOnGround())
            {
                StartFalling?.Invoke();
                enabled = false;
            }
        }

        private bool CheckIsOnGround()
        {
            return Physics.Raycast(target.position, Vector3.down, 5, 1 << 6);
        }
    }
}