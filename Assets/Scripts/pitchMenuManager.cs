using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pitchMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject quetionPage;

    [SerializeField]
    private Transform quetionobject;

    [SerializeField]
    private string storeLink;

    [SerializeField]
    private Image[] starsImages;

    [SerializeField]
    private Sprite starDefault;

    [SerializeField]
    private Sprite starSelected;

    public static int musicOffOn
    {
        get
        {
            if (PlayerPrefs.HasKey("pitchMusicStateSaveKey"))
            {
                return PlayerPrefs.GetInt("pitchMusicStateSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("pitchMusicStateSaveKey", value);
        }
    }

    public static int soundOffOn
    {
        get
        {
            if (PlayerPrefs.HasKey("pitchSoundStateSaveKey"))
            {
                return PlayerPrefs.GetInt("pitchSoundStateSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("pitchSoundStateSaveKey", value);
        }
    }

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("pitchQuetionShowedSaveKey"))
        {
            quetionPage.SetActive(true);
            quetionobject.localScale = Vector3.zero;
            quetionobject.DOScale(Vector3.one, 0.25f);
            PlayerPrefs.SetInt("pitchQuetionShowedSaveKey", 1);
        }
        musicSource.mute = musicOffOn != 0 ? false : true;
        soundSource.mute = soundOffOn != 0 ? false : true;
    }

    public void ChangeSound(int value) 
    {
        soundOffOn = value;
        soundSource.mute = soundOffOn != 0 ? false : true;
    }

    public void ChangeMusic(int value) 
    {
        musicOffOn = value;
        musicSource.mute = musicOffOn != 0 ? false : true;
    }

    public void OnClickStarSelect(int starIndex) 
    {
        foreach (var item in starsImages)
        {
            item.sprite = starDefault;
        }
        for (int i = 0; i < starIndex; i++)
        {
            starsImages[i].sprite = starSelected;
        }
    }

    public void OnClickSubmit() 
    {
        quetionPage.SetActive(false);
        Application.OpenURL(storeLink);
    }
}
