using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GeometeryWars
{
    //keeps track of the level time
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;


        [SerializeField] private int levelTime = 30;
        private float currentTime;
        public float GetCurrentTime() { return currentTime; }
        public float GetTimeFromStart() { return levelTime - currentTime; }

        private void Start()
        {
            currentTime = levelTime;
            text.text = currentTime.ToString();
        }

        public bool AdjustTime(float deltaTime)
        {
            currentTime -= deltaTime;
            text.text = Mathf.Ceil(currentTime).ToString();
            return currentTime < 0f;
        }
    }
}
