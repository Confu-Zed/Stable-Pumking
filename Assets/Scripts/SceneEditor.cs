using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEditor : MonoBehaviour
{
    public int AvailableScene { get; set; } = 2;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(int sceneId)
    {
        if (AvailableScene >= sceneId)
            SceneManager.LoadScene(sceneId);
    }
    public void LevelPass()
    {
        AvailableScene++;
    }
}
