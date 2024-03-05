using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pitchButcherModule : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private List<GameObject> pitchButchers;

    private List<Vector3> pitchButchersScales = new List<Vector3>();

    private int price = 150;

    [SerializeField]
    private TMP_Text showPrice;

    [SerializeField]
    private Image moduleImage;

    [SerializeField]
    private Sprite[] moduleSprites;

    private bool available;

    private IEnumerator CheckAvailable() 
    {
        while (true)
        {
            showPrice.text = price.ToString();
            if (pitchButchers.Count <= 0)
            {
                showPrice.text = "MAX";
            }
            yield return new WaitForSeconds(0.3f);
            if (pitchButchers.Count > 0)
            {
                if (pitchGame.coins >= price)
                {
                    moduleImage.sprite = moduleSprites[1];
                    available = true;
                }
                else
                {
                    moduleImage.sprite = moduleSprites[0];
                    available = false;
                }
            }
        }
    }

    private void OnEnable()
    {
        available = false;
        foreach (var item in pitchButchers)
        {
            pitchButchersScales.Add(item.transform.localScale);
        }
        StartCoroutine(CheckAvailable());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!available)
            return;
        if (pitchButchers.Count <= 0)
            return;
        pitchGame.coins -= price;
        price *= 2;
        
        pitchButchers[0].gameObject.SetActive(true);
        pitchButchers[0].transform.localScale = Vector3.zero;
        pitchButchers[0].transform.DOScale(pitchButchersScales[0], 0.25f);
        pitchButchersScales.Remove(pitchButchersScales[0]);
        pitchButchers.Remove(pitchButchers[0]);
    }
}
