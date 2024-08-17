using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Beam : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    BoxCollider2D col;
    public GameObject Target;
    public int damage;

    void Start()
    {
        StartCoroutine(Beam());
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(damage);
        }
    }


    IEnumerator Beam()
    {
        for(int i = 0; i < 30; i++)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward,Target.transform.position - transform.position);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2f);
        anim.Play("Beam_On");
        col.enabled = true;

        AnimatorStateInfo animstat = anim.GetCurrentAnimatorStateInfo(0);
        while (!animstat.IsName("Beam_On"))
        {
            animstat = anim.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForFixedUpdate();

        }
        while (animstat.IsName("Beam_On") && animstat.normalizedTime <= 1f)
        {
            animstat = anim.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
