using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class pitchMenu : MonoBehaviour,IPointerClickHandler
{
    public Transform lastPageObject;

    private void OnEnable()
    {
        lastPageObject.DOScale(Vector3.one, 0.25f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        lastPageObject.DOScale(Vector3.zero, 0.25f).OnComplete(() => SceneManager.LoadScene("game"));
    }
}
