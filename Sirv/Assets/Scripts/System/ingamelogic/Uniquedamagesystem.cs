using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniquedamagesystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static Uniquedamagesystem instance;
    public Player player;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }   

    public void BloodSuck(int damage)
    {
        if (Random.Range(0f, 1f) <= player.BloodSuck)
        {
            if (player.health + 3 >= player.maxHealthNow)
            {
                player.health = player.maxHealthNow;
            }
            else
            {
                player.health += 3;
            }
        }
    }
    public void Kill()
    {
        player.EnemyKill();
    }
}
