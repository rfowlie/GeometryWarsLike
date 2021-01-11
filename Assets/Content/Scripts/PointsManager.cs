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
        public int points = 0;

        private void OnEnable()
        {
            AEnemy.SHOT += Adjust;
            Adjust(0);
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

