using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Man : MonoBehaviour
{
    // Start is called before the first frame update
    public int PlayerType;
    public GameObject[] PlayerPrefs;
    public GameObject cam;
    public GameObject Spawner;
    public GameObject AmmoUI;
    public GameObject SkillUI;
    public void GetData(int PlayerType)
    {
        this.PlayerType = PlayerType;
        Debug.Log("GetData");
        SceneManager.LoadScene("Legitgame", LoadSceneMode.Additive);
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        while (true)
        {
            if (SceneManager.GetSceneByName("Legitgame").isLoaded)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Legitgame"));
                Instantiate(PlayerPrefs[PlayerType]);
                Instantiate(cam);
                GameObject.FindWithTag("Cam_mousefal").GetComponent<Cam_MouseFollow>().FindPlayer();
                Instantiate(Spawner);
                Instantiate(AmmoUI);
                GameObject.FindWithTag("AmmoUI").GetComponentInChildren<AmmoUI>().getData();
                Instantiate(SkillUI);
                GameObject.FindWithTag("SkillUI").GetComponent<SkillUI>().getData();

                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
