using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Resultscene : MonoBehaviour
{
    // Start is called before the first frame update
    public static Resultscene instance;
    public TMP_Text kill;
    public TMP_Text gold;
    public TMP_Text exp;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    public void setText(int kill, int gold, int exp)
    {
        this.kill.text = kill.ToString();
        this.gold.text = gold.ToString();
        this.exp.text = exp.ToString();
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("mainmenu");
        }
    }
}
