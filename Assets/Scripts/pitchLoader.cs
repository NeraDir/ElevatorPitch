using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pitchLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("menu");
    }
}
