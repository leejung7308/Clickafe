using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    AudioSource bgm_player;
    AudioSource sfx_player;
    public AudioClip[] audioClips;
    public static SoundManager instance;
    public Slider bgmSlider;
    public Slider sfxSlider;
    void Awake()
    {
        instance = this;
        bgm_player = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        sfx_player = GameObject.Find("SFX Player").GetComponent<AudioSource>();
        bgmSlider = bgmSlider.GetComponent<Slider>();
        sfxSlider = sfxSlider.GetComponent<Slider>();
        bgmSlider.onValueChanged.AddListener(ChangeBgmSound);
        sfxSlider.onValueChanged.AddListener(ChangeSfxSound);
        bgmSlider.value = DataController.Instance.gameData.bgmVol;
        sfxSlider.value = DataController.Instance.gameData.sfxVol;
    }
    public void PlaySound(string type)
    {
        int index = 0;
        switch (type)
        {
            case "Button": index = 0; break;
            case "Coin": index = 1; break;
            case "Purchase": index = 2; break;
        }
        sfx_player.PlayOneShot(audioClips[index]);
    }
    void ChangeBgmSound(float value)
    {
        bgm_player.volume = value;
        DataController.Instance.gameData.bgmVol = value;
    }
    void ChangeSfxSound(float value)
    {
        sfx_player.volume = value;
        DataController.Instance.gameData.sfxVol = value;
    }
}
