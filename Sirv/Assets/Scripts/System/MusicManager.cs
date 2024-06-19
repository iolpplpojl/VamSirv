using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static MusicManager instance;

    public AudioClip[] songs;

    public AudioSource source;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }   
        DontDestroyOnLoad(gameObject);
    }

    public void Sing(string situation)
    {
        switch (situation) {
            case "Fight":
                source.clip = songs[0];
                source.Play();
                break;
            case "Main":
                source.clip= songs[1];
                source.Play();
                break;
        }
            
    }
}
