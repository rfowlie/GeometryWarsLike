using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GeometeryWars
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        public int score;

        private void OnEnable()
        {
            AEnemy.SCORE += Adjust;
        }
        private void OnDisable()
        {
            AEnemy.SCORE -= Adjust;
        }

        private void Start()
        {
            score = 0;
            text.text = score.ToString();
        }

        private void Adjust(int value)
        {
            //change score
            score += value;
            //update UI
            text.text = score.ToString();
        }
    }
}

