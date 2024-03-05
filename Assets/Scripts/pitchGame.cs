using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class pitchGame : MonoBehaviour
{
    public Transform[] gameStartObjects;

    public Transform[] gameDefaultObjects;

    public Transform gameCloseButton;

    public GameObject gameObjects;

    public List<pitchTiger> tigers = new List<pitchTiger>();

    private List<Vector3> objectsScale = new List<Vector3>();

    private bool canSpeedUp;

    public static int coins;

    public static int meats;

    public TMP_Text showMeats;

    public TMP_Text showCoins;

    public TMP_Text showTigerPrice;

    public TMP_Text showSpeedPrice;

    private int maxCountTigers = 3;

    private int maxSpeedUpCount = 3;

    private int speedPrice = 50;

    private int tigerPrice = 300;

    [SerializeField]
    private Image tigerModuleImage;

    [SerializeField]
    private Sprite[] tigerModuleSprites;

    [SerializeField]
    private Image speedModuleImage;

    [SerializeField]
    private Sprite[] speedModuleSprites;

    public Transform tigerSpawnPosition;

    public pitchTiger tigerPrefab;

    private bool speedAvialable;

    private bool tigersAvailable;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;

    public static AudioSource soundSourcePlayer;

    private void Start()
    {
        meats = 0;
        coins = 0;
        showSpeedPrice.text = speedPrice.ToString();
        showTigerPrice.text = tigerPrice.ToString();
        canSpeedUp = true;
        gameCloseButton.transform.localScale = Vector3.zero;
        foreach (var item in gameStartObjects)
        {
            item.localScale = Vector3.zero;
        }
        foreach (var item in gameDefaultObjects)
        {
            objectsScale.Add(item.localScale);
            item.localScale = Vector3.zero;
        }

        foreach (var item in gameStartObjects) 
        {
            item.DOScale(Vector3.one, 0.25f);
        }
        gameCloseButton.transform.DOScale(Vector3.one, 0.25f);

        musicSource.mute = pitchMenuManager.musicOffOn != 0 ? false : true;
        soundSource.mute = pitchMenuManager.soundOffOn != 0 ? false : true;

        soundSourcePlayer = soundSource;
    }

    public void OnClickUpSpeed()
    {
        if (meats < speedPrice)
            return;
        if (maxSpeedUpCount <= 0)
        {
            showSpeedPrice.text = "MAX";
            return;
        }
        maxSpeedUpCount--;
        meats -= speedPrice;
        speedPrice *= 2;
        showSpeedPrice.text = speedPrice.ToString();
        float tempSpeed = tigers[0].speed;
        tempSpeed += 1.5f;
        if (maxSpeedUpCount <= 0)
        {
            showSpeedPrice.text = "MAX";
        }
        foreach (var item in tigers)
        {
            item.speed = tempSpeed;
        }
    }

    public void OnClickAddTiger() 
    {
        if (coins < tigerPrice)
            return;
        if (maxCountTigers <= 0)
        {
            showTigerPrice.text = "MAX";
            return;
        }
        coins -= tigerPrice;
        maxCountTigers--;
        tigerPrice *= 2;
        showTigerPrice.text = tigerPrice.ToString();
        pitchTiger tempTiger = Instantiate(tigerPrefab, tigerSpawnPosition.position,Quaternion.identity);
        tempTiger.speed = tigers[0].speed;
        StartCoroutine(InitializatingTiger(tempTiger));
        if (maxCountTigers <= 0)
        {
            showTigerPrice.text = "MAX";
        }
    }

    private IEnumerator InitializatingTiger(pitchTiger tiger) 
    {
        yield return new WaitForSeconds(0.5f);
        tiger.moveTargets = tigers[0].moveTargets;
        tiger.Init();
        
        tigers.Add(tiger);
    }

    private void LateUpdate()
    {
        showCoins.text = coins.ToString();
        showMeats.text = meats.ToString();
        if (Input.GetMouseButtonDown(0) && canSpeedUp)
        {
            StartCoroutine(SpeedUping());
        }

        if (meats >= speedPrice && maxSpeedUpCount>0)
        {
            speedModuleImage.sprite = speedModuleSprites[1];
        }
        else
        {
            speedModuleImage.sprite = speedModuleSprites[0];
        }

        if (coins >= tigerPrice && maxCountTigers > 0)
        {
            tigerModuleImage.sprite = tigerModuleSprites[1];
        }
        else
        {
            tigerModuleImage.sprite = tigerModuleSprites[0];
        }
    }

    private IEnumerator SpeedUping()
    {
        canSpeedUp = false;
        foreach (var item in tigers)
        {
            item.speed *= 2;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (var item in tigers)
        {
            item.speed /= 2;
        }
        canSpeedUp = true;
    }

    public void OnClickStartGame() 
    {
        foreach (var item in gameStartObjects)
        {
            item.DOScale(Vector3.zero, 0.25f);
        }
        gameObjects.SetActive(true);
        for (int i = 0; i < gameDefaultObjects.Length; i++)
        {
            gameDefaultObjects[i].DOScale(objectsScale[i],0.25f);

        }
        StartCoroutine(Starter());
    }

    private IEnumerator Starter() 
    {
        yield return new WaitForSeconds(1);
        foreach (var item in tigers)
        {
            item.Init();
        }
    }

    public void OnClickExitGame() 
    {
        SceneManager.LoadScene("menu");
    }
}
