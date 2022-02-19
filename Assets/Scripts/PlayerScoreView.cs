using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class PlayerScoreView : MonoBehaviour
    {
        [SerializeField] private Text scoreText;

        public void SetScore(int score)
        {
            scoreText.text = $"{score}";
        }
    }
}