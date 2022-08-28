using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void NextLevel()
    {
        StartCoroutine(TransitionSequence());
    }

    IEnumerator TransitionSequence()
    {
        yield return new WaitForSeconds(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ReturnToMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
