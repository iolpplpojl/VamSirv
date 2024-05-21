using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pause : MonoBehaviour
{
    // Start is called before the first frame update
    bool stop = false;
    public GameObject canvs;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            DoPause();   
        }
    }

    public void DoPause()
    {
        if (stop)
        {
            stop = false;
            canvs.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            stop = true;
            canvs.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    public void DoQuit()
    {
        SceneManager.LoadScene("mainmenu");
    }
    public void DoOption()
    {
        
    }
    
}
