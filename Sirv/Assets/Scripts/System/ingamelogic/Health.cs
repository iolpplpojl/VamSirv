using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TMP_Text Text;
    public Player player;


    public void GetPlayer(Player player)
    {
        this.player = player;
    }
    private void Update()
    {
        Debug.Log(player + "asdkln" + Text + "asdkln");
        Text.text = string.Format("HP : {0}", player.health);
    }
}
