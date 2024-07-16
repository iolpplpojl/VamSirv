using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillUI : MonoBehaviour
{
    // Start is called before the first frame update
    Player Player;
    public TMP_Text AText;
    public TMP_Text BText;
    public Image APanel;
    public Image BPanel;
    public Image AIcon;
    public Image BIcon;

    private void LateUpdate()
    {
        if (Player != null)
        {
            if (Player.skillAcooltimenow > 0)
            {
                AText.alpha = 1.0f;
                AText.text = Player.skillAcooltimenow.ToString("0.0");
                APanel.color = APanel.color - new Color(0f, 0.5f, 0.5f, 0f);
                AIcon.color = AIcon.color - new Color(0f, 0.5f, 0.5f, 0f);
            }
            else
            {

                AText.alpha = 0;
                APanel.color = new Color(1f, 1f, 1f);
                AIcon.color = new Color(1f, 1f, 1f);

            }

            if (Player.skillBcooltimenow > 0)
            {
                BText.alpha = 1.0f;
                BText.text = Player.skillBcooltimenow.ToString("0.0");
                BPanel.color = BPanel.color - new Color(0f, 0.5f, 0.5f, 0f);
                BIcon.color = BIcon.color - new Color(0f, 0.5f, 0.5f, 0f);

            }
            else
            {
                BText.alpha = 0;
                BPanel.color = new Color(1f, 1f, 1f);
                BIcon.color = new Color(1f, 1f, 1f);
            }
        }
    }

    // Update is called once per frame
    public void getData()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        AIcon.sprite = Player.SkillIcon[0];
        BIcon.sprite = Player.SkillIcon[1];
    }
}
