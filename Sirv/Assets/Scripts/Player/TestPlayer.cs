using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Player
{
    // Start is called before the first frame update

    public GameObject Bullet;
    public override void Attack()
    {
        Debug.Log("TestAttack");
        GameObject Bul = Instantiate(Bullet);
        Bul.transform.position = shotpoint.position;
        Bul.transform.rotation = shotpoint.rotation;
        ammo--;
        attackspeed_now = 1.0f/attackspeed;
    }
    public override void Skill_A()
    {
        Debug.Log("Skill_A");

    }
    public override void Skill_B()
    {
        Debug.Log("Skill_B");
    }
}
