using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Option : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    public int resolutionNum;

    public Slider sfxSound;
    public Slider songSound;
    public Image dmgBtn;
    public bool showDamage;

    // Start is called before the first frame update

    [System.Obsolete]
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
        InitUI();
        
    }

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

    [System.Obsolete]
    void InitUI()
    {
        for(int i = 0; i<Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                resolutions.Add(Screen.resolutions[i]);
        }
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " x " + item.height;
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    
    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,screenMode);
    }
}
