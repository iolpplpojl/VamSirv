using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Data_Send : MonoBehaviour
{
    // Start is called before the first frame update
    public int PlayerType;
    bool clicked;
    public void onclick()
    {
        if (!clicked)
        {
            SceneManager.LoadScene("Scenemanager", LoadSceneMode.Additive);
            StartCoroutine(CheckLoad());
            clicked = true;
        }
    }

    IEnumerator CheckLoad()
    {
        while (true)
        {
            if (SceneManager.GetSceneByName("Scenemanager").isLoaded)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenemanager"));
                GameObject.FindWithTag("Scene_Man").GetComponent<Scene_Man>().GetData(PlayerType);
                SceneManager.UnloadSceneAsync("Mainmenu");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
