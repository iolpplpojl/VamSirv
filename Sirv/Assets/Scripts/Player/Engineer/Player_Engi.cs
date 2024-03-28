using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Engi : Player
{
    // Start is called before the first frame update


    public void GetDefaultWeapon(Rewardsystem rewardsystem)
    {
        rewardsystem.DoTurret(3);
    }
    public override void Attack()
    {
        return;
    }
    public override void Skill_A()
    {
        return;
    }
    public override void Skill_B()
    {
        return;
    }

    public override void GetUniqueItem(int idx)
    {
        return;
    }
}
