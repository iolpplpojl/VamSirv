using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Engi : Player
{
    // Start is called before the first frame update
    Rewardsystem rewardsystem;
    public bool bomb = false;
    bool TP_ATS;
    public int overheatarmor = 0;
    bool overheatTime = false;
    int overheatDuration = 400;
    public bool TP_MOS = false;
    bool overheat;
    public void GetDefaultWeapon(Rewardsystem rewardsystem)
    {
        this.rewardsystem = rewardsystem;
        GetUniqueWeapon();
    }
    void GetUniqueWeapon()
    {
        rewardsystem.DoTurret(0, 1000);
        rewardsystem.UpdateSideArmUI();
    }
    public override void Attack()
    {
        Debug.Log(Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        return;
    }
    public override void Skill_A()
    {
        if (TP_MOS == false)
        {
            Vector2 pos = shotpoint.up.normalized * 2;
            rigid.position = rigid.position + pos;
        }
        else
        {
            Debug.Log(Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= 3f)
            {
                rigid.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Vector2 pos = shotpoint.up.normalized * 3;
                rigid.position = rigid.position + pos;
            }
        }
        skillAcooltimenow = skillAcooltime * skillAcoolPer;

        /**
        if (bomb) {
            StartCoroutine(Bombed());
        }
        **/
        if (TP_ATS)
        {
            StartCoroutine(TPATS());
        }
        SFXsystem.instance.PlaySoundFX(new AudioClip[] { Effects[3], Effects[4]},transform,1f);
        return;
    }
    public override void Skill_B()
    {
        if (overheat == false)
        {
            StartCoroutine(OverHeat());
            skillBcooltimenow = 999;
        }
        return;
    }

    IEnumerator OverHeat()
    {
        AudioSource temp = SFXsystem.instance.PlaySoundFX(Effects[0], transform, 0.3f, true,false);
        overheat = true;
        armor += overheatarmor;
        float tempdmg = 0f;
        float tempatk = 0f;
        if (overheatTime == false)
        {
            for (int i = 0; i < 660; i++)
            {
                if (temp == null)
                {
                    temp = SFXsystem.instance.PlaySoundFX(Effects[1], transform, 0.3f, true, true);
                }
                damagePer -= 0.001f;
                tempdmg += 0.001f;
                spriteRenderer.color = new Color(1, 1f - ((0.7f / 660) * i), 1f - ((1f / 660) * i), 1f);
                attackspeedPer += 0.003f;
                tempatk += 0.003f;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            damagePer -= 0.001f*660;
            tempdmg += 0.001f * 660;
            spriteRenderer.color = new Color(1, 1f - 0.7f, 1f - 1f, 1f);
            attackspeedPer += 0.003f * 660;
            tempatk += 0.003f * 660;
        }
        fireCount += 5;
        for(int i = 0; i< overheatDuration; i++)
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
        armor -= overheatarmor;
        skillBcooltimenow = skillBcooltime * skillBcoolPer;

        yield break;
    }
    IEnumerator TPATS()
    {
        attackspeedPer += 0.66f;
        speedPer += 0.20f;
        yield return new WaitForSeconds(1.0f);
        attackspeedPer -= 0.66f;
        speedPer -= 0.20f;
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
                GetUniqueWeapon();
                GetUniqueWeapon();
                break;
            case 2:
                overheatarmor += 7;
                break;
            case 3:
                TP_MOS = true;
                break;
            case 4:
                GetUniqueWeapon();
                GetUniqueWeapon();
                break;
            case 5:
                overheatTime = true;
                overheatDuration = 520;
                skillBcooltime = 10f;
                break;
            case 6:
                GetUniqueWeapon();
                GetUniqueWeapon();
                GetUniqueWeapon();
                break;
            case 7:
                TP_ATS = true;
                break;
            case 8:
                GetUniqueWeapon();
                GetUniqueWeapon();
                GetUniqueWeapon();
                GetUniqueWeapon();
                break;
        }

    }
}
