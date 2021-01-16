using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{    
    public class DropManager 
    {
        public DropManager(SO_Drops drops)
        {
            this.drops = drops;

            AEnemy.SHOT += (ctx) =>
            {
                Drop temp = drops.GetRandomPickUp();
                temp.transform.position = ctx.position;
            };
        }

        //List of all drops...
        //later on this will be an array of DropInfo which will hold the players 
        //currently available drops and chances of getting them depending on the drops they have
        //unlocked and their drop level
        private SO_Drops drops;
    }
}