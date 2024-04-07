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
        int m_health = (int)(damage * player.BloodSuck);
        if (player.health+m_health >= player.maxHealthNow)
        {
            player.health = player.maxHealthNow;
        }
        else
        {
            player.health += m_health;
        }
    }
}
