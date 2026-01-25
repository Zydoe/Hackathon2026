using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageManager : MonoBehaviour
{
    [SerializeField] private string nightSceneName = "NightScene";

    private void Start()
    {
        // Ensure session exists
        if (GameSession.Instance == null)
        {
            var go = new GameObject("GameSession");
            go.AddComponent<GameSession>();
        }
    }

    public void StartNextNight()
    {
        SceneManager.LoadScene(nightSceneName);
    }
}
