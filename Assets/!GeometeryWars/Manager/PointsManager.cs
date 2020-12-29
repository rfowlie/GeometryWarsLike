using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GeometeryWars
{
    //keep track of points...
    public class PointsManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        public int points;

        private void OnEnable()
        {
            AEnemy.SHOT += Adjust;
        }    

        public void Adjust(int value)
        {
            //change score
            points += value;
            //update UI
            text.text = points.ToString();
        }
    }
}

