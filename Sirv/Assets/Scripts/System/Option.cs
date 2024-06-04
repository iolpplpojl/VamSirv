using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Option : MonoBehaviour
{
    public Slider sfxSound;
    public Slider songSound;
    public Image dmgBtn;
    public bool showDamage;

    // Start is called before the first frame update
    void Start()
    {
        sfxSound.value = SaveSystem.instance.data.sfxSound;
        songSound.value = SaveSystem.instance.data.songSound;
        showDamage = SaveSystem.instance.data.showDmg;
        if (showDamage)
        {
            dmgBtn.color = Color.red;
        }
        else
        {
            dmgBtn.color = Color.white;
        }
    }

    // Update is called once per frame
    public void sfxSoundChange()
    {
        SaveSystem.instance.setAudio("SFX",sfxSound.value);
    }
    public void songSoundChange()
    {
        SaveSystem.instance.setAudio("SONG", songSound.value);
    }
    public void showDamageChange()
    {
        showDamage = !showDamage;
        if (showDamage)
        {
            dmgBtn.color = Color.red;
        }
        else
        {
            dmgBtn.color = Color.white;
        }
        SaveSystem.instance.data.showDmg = showDamage;

    }
}
