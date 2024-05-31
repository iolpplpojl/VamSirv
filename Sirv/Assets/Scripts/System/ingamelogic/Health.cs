using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TMP_Text Text;
    public Player player;
    public Scrollbar scrollbar;

    public void GetPlayer(Player player)
    {
        this.player = player;
    }
    private void Update()
    {
        Text.text = string.Format("{0} / {1}", player.health,player.maxHealthNow);
        scrollbar.size = (float)player.health / (float)player.maxHealthNow;
    }
}
