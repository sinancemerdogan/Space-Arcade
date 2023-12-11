using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame() {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad("GameOver", 2f));
    }

    public void QuitGame() {
        Application.Quit();
    }

    private IEnumerator WaitAndLoad(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
