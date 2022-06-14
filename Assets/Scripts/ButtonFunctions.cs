/// <summary>
/// Button Functions
/// Joonas Erho, 12.6.2022
/// 
/// Contains functionality for main menu buttons.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    void Start() {
        playButton.onClick.AddListener(LoadLevel);
        quitButton.onClick.AddListener(Quit);
    }

    // Loads the main gameplay level.
    public void LoadLevel() {
        SceneManager.LoadScene("Gameplay");
    }

    public void Quit() {
        Application.Quit();
    }
}
