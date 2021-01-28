using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{    
    public class DropManager 
    {
        public DropManager(SO_Drops drops, int playerDropRateLevel)
        {
            this.drops = drops;
            this.playerDropRateLevel = playerDropRateLevel;
            
            AEnemy.SHOT += (ctx) =>
            {
                if (isDrop()) { CreateDrop(ctx); }                              
            };
        }

        private bool isDrop()
        {
            //change to compare against player drop rate level...
            return Random.Range(0f, 1f) > 0.8f;
        }
        private void CreateDrop(EnemyInfo e)
        {
            //spawn drop
            DropInfo info = drops.GetRandomPickUp();
            Debug.Log($"<color=red>DROP: {info.type}</color>");

            //Use ObjectPooling later on...
            GameObject obj = GameObject.Instantiate(info.prefab);
            //attach Drops component and set type
            Drop drop = obj.GetComponent<Drop>();
            if (drop == null)
            {
                drop = obj.AddComponent<Drop>();
            }

            drop.type = info.type;
            obj.transform.position = e.position;
        }

        //List of all drops...
        //later on this will be an array of DropInfo which will hold the players 
        //currently available drops and chances of getting them depending on the drops they have
        //unlocked and their drop level
        private SO_Drops drops;
        private int playerDropRateLevel;
        public static event System.Action<GameStateInfo> UPDATE;
    }
}