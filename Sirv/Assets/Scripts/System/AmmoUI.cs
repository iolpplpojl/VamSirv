using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    // Start is called before the first frame update


    Player Player;
    TMP_Text Text;
    // Update is called once per frame
    private void Start()
    {
       Text = GetComponentInChildren<TMP_Text>();
    }
    void Update()
    {
        float Originalwidth = 1920;
        float Originalheight = 1080;
        transform.position = new Vector3(Input.mousePosition.x + (70 * (Screen.width / Originalwidth)), Input.mousePosition.y + (-40 * (Screen.height / Originalheight)), 0f);
         if(Player != null)
         {
             Text.text = string.Format("{0}/{1}",Player.ammo,Player.maxammonow);
         }
        
    }
    public void getData()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
}
