using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public int NightNumber { get; private set; } = 1;



    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    private bool ended = false;


    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // find even if inactive
        var goc = FindAnyObjectByType<GameOverController>(FindObjectsInactive.Include);
        if (goc != null) gameOverPanel = goc.gameObject;

        // (optional) same idea for win panel if you have a WinController
        // var wc = FindAnyObjectByType<WinController>(FindObjectsInactive.Include);
        // if (wc != null) winPanel = wc.gameObject;

        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (winPanel) winPanel.SetActive(false);

        ended = false;
        Time.timeScale = 1f;
    }
    public void AdvanceNight()
    {
        NightNumber++;
    }
    void Start()
    {
        HideAll();
    }

    public void GameOver()
    {
        if (ended) return;
        ended = true;

        Time.timeScale = 0f; // pause game
        HideAll();
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        if (ended) return;
        ended = true;

        Time.timeScale = 0f;
        HideAll();
        if (winPanel) winPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        ended = false;
        HideAll();
        NightNumber = 1;
    
        if (Player.Instance != null)
        Destroy(Player.Instance.gameObject);

        var inv = FindObjectOfType<Inventory>();
        if (inv != null)
        Destroy(inv.gameObject);

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu(int menuBuildIndex)
    {
        Time.timeScale = 1f;
        ended = false;
        HideAll();
        SceneManager.LoadScene(menuBuildIndex);
    }

    private void ResetGameState()
    {
        // Reset player stats
        if (Player.Instance != null)
        {
            Player.Instance.ResetStats();
            Player.Instance.gameObject.SetActive(true);
        }
        NightNumber = 0;
        // Reset EnemyRegistry
        // var reg = FindObjectOfType<EnemyRegistry>();
        // if (reg != null)
        // {
        //     reg.ResetCount(); // we'll add this below
        // }

        // // Reset night/wave manager (example)
        // var night = FindObjectOfType<WaveNightManager>();
        // if (night != null)
        // {
        //     night.ResetRun();
        // }
    }


    private void HideAll()
    {
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (winPanel) winPanel.SetActive(false);
    }
}
