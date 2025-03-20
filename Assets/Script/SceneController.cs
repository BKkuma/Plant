using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // โหลดฉากโดยใช้ชื่อ
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // โหลดฉากโดยใช้ Index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // ออกจากเกม
    public void QuitGame()
    {
        Debug.Log("Game Quit!"); // ใช้ Debug เช็คตอนทดสอบใน Unity Editor
        Application.Quit();
    }
}
