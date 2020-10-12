using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
     public void LoadNextScene()
    {
        int lastPlayableIndex = SceneManager.sceneCountInBuildSettings - 3;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != lastPlayableIndex)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("GameWin");
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        GameStatus gameStatus = FindObjectOfType<GameStatus>();
        gameStatus.DestroyGameStatus();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
  
}
