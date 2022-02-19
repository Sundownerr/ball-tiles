using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class PlayerMover : MonoBehaviour
    {
        public enum Direction
        {
           None =0, Forward =1 , Right =2 , Down =3
        }

        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        [SerializeField] private float fallSpeed;
        
        private Direction direction;
      
        private void Update()
        {
            if (direction == Direction.None)
                return;

            target.position += GetMoveDirection() * Time.deltaTime * speed;
        }

        public void StartFalling()
        {
            direction = Direction.Down;
            speed = fallSpeed;
        }

        public void ChangeMoveDirection()
        {
            direction++;

            if (direction > Direction.Right)
                direction = Direction.Forward;
        }
        
        private Vector3 GetMoveDirection()
        {
            return direction switch
            {
                Direction.Forward => target.forward,
                Direction.Right => target.right,
                Direction.Down => Vector3.down,
                _ => Vector3.zero
            };
        }
    }
}