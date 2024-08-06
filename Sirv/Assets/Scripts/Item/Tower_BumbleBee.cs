using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_BumbleBee : Tower
{
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        StartCoroutine(Heal());
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    IEnumerator Heal()
    {
        while (true)
        {
            anim.Play("bumble_heal");
            var hit = Physics2D.OverlapCircle(transform.position, Radius, LayerMask.GetMask("Player"));
            if(hit != null)
            {
                PlayerComp.Heal(3);
            }
            yield return new WaitForSeconds(2.0f);
        }

    }
}
