using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuController : MonoBehaviour
{
    public VisualElement ui;
    public Button PlayGameButton;
    public Button SettingsButton;
    public Button QuitButton;


    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        Debug.Log(" MenuController START is running");
    }



    private void OnEnable()
    {
        PlayGameButton = ui.Q<Button>("PlayGameButton");
        PlayGameButton.clicked += OnPlayButtonClicked;

        SettingsButton = ui.Q<Button>("Settings");
        SettingsButton.clicked += OnSettingsClicked;

        QuitButton = ui.Q<Button>("QuitButton");
        QuitButton.clicked += OnQuitButtonClicked;
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }

    private void OnSettingsClicked()
    {
        Debug.Log("Settings clicked");
    }

    private void OnPlayButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
