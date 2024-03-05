using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitchBank : MonoBehaviour
{
    public GameObject coinsImage;

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
            GameObject tempCoin = Instantiate(coinsImage, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity);
            tempCoin.transform.parent = targetToMove.parent;
            tempCoin.transform.localScale = Vector3.zero;
            tempCoin.transform.DOScale(Vector3.one, 0.25f).OnComplete(() => tempCoin.transform.DOMove(targetToMove.position, 0.25f).OnComplete(() => tempCoin.transform.DOScale(Vector3.zero,0.25f).OnComplete(() => { pitchGame.coins++; Destroy(tempCoin.gameObject); })));
            yield return new WaitForSeconds(0.25f);
        }
    }
}
