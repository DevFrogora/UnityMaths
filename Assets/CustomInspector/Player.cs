using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    public string playerName;
    public string backStory;
    public float health;
    public float damage;

    public float weapon1Damage,weapon2Damage;
    /*
    public string shoeName;
    public int shoeSize;
    public string shoeType;
    */

    [SpaceWithImage(150)]

    public Shoe shoe;

    [ReadOnly]
    public int achievementCount = 5;
    [ReadOnly]
    public float floatValueOfCountr = 5.4f;
    [ReadOnly]
    public string achieveString = "Hello";

    [ReadOnlyWithColor(1,0,0)]
    public string bestAchievement = "You are awesome";

    // Start is called before the first frame update
    void Start()
    {
        health = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomNameForPlayer()
    {
        playerName = RandomNameGenerator.GetRandomName();
    }
}
