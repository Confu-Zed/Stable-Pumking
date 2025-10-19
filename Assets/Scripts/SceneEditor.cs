using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneEditor : MonoBehaviour
{
    static int availableScene = 3;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadScene(1);
    }
    public void LoadScene(int sceneId)
    {
        if (availableScene >= sceneId)
            SceneManager.LoadScene(sceneId);
    }
    public void LevelPass()
    {
        availableScene++;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene(6);
    }
}
