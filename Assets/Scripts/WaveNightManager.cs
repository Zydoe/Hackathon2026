using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveNightManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string villageSceneName = "VillageScene";

    [Header("Refs")]
    [SerializeField] private WaveSpawnerFromCaves spawner;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI countdownText;

    [Header("Timing")]
    [SerializeField] private float prepTime = 6f; // 5–10 seconds recommended
    [SerializeField] private float timeBetweenWaves = 2f;
    [SerializeField] private float messageDisplayTime = 1.2f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip waveStartHorn;
    [SerializeField] private AudioClip waveEndHorn;

    [Header("Wave Presets")]
    [SerializeField] private List<WaveConfig> wavePresets = new List<WaveConfig>();

    private void Start()
    {
        if (GameSession.Instance == null)
        {
            var go = new GameObject("GameSession");
            go.AddComponent<GameSession>();
        }

        StartCoroutine(RunNight());
    }

    private IEnumerator RunNight()
    {
        int night = GameSession.Instance.NightNumber;
        int wavesThisNight = night;

        if (wavePresets.Count > 0)
            wavesThisNight = Mathf.Min(wavesThisNight, wavePresets.Count);

        yield return ShowMessage($"Night {night} begins!");

        for (int w = 0; w < wavesThisNight; w++)
        {
            int waveNum = w + 1;
            SetWaveUI(night, waveNum, wavesThisNight);

            WaveConfig wave = wavePresets[w];

            // ---- PREP PHASE ----
            yield return StartCoroutine(PrepCountdown(waveNum));

            // Horn at wave start
            PlayHorn(waveStartHorn);

            // Spawn wave
            yield return StartCoroutine(spawner.SpawnWave(wave));

            // Wait until cleared
            yield return new WaitUntil(() =>
                EnemyRegistry.Instance != null &&
                EnemyRegistry.Instance.AliveCount == 0
            );

            // Horn at wave end
            PlayHorn(waveEndHorn);

            yield return ShowMessage("Wave Complete!");
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        yield return ShowMessage("Night Complete!");

        GameSession.Instance.AdvanceNight();

        SceneManager.LoadScene(villageSceneName);
    }

    // ---------------- PREP COUNTDOWN ----------------

    private IEnumerator PrepCountdown(int waveNum)
    {
        ShowText($"Wave {waveNum} incoming...");

        if (countdownText != null)
            countdownText.gameObject.SetActive(true);

        float t = prepTime;

        while (t > 0)
        {
            if (countdownText != null)
                countdownText.text = Mathf.Ceil(t).ToString();

            yield return new WaitForSeconds(1f);
            t--;
        }

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        HideMessage();
    }

    // ---------------- UI HELPERS ----------------

    private IEnumerator ShowMessage(string msg)
    {
        ShowText(msg);
        yield return new WaitForSeconds(messageDisplayTime);
        HideMessage();
    }

    private void ShowText(string msg)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = msg;
        }
    }

    private void HideMessage()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    private void SetWaveUI(int night, int waveNum, int totalWaves)
    {
        if (waveText != null)
            waveText.text = $"Night {night}   Wave {waveNum}/{totalWaves}";
    }

    // ---------------- AUDIO ----------------

    private void PlayHorn(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}
