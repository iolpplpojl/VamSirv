using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pause : MonoBehaviour
{
    // Start is called before the first frame update
    bool stop = false;
    bool OptOn = false;
    public GameObject canvs;
    public GameObject Opt;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            DoPause();   
        }
    }

    public void OptOpen()    
    {
        if(!OptOn)
        {
            Opt.SetActive(true);
            OptOn = true;
        }
        else{
            Opt.SetActive(false);
            OptOn = false;
        }
    }
    public void DoPause()
    {
        if (stop)
        {
            stop = false;
            Opt.SetActive(false);
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
        Time.timeScale = 1;
        SceneManager.LoadScene("mainmenu");
    }
    public void DoOption()
    {
        
    }
    
}
