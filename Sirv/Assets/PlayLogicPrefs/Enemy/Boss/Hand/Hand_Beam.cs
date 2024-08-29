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
    public bool Rotate = true;
    public float Duration = 2.0f;
    public AudioClip Aim;
    public AudioClip Fire;
    public int rotatetime = 30;
    void Start()
    {
        StartCoroutine(Beam());
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        SFXsystem.instance.PlaySoundFX(Aim, transform, 1.0f);
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
        if (Rotate)
        {
            for (int i = 0; i < rotatetime; i++)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, Target.transform.position - transform.position);
                yield return new WaitForFixedUpdate();
            }
        }
        yield return new WaitForSeconds(Duration);
        SFXsystem.instance.PlaySoundFX(Fire, transform, 1.0f);

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
