using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PatternCreator
{
    [CreateAssetMenu(fileName = "PatternInfoContainer", menuName = "PatternInfoContainer")]
    public class SO_PatternInfoContainer : ScriptableObject
    {
        //still make accessible later
        [SerializeField] public PatternInfo[] values;

        public void AddPatternInfo(PatternInfo p)
        {
            values = ArrayEX.Grow(values, values.Length + 1, p);
        }
        public void RemovePatternInfo(int index)
        {
            values = ArrayEX.RemoveAndResize(values, new int[] { index });
        }
        public PatternInfo[] GetValues() { return values; }
        public int GetLength() { if (values == null) { return 0; } else { return values.Length; } }
    }
}