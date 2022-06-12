using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Unity Variables
    // SymbolSet is a custom class that contains the data used in the level.
    public SymbolSet set;
    public SpriteRenderer srChanging;
    public SpriteRenderer srTarget;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelName;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI finalScoreText;
    public HealthBar healthBar;
    private Coroutine coroutine;
    
    
    // Delay between symbol changes on the constantly changing symbol.
    public float startingTimeDelay;

    // Base change of delay as score increases.
    public float delayChange;

    // Amount that change is multiplied by as game progresses. (less than 1)
    public float delayMultiplier;

    // The delay can never go below this value
    public float minimumDelay;


    // Amount of sprites in our set. Calculated once at start for convenience.
    private int spriteAmount;


    // The ID (index) of each symbol. Changes as the sprites change.
    [SerializeField] private int changingId = -1;
    [SerializeField] private int targetId = -1;

    [SerializeField] private float currentTimeDelay;


    // Game Variables
    private bool gameOver = false;
    private int score = 0;

    public void Start() {
        // Set level name to appear.
        levelName.text = set.setName;

        // Initialize values.
        currentTimeDelay = startingTimeDelay;
        spriteAmount = set.sprites.Length;

        // Randomly assign target symbol.
        ChangeTargetSymbol();

        // Set values for health bar.
        healthBar.UpdateValues(currentTimeDelay, spriteAmount);

        // Start the coroutine that constantly changes the left-hand symbols.
        coroutine = StartCoroutine(ChangeSymbols());
    }

    public void Update() {
        // Check for SPACEBAR keypress.
        if (Input.GetKeyDown("space")) {
            // If the symbols were the same, increase score and speed, reset timer and change symbol.
            if (changingId == targetId) {
                // Increase score and update text.
                score++;
                scoreText.text = score.ToString();

                // Decrease delay. Also decrease the change in delay, so that the amount of milliseconds
                // that the delay is changed scales down.
                currentTimeDelay -= delayChange;
                delayChange *= delayMultiplier;

                // If someone is so crazy that they manage to get so far that the time delay would go
                // below minimum, clamp it.
                if (currentTimeDelay < minimumDelay) currentTimeDelay = minimumDelay;

                // Reset the health bar.
                healthBar.UpdateValues(currentTimeDelay, spriteAmount);

                // Change the target symbol (symbol on right).
                ChangeTargetSymbol();
            }
            else {
                // Otherwise game is lost.
                GameOver();
            }
        }
    }

    /// <summary>
    /// Coroutine that changes the left-hand symbol.
    /// </summary>
    /// <returns>Nothing.</returns>
    IEnumerator ChangeSymbols() {
        // Create a list of sprites that have already been shown.
        List<int> spritesShown = new List<int>();
        while(!gameOver) {
            // Make sure the new value is different from the past one.
            int currentId = changingId;
            while(true) {
                changingId = (Random.Range(0, spriteAmount) + 1) % spriteAmount;
                if (spritesShown.Contains(changingId) || changingId == currentId) {
                    continue;
                }
                else {
                    spritesShown.Add(changingId);
                    break;
                }
            };
            srChanging.sprite = set.sprites[changingId];
            if (spritesShown.Count == spriteAmount) {
                spritesShown.Clear();
            }
            yield return new WaitForSeconds(currentTimeDelay);
        } 
    }

    /// <summary>
    /// Changes the right-hand symbol (target symbol).
    /// </summary>
    private void ChangeTargetSymbol() {
        // Make sure the new value is different from the past one.
        int currentId = targetId;
        do {
            targetId = Random.Range(0, spriteAmount);
        } while(currentId == targetId);
        srTarget.sprite = set.sprites[targetId];
    }

    /// <summary>
    /// Does necessary actions when game is over, such as displaying the appropriate screen.
    /// </summary>
    public void GameOver() {
        gameOver = true;
        StopCoroutine(coroutine);
        finalScoreText.text = score.ToString();
        gameOverCanvas.SetActive(true);
    }

    public bool IsGameOver() {
        return gameOver;
    }
}
