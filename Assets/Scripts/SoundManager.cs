using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    PlayerController playerController;

    public static SoundManager instance;

    public AudioMixer mixer;
    public AudioSource bgSound;
    public AudioClip[] bglist;
    public GameObject soundControlPanelInMainMenu;

    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                BgSoundPlay(bglist[i]);
        }
    }

    public void BGVolumeControl(float val)
    {
        mixer.SetFloat("BGSoundVolume", MathF.Log10(val) * 20);
    }

    public void SFXVolumeControl(float val)
    {
        mixer.SetFloat("SFXSoundVolume", MathF.Log10(val) * 20);
    }



    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.3f;
        bgSound.Play();
    }

    public void SFXPlayer(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFXSound")[0];
        audiosource.clip = clip;
        audiosource.volume = 0.15f;
        audiosource.Play();

        Destroy(go, clip.length);
    }


    public void OpenSoundControlPanel()
    {
        soundControlPanelInMainMenu.SetActive(true);
    }

    public void CloseSoundControllPanel()
    {
        soundControlPanelInMainMenu.SetActive(false);
    }

}
