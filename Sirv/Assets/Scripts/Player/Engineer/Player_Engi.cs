using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Engi : Player
{
    // Start is called before the first frame update
    Rewardsystem rewardsystem;
    public bool bomb = false;
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
        Vector2 pos = shotpoint.up.normalized * 2;
        rigid.position = rigid.position + pos;
        skillAcooltimenow = skillAcooltime * skillAcoolPer;
        if (bomb) {
            StartCoroutine(Bombed());
        }
        return;
    }
    public override void Skill_B()
    {
        return;
    }

    IEnumerator Bombed()
    {
        yield return new WaitForFixedUpdate();
        var hit = Physics2D.OverlapCircleAll(transform.position, 0.6f, LayerMask.GetMask("Enemy"));
        foreach (var hitcol in hit)
        {
            hitcol.GetComponent<Enemy>().GetDamage((int)(50 * damagePer), true);
        }
        yield break;
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
        switch (idx) {
            case 0:
                bomb = true;
                break;

        }
        
    }
}
