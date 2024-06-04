using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public static LevelManager instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //        Destroy(gameObject);
    //}


    public void FirstLevel()
    {
        int level = 0;
        LoadLevel(level);
    }

    public void LastLevel()
    {
        int level = SceneManager.sceneCountInBuildSettings - 1;
        LoadLevel(level);
    }

    public void NextLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        if (level > SceneManager.sceneCountInBuildSettings - 1) {
            level = SceneManager.sceneCountInBuildSettings - 1;
        }
        LoadLevel(level);
    }

    public void PreviousLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex - 1;
        if (level < 0 )
        {
            level = 0;
        }
        LoadLevel(level);
    }

    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
