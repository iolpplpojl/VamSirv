using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Hand : Boss
{
    public GameObject Bullet;
    public GameObject LaserBeam;
    public Transform[] Beampos;
    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;
    int beforepattern = 999;
    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);
        while (temp == beforepattern)
        {
            temp = Random.Range(0, patternTimeTable.Length);
        }
        switch (temp)
        {
            case 0:
                StartCoroutine(Shoot());    
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;
            case 1:
                StartCoroutine(Shoot());
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;

        }
    }
    private void FixedUpdate()
    {
        if (attackTimenow > 0)
        {
            attackTimenow -= Time.fixedDeltaTime;
        }
        if (attackTimenow <= 0)
        {
            Attack();
        }
    }
    void Update()
    {
        if (flashtime > 0)
        {
            flashtime -= Time.deltaTime * 5f;
            Sprite.material.SetFloat("_FlashAmount", flashtime);
        }
    }
    public override void DeadUniq()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update

    IEnumerator Shoot()
    {
        for(int i = 0; i < 15; i++)
        {
            Hand_Beam beam = Instantiate(LaserBeam, Beampos[Random.Range(0,Beampos.Length)].position,Quaternion.identity,transform.parent).GetComponent<Hand_Beam>();
            beam.Target = target;
            beam.damage = 40;

            yield return new WaitForSeconds(0.2f);
        }
    }
    // Update is called once per frame

}
