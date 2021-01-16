using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace GeometeryWars
{
    //keep track of points...
    public class PointsManager 
    {
        public PointsManager(TextMeshProUGUI ui)
        {
            //hate that this is coupled...
            AEnemy.SHOT += Adjust;

            this.ui = ui;
            points = 0;
            ui.text = points.ToString();
        }

        private TextMeshProUGUI ui;
        public int points { get; private set; }

        public void Adjust(EnemyInfo e)
        {
            //change score
            points += e.points;
            //update UI
            ui.text = points.ToString();
        }
    }
}

