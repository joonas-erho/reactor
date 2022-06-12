using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    void Start() {
        playButton.onClick.AddListener(LoadLevelSelect);
    }

    public void LoadLevelSelect() {
        Debug.Log("yee");
    }

    public void Quit() {
        Application.Quit();
    }
}
