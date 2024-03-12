using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Knife : Turret
{

    [Space]
    public bool attacking = false;
    public int Level = 0;
    public int Damage;
    BoxCollider2D col;
    private void Start()
    {
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
            Attackspeed_Now = 1/Attackspped;
            Debug.Log(Target);
            StartCoroutine(Attack(Target.transform.position));
        }
        if(Attackspeed_Now > 0 && attacking == false)
        {
            Attackspeed_Now -= Time.fixedDeltaTime;
        }
    }

    
    IEnumerator Attack(Vector3 Target)
    {
        while (true)
        {
            if (Level == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target, 10 * Time.deltaTime);
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

        collision.GetComponent<Enemy>().GetDamage(Damage);
        
    }

    // Start is called before the first frame update
}
