using UnityEngine;
using TMPro;
using System.Collections.Generic; // Needed for using Lists
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenu;
    public GameObject gameoverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stateText; // Reference to the UI text component that shows the game's state
    private bool isPaused = false; // Track whether the game is paused
    private List<AudioSource> audioSources = new List<AudioSource>(); // List to keep track of all audio sources

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // This keeps the instance alive across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        pauseMenu.SetActive(false);

        // Find all audio sources in the scene and add them to the list
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        audioSources.AddRange(sources);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the Escape key press to toggle pause state
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the pause state
            isPaused = !isPaused;

            if (isPaused)
            {
                // Game is now paused
                Time.timeScale = 0f; // Pause the game by setting time scale to 0
                stateText.text = "PAUSE"; // Update the UI text to indicate the game is paused
                pauseMenu.SetActive(true);
                PauseAudio(); // Pause all audio sources
            }
            else
            {
                // Game is now playing/resumed
                Time.timeScale = 1f; // Resume the game by setting time scale back to 1
                stateText.text = "PLAY"; // Update the UI text to indicate the game is playing
                pauseMenu?.SetActive(false);
                ResumeAudio(); // Resume all audio sources
            }
        }
    }

    // Method to pause all audio sources
    void PauseAudio()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                source.Pause(); // Pause the audio source if it is currently playing
            }
        }
    }

    // Method to resume all audio sources
    void ResumeAudio()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.UnPause(); // Unpause the audio source if it is not currently playing
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game by setting time scale back to 1
        stateText.text = "PLAY"; // Update the UI text to indicate the game is playing
        pauseMenu?.SetActive(false);
        ResumeAudio(); // Resume all audio sources
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

        // Reset all values smh
        SpeedController.Instance.currentSpeed = 1.5f;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameoverScreen.SetActive(true);
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score.ToString();
    }
}
