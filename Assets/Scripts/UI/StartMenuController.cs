using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }
}
