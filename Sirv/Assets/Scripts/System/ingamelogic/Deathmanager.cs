using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Deathmanager : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    public Image img;
    public Canvas canvas;

    public void Death()
    {
        canvas.sortingOrder = 9999;
        StartCoroutine(FadeIN());
    }

    IEnumerator FadeIN()
    {
        while (true)
        {
            img.color = img.color + new Color(0, 0, 0, 0.0075f);
            if (img.color.a >= 1.0f)
            {
                SceneManager.LoadScene("Mainmenu");
                yield break;
            }
            yield return new WaitForFixedUpdate();

        }
    }
}
