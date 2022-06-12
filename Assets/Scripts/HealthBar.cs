/// <summary>
/// Health Bar
/// Joonas Erho, 2022
/// 
/// This class controls the health bar visible in the gameplay screen.
/// It updates it's color and width with values given from GameController.
/// This class is also responsible for ending the game if time runs out.
/// </summary>
/// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Unity Variables
    public Image barImage;
    public RectTransform rt;
    public GameController gc;

    // Values used to correctly change the look of the bar.
    private readonly Color startColor = new Color(0.9f, 0.8f, 0.8f, 1.0f);
    private readonly Color endColor = new Color(0.85f, 0.2f, 0.2f, 1.0f);
    private readonly float fullWidth = 1000f;
    private readonly float height = 75f;
    
    // The total time the player has to react to the current symbol.
    private float maxTime;

    // How much of that time is left.
    private float currentTime;

    /// <summary>
    /// Updates the values needed by this class to show and determine how much
    /// time the player has left.
    /// 
    /// Sets the time that the player has so that the player can only afford
    /// to miss once symbol. (If there are 4 symbols and it takes 0.5 seconds)
    /// for the symbol to change, the player would have 4 seconds to react.)
    /// </summary>
    /// <param name="changeTime">Amount of time it takes for a symbol to change.</param>
    /// <param name="symbolAmount">Amount of symbols in set.</param>
    public void UpdateValues(float changeTime, int symbolAmount) {
        maxTime = changeTime * symbolAmount * 2;
        currentTime = maxTime;

        // Also reset the values of the bar.
        barImage.color = startColor;
        rt.sizeDelta = new Vector2(fullWidth, height);
    }

    void Update() {
        // Since game might also end due to player mistiming, check if game has
        // ended first.
        if (gc.IsGameOver()) return;
        
        // Remove the time that has elapsed since last frame.
        currentTime = currentTime - Time.deltaTime;

        // If the player ran out of time this frame, game ends.
        if (currentTime <= 0) {
            gc.GameOver();
            return;
        }

        // Change the color and the width of the health bar to reflect time.
        barImage.color = Color.Lerp(endColor, startColor, (currentTime / maxTime));
        float width = Mathf.Lerp(0f, fullWidth, (currentTime / maxTime));
        rt.sizeDelta = new Vector2(width, height);
    }
}
