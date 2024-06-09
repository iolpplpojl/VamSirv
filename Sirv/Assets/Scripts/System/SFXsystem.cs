using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXsystem : MonoBehaviour
{
    public static SFXsystem instance;
    public AudioSource SFXobj;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    public void PlaySoundFX(AudioClip source, Transform spawnTransform, float volume)
    {
        AudioSource m_SFX = Instantiate(SFXobj, spawnTransform.position, Quaternion.identity);
        m_SFX.clip = source;
        m_SFX.volume = volume;
        m_SFX.Play();
        float clipLength = m_SFX.clip.length;
        Destroy(m_SFX.gameObject, clipLength);
    }
    public AudioSource PlaySoundFX(AudioClip source, Transform spawnTransform, float volume, bool ret, bool loop)
    {
        AudioSource m_SFX = Instantiate(SFXobj, spawnTransform.position, Quaternion.identity);
        if (loop)
        {
            m_SFX.loop = true;
        }
        m_SFX.clip = source;
        m_SFX.volume = volume;
        m_SFX.Play();
        float clipLength = m_SFX.clip.length;
        if (loop == false)
        {
            Destroy(m_SFX.gameObject, clipLength);
        }
        return m_SFX;
    }
    public void PlaySoundFX(AudioClip[] source, Transform spawnTransform, float volume)
    {
        AudioSource m_SFX = Instantiate(SFXobj, spawnTransform.position, Quaternion.identity);
        m_SFX.clip = source[Random.Range(0, source.Length)];
        m_SFX.volume = volume;
        m_SFX.Play();
        float clipLength = m_SFX.clip.length;
        Destroy(m_SFX.gameObject, clipLength);
    }
    public void PlaySoundFX(AudioClip source, Transform spawnTransform, float volume, float time)
    {
        AudioSource m_SFX = Instantiate(SFXobj, spawnTransform.position, Quaternion.identity);
        m_SFX.clip = source;
        m_SFX.volume = volume;
        m_SFX.Play();
        Destroy(m_SFX.gameObject, time);
    }
}
