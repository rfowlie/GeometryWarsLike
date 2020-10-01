using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class EnemyManager : MonoBehaviour
    {
        //contain pool for each type of enemy
        //COULD RUN EACH ENEMIES MOVEMENT FROM HERE, ALLOWING FOR GAME STATE PAUSES...
        [SerializeField] private ObjectPool chase;
        [SerializeField] private ObjectPool wander;
    }
}