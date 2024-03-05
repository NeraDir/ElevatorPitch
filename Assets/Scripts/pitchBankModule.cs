using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class pitchBankModule : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private List<GameObject> pitchBank;

    private List<Vector3> pitchBankScales = new List<Vector3>();

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
            if (pitchBank.Count <= 0)
            {
                showPrice.text = "MAX";
            }
            yield return new WaitForSeconds(0.3f);
            if (pitchBank.Count > 0)
            {
                if (pitchGame.meats >= price)
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
        foreach (var item in pitchBank)
        {
            pitchBankScales.Add(item.transform.localScale);
        }
        StartCoroutine(CheckAvailable());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!available)
            return;
        if (pitchBank.Count <= 0)
            return;
       
        pitchGame.meats -= price;
        price *= 2;
        pitchBank[0].gameObject.SetActive(true);
        pitchBank[0].transform.localScale = Vector3.zero;
        pitchBank[0].transform.DOScale(pitchBankScales[0], 0.25f);
        pitchBankScales.Remove(pitchBankScales[0]);
        pitchBank.Remove(pitchBank[0]);
    }
}
