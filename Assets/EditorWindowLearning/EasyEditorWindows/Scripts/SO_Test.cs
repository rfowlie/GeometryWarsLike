using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Test", menuName = "ScriptableObjects/Test Object")]
public class SO_Test : ScriptableObject
{
    [SerializeField] public ID[] ids;
    [SerializeField] public Goblin[] goblins;
    public void AddGoblin()
    {
        goblins = ArrayEX.ResizeArray(goblins.Length + 1, goblins);
    }
}

[System.Serializable]
public struct ID
{
    public int luckyNumber;
    public string nameFirst;
    public string[] nicknames;
}

[System.Serializable]
public struct Goblin
{
    public string name;
    public int strength;
    public int defence;
    public int health;
    public int mana;
}
