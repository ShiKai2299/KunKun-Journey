using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : WindowRoot
{
    public GameObject player;
    public GameObject[] levels;
    public GameObject gameOverTip; 
    public pauseMenuController pauseMenu;
    public AudioSource bgmAudioSource;

    private GameObject keyIconPlay;
    private GameObject keyIconExit;

    public int levelCount;
    private bool isDead;
    private bool isPlayerNearby;
    private string objectTag;

    public Animator transitionAnim;

    public void Start()
    {
        levelCount = 0;
        gameStart();
        loadLevel();

        pauseMenu.gameObject.SetActive(false);

        // Find key icons by tag
        keyIconPlay = GameObject.Find("f_button_play");
        keyIconExit = GameObject.Find("f_button_exit");

        // Hide key icons at start
        if (keyIconPlay) keyIconPlay.SetActive(false);
        if (keyIconExit) keyIconExit.SetActive(false);
    }

    private void gameStart()
    {
        isDead = false;
        player.SetActive(true);
        gameOverTip.SetActive(false);
    }

    public void delLevel()
    {
        Transform currentLevel = transform.Find("Level" + levelCount);
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject.gameObject);
        }
    }

    public void nextLevel()
    {
        transitionAnim.SetTrigger("End");
        delLevel();
        levelCount++;
        loadLevel();
        transitionAnim.SetTrigger("Start");
    }

    public void gameOver()
    {
        isDead = true;
        Time.timeScale = 0;
        player.SetActive(false);
        gameOverTip.SetActive(true);  // Show GameOver Tip
        bgmAudioSource.Pause(); // Pause the BGM
    }

    public void loadLevel()
    {
        if (levelCount >= levels.Length)
        {
            Start();
            return;
        }

        // Load the current level and set it as a child of GameWindow
        GameObject level = Instantiate(levels[levelCount]);
        level.name = "Level" + levelCount;
        level.transform.SetParent(transform, false);

        // Position the player at the StartPoint of the new level
        Transform start = level.transform.Find("StartPoint");
        if (start != null)
        {
            player.transform.localPosition = start.localPosition;
            bgmAudioSource.UnPause(); // Resume BGM from where it was paused
        }
    }

    private void Update()
    {
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            respawn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.Resume();
                bgmAudioSource.UnPause();
            }
            else
            {
                pauseMenu.Pause();
                bgmAudioSource.Pause(); // Pause the BGM
            }
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            if (objectTag == "ExitGameSign")
            {
                Application.Quit();
            }
            else if (objectTag == "StartGameSign")
            {
                nextLevel();
            }
        }
    }

    public void respawn()
    {
        gameStart();
        delLevel();
        loadLevel();

        // Reassign key icons
        keyIconPlay = GameObject.Find("f_button_play");
        keyIconExit = GameObject.Find("f_button_exit");

        // Hide them again at restart
        if (keyIconPlay) keyIconPlay.SetActive(false);
        if (keyIconExit) keyIconExit.SetActive(false);

        Time.timeScale = 1;
    }

    public void signInteraction(string signTag, bool isEntering)
    {
        isPlayerNearby = isEntering;
        objectTag = signTag; // Store the tag for Update() handling

        if (signTag == "StartGameSign")
        {
            keyIconPlay.SetActive(isEntering);
        }
        else if (signTag == "ExitGameSign")
        {
            keyIconExit.SetActive(isEntering);
        }
    }
}
