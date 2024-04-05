using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Engi : Player
{
    // Start is called before the first frame update
    Rewardsystem rewardsystem;

    public void GetDefaultWeapon(Rewardsystem rewardsystem)
    {
        this.rewardsystem = rewardsystem;
        GetUniqueWeapon();
    }
    void GetUniqueWeapon()
    {
        rewardsystem.DoTurret(3);
        rewardsystem.UpdateSideArmUI();
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
        switch (idx)
        {
            case 0:
                GetUniqueWeapon();
                break;
        }

        return;
    }
    public override void UniqueLevelUP(int idx)
    {

    }
}
