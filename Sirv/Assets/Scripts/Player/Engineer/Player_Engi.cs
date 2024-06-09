using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Engi : Player
{
    // Start is called before the first frame update
    Rewardsystem rewardsystem;
    public bool bomb = false;
    bool overheat;
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
        if (overheat == false)
        {
            StartCoroutine(OverHeat());
            skillBcooltimenow = skillBcooltime * skillBcoolPer;
        }
        return;
    }

    IEnumerator OverHeat()
    {
        AudioSource temp = SFXsystem.instance.PlaySoundFX(Effects[0], transform, 0.3f, true,false);
        overheat = true;
        float tempdmg = 0f;
        float tempatk = 0f;
        for (int i = 0; i < 660; i++)
        {
            if(temp == null)
            {
                temp = SFXsystem.instance.PlaySoundFX(Effects[1], transform, 0.3f, true,true);
            }
            damagePer -= 0.001f;
            tempdmg += 0.001f;
            spriteRenderer.color = new Color(1, 1f-((0.7f/660)*i), 1f - ((1f / 660)*i), 1f);
            attackspeedPer += 0.003f;
            tempatk += 0.003f;
            yield return new WaitForSeconds(0.01f);
        }
        fireCount += 5;
        for(int i = 0; i<400; i++)
        {
            if (temp == null)
            {
                temp = SFXsystem.instance.PlaySoundFX(Effects[1], transform, 0.3f, true, true);
            }
            yield return new WaitForSeconds(0.01f);
        }
        if (temp != null)
        {
            Destroy(temp);
        }
        temp = SFXsystem.instance.PlaySoundFX(Effects[2], transform, 0.3f, true,false);
        damagePer += tempdmg;
        attackspeedPer -= tempatk;
        fireCount -= 5;
        overheat = false;
        spriteRenderer.color = Color.white;

        yield break;
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
            case 1:
                bomb = true;
                break;

        }
        
    }
}
