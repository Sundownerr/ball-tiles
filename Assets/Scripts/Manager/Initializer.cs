using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private List<Manager> managers;

        private void Start()
        {
            for (var i = 0; i < managers.Count; i++)
            {
                managers[i].Initialize();
            }
        }
    }
}