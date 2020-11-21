using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeometeryWars
{
    //rotates to face player...
    //dashs quickly forward a certain distance/range???
    //then stops for awhile
    //only when player in range??? 
    //measure the arc distance??? 
    //wouldn't be accurate if using odd shapes

    //this one will need 3 movement states
    //IDLE/ROTATETOFACE/DASH
    public class Enemy_Dash_Surface : AEnemy
    {
        protected override void SetMovement()
        {
            currentMovement = new RotateToTarget(this);
        }

        //FOR NOWWWWW
        bool isDash = false;
        float count = 0f;
        private void Update()
        {
            count += Time.deltaTime;
            if(count > 5f)
            {
                count = 0f;
                isDash = !isDash;
                if(isDash) { currentMovement = new wander(this); }
                else { currentMovement = new RotateToTarget(this); }
            }
        }
    }
}
