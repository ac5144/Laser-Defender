using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] float delayInSeconds = 1f;

    public void LoadGameScene() {

        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {

        Application.Quit();
    }

    public void LoadStartScene() {

        SceneManager.LoadScene("Start");
    }

    public void LoadGameOver() {

        StartCoroutine(WaitAndLoad());
    }

    public void LoadGameWin() {

        StartCoroutine(WaitAndLoadWin());
    }

    IEnumerator WaitAndLoad() {

        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    IEnumerator WaitAndLoadWin() {

        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Win");
    }
}
