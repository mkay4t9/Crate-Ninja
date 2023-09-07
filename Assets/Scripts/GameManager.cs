using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetPrefabs;
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    private int Score = 0;
    private int Lives = 3;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject displayInGameUI;
    public GameObject pauseMenu;
    public bool gameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!gameIsPaused)
            {
                gameIsPaused = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                gameIsPaused = false;
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);
            }
        }
    }

    IEnumerator spawnTargets()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[randomIndex]);
        }
    }

    public void updateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score : " + Score;
    }

    public void updateLives()
    {
        if(Lives >= 0)
            livesText.text = "Lives : " + Lives;
    }

    public void GameOver()
    {
        Lives--;
        updateLives();
        if(Lives == 0)
        {
            restartButton.gameObject.SetActive(true);
            gameoverText.gameObject.SetActive(true);
            isGameActive = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame(int difficulty)
    {
        isGameActive = true;
        spawnRate /= difficulty;
        StartCoroutine(spawnTargets());
        Score = 0;
        updateScore(0);
        updateLives();
        titleScreen.SetActive(false);
        displayInGameUI.SetActive(true);
    }
}
