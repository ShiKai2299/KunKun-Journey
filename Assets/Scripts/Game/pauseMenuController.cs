using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameWindow gameWindow;
    public Animator transitionAnim;
    public AudioSource bgmAudioSource;

    public void Pause() 
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Home() 
    {
        transitionAnim.SetTrigger("End");
        pauseMenu.SetActive(false);
        gameWindow.delLevel();
        gameWindow.Start();
        Time.timeScale = 1;
        transitionAnim.SetTrigger("Start");
    }
    
    public void Resume() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        bgmAudioSource.UnPause(); // Resume BGM from where it was paused
    }

    public void Restart()
    {
        transitionAnim.SetTrigger("End");
        pauseMenu.SetActive(false);
        gameWindow.respawn();
        Time.timeScale = 1;
        transitionAnim.SetTrigger("Start");
    }
}
