using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        if (SceneExists(GetCurrentSceneIndex() + 1))
        {
            SceneManager.LoadScene(GetCurrentSceneIndex() + 1);
        }
    }

    public void LoadSceneWithIndex(int buildIndex)
    {
        if (SceneExists(buildIndex))
        {
            SceneManager.LoadScene(buildIndex);
        }
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private bool SceneExists(int buildIndex)
    {
        return SceneManager.GetSceneByBuildIndex(buildIndex) != null;
    }
}
