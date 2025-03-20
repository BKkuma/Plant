using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // ��Ŵ�ҡ�������
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ��Ŵ�ҡ���� Index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // �͡�ҡ��
    public void QuitGame()
    {
        Debug.Log("Game Quit!"); // �� Debug �礵͹���ͺ� Unity Editor
        Application.Quit();
    }
}
