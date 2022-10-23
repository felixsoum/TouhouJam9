using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    private void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }
}
