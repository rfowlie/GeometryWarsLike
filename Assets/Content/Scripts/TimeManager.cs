using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GeometeryWars
{
    //keeps track of the level time
    public class TimeManager
    {
        public TimeManager(TextMeshProUGUI ui, int levelTime)
        {
            this.levelTime = levelTime;
            this.ui = ui;

            currentTime = levelTime;
            ui.text = currentTime.ToString();
        }

        private int levelTime;
        private TextMeshProUGUI ui;


        private float currentTime;
        public float GetCurrentTime() { return currentTime; }
        public float GetTimeFromStart() { return levelTime - currentTime; }

        public bool AdjustTime(float deltaTime)
        {
            currentTime -= deltaTime;
            ui.text = Mathf.Ceil(currentTime).ToString();
            return currentTime < 0f;
        }
    }
}
