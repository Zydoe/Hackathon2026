using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadExitTrigger : MonoBehaviour
{
    [SerializeField] private string nightSceneName = "NightScene";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nightSceneName);
        }
    }
}
