using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Knife : Turret
{

    [Space]
    public bool attacking = false;
    public int Level = 0;
    public int Damage;

    public int[] BonusdamageSheet;
    BoxCollider2D col;
    private void Start()
    {
        SetDamage();
        SetUpgradeAmount();
        Renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (attacking == false)
        {
            MoveMent();
            col.enabled = false;
        }
        if(Attackspeed_Now <= 0 && Find== true && attacking != true)
        {
            attacking = true;
            col.enabled = true;
            Attackspeed_Now = 1.0f / (Attackspped * (1f + ((PlayerComp.attackspeedPer - 1f) * PlayerComp.SideArmAttackSpeddper)));
            StartCoroutine(Attack(Target.transform.position));
        }
        if (UpgradeTool == true)
        {
            Upgrade();
            UpgradeTool = false;
        }
        SetDamage();

    }

    private void FixedUpdate()
    {
        if (Attackspeed_Now > 0 && attacking == false)
        {
            Attackspeed_Now -= Time.fixedDeltaTime;
        }
    }

    override protected void BonusDamage()
    {
        switch (Rarity)
        {
            case 0:
                Damage = BonusdamageSheet[0];
                break;
            case 1:
                Damage = BonusdamageSheet[1];
                break;
            case 2:
                Damage = BonusdamageSheet[2];
                break;
            case 3:
                Damage = BonusdamageSheet[3];
                break;
        }
    }

    IEnumerator Attack(Vector3 Target)
    {
        while (true)
        {
            if (Level == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target, 20 * Time.deltaTime);
            }
            if (Level == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.position, 20 * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, Target) < 0.001f)
            {
                Level = 1;
                Debug.Log("°ø°ÝµµÂø");
                col.enabled = false;

            }
            else if (Vector3.Distance(Player.position, transform.position) < 0.001f)
            {
                Level = 0;
                Debug.Log("º¹±ÍµµÂø");
                attacking = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        collision.GetComponent<Enemy>().GetDamage(Damage+(int)(Damage*KnifeDamagePer));
        
    }

    // Start is called before the first frame update
}
