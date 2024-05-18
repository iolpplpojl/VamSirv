using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Resultsystem : MonoBehaviour
{
    int kill;
    int gold;
    int exp;
    int damage;

    public static Resultsystem instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void killUp()
    {
        kill++;
    }
    public void goldUp(int x)
    {
        gold += x;
    }

    public void expUp(int x)
    {
        exp += x;
    }
    public void damageUP(int x)
    {
        damage += x;
    }

    public void StartLoad()
    {
        SceneManager.LoadScene("ResultScene", LoadSceneMode.Additive);
        StartCoroutine(ResultScene());
    }
    public IEnumerator ResultScene()
    {
        while (true)
        {
            if (SceneManager.GetSceneByName("ResultScene").isLoaded)
            {
                Resultscene.instance.setText(kill, gold, exp);
                SceneManager.UnloadSceneAsync("Legitgame");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
