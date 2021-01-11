using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    //keeps track of the level time
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private int levelTime = 30;
        private float currentTime;
        public float GetCurrentTime() { return currentTime; }
        public float GetTimeFromStart() { return levelTime - currentTime; }

        private void Start()
        {
            currentTime = levelTime;
        }

        public bool AdjustTime(float deltaTime)
        {
            currentTime -= deltaTime;
            return currentTime < 0f;
        }
    }
}
