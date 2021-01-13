using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make struct so info gets copied and can't be tampered with
public struct GameStateInfo
{
    public int points;
    public int levelPlayer;
    public int levelHealth;
    public int levelMovementSpeed;
    public int levelFireRate;
}
