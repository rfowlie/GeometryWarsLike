using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    //hold an array of all map gameObjects that will be used in the game
    [CreateAssetMenu(fileName = "Maps", menuName = "ScriptableObjects/Game/Maps")]
    public class SO_Maps : ScriptableObject
    {
        [SerializeField] private GameObject[] allMaps;
        public GameObject GetMapAtIndex(int index)
        {
            if(index < 0 || index > allMaps.Length)
            {
                index = 0;
            }

            return allMaps[index];
        }
    }
}