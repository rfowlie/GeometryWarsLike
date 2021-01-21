using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PatternCreator
{
    [CreateAssetMenu(fileName = "PatternInfoContainer", menuName = "PatternInfoContainer")]
    public class SO_PatternInfoContainer : ScriptableObject
    {
        public SO_PatternInfoContainer(PatternInfo[] arr)
        {
            values = arr;
        }
        //still make accessible later
        [SerializeField] public PatternInfo[] values;
        public int GetLength() { return values.Length; }
    }
}