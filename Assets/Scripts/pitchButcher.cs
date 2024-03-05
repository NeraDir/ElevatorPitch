using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pitchButcher : MonoBehaviour
{
    public GameObject meatImage;

    public Transform targetToMove;

    public int countAdd;
    public AudioClip sound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out pitchTiger tiger))
        {

            StartCoroutine(Spawning());
        }
    }

    private IEnumerator Spawning()
    {
        for (int i = 0; i < countAdd; i++)
        {
            pitchGame.soundSourcePlayer.PlayOneShot(sound);
            GameObject tempMEat = Instantiate(meatImage, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity);
            tempMEat.transform.parent = targetToMove.parent;
            tempMEat.transform.localScale = Vector3.zero;
            tempMEat.transform.DOScale(Vector3.one, 0.25f).OnComplete(() => tempMEat.transform.DOMove(targetToMove.position, 0.25f).OnComplete(() => tempMEat.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { pitchGame.meats++; Destroy(tempMEat.gameObject); })));
            yield return new WaitForSeconds(0.25f);
        }
    }
}
