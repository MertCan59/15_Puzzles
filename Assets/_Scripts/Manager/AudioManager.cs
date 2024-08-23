using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Audio[] sfxAudio;
    public AudioSource sfxSource;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);  
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void PlaySfx(string sfxName)
    {
        Audio a = Array.Find(sfxAudio, x => x.audioName == sfxName);
        if (a != null)
        {
            sfxSource.PlayOneShot(a.clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found!");
        }
    }
}