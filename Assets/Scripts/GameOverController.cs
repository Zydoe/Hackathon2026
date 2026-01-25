using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public void Awake(){
        if (GameSession.Instance != null)
            GameSession.Instance.gameOverPanel = this.gameObject;
    }
    public void RestartGame()
    {
        
        Time.timeScale = 1f;
        print("RESTART");
        if (GameSession.Instance != null)

            GameSession.Instance.Restart();

    }

    public void GoToMenu()
    {
        print("Menu");
        Time.timeScale = 1f;

        if (GameSession.Instance != null)
            GameSession.Instance.QuitToMenu(0);

    }
}
