using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEditor : MonoBehaviour
{
    static int availableScene = 2;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
}
