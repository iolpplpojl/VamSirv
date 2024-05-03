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

    public void BloodSuck()
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

    public void Fire(Enemy enemy)
    {
        if (Random.Range(0f, 1f) <= player.fire)
        {
            enemy.StartCoroutine(enemy.Fire((int)(12*player.damagePer)));
        }
    }
    public void Kill()
    {
        player.EnemyKill();
    }
}
