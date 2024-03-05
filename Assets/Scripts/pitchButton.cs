using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class pitchButton : MonoBehaviour, IPointerClickHandler
{
    private bool canClick;

    public Transform mineToDo;

    public Transform[] ToZeroing;

    public Transform openingPage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick)
            return;
        canClick = false;
        DoOpening(openingPage);
    }

    public void DoOpening(Transform openPage)
    {
        foreach (var item in ToZeroing)
        {
            item.DOScale(Vector3.zero, 0.25f).OnComplete(()=> { mineToDo.parent.gameObject.SetActive(false); openPage.gameObject.SetActive(true); });
        }
    }

    private void OnEnable()
    {
        mineToDo.DOScale(Vector3.one, 0.25f).OnComplete(() => canClick=true);
    }
}
