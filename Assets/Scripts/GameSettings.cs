using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    [SerializeField] Slider timeSlider;
    [SerializeField] Text speedText;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject quitPanel;

    public BallGeneration ballGeneration;
    public CharacterController characterController;
    public BallDifficulty ballDifficulty;
    public UIAlignement uiAlignement;

    bool gameStarted = false;
    bool ballGenerated = false;
    float timer = 0;
    

    // Update is called once per frame
    void Start()
    {
        ballGeneration = FindObjectOfType<BallGeneration>();
        characterController = FindObjectOfType<CharacterController>();
        ballDifficulty = FindObjectOfType<BallDifficulty>();
        uiAlignement = FindObjectOfType<UIAlignement>();
        Time.timeScale = 1;
    }

    public void Update()
    {
       
        if (gameStarted)
        {
            GameStateChecker();

            if (!ballGenerated)
            {
                ballGeneration.BallSpawner();
                ballGenerated = true;
            }
            StartCoroutine(DelayedMethod());
            ballDifficulty.BallDifficultySetStart();
            characterController.BallCollectionStart();

        }
    }

 

  

    public void TimeSlider()
    {
        Time.timeScale = timeSlider.value;
        speedText.text = "x"+timeSlider.value.ToString("F0");
    }

    public void GameStart()
    {
        gameStarted = true;
        startButton.SetActive(false);
        restartButton.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator DelayedMethod()
    {
        yield return new WaitForSeconds(2); // Wait for 3 seconds
        ExecuteAfterDelay(); // Call the method after the delay
    }

    private void ExecuteAfterDelay()
    {
        uiAlignement.CameraSettings();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void GameStateChecker()
    {
        if (characterController.healthBar.value < 1f)
        {
            GameOver();
        }
    }

    public void YesButton()
    {
        Application.Quit();
    }
    public void NoButton()
    {
        quitPanel.SetActive(false);
        Time.timeScale = timeSlider.value;
    }
    public void QuitButton()
    {
        quitPanel.SetActive(true);
        Time.timeScale = 0;
    }


}
