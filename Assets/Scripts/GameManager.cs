using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager>
{
    public static UnityEvent<int> scoreGained;
    public PlayerController playerController;
    public Transform playerSpawnPos;
    public AudioClip lifeLostSFX;
    public AudioMixerSnapshot defaultMixerSnapshot;
    public AudioMixerSnapshot gameoverMixerSnapshot;
    public int currentScore = 0;
    public int difficulty = 0;

    public int highestScore;
    public bool gameOver = false;
    public HealthScript _health;


    void Start()
    {
        highestScore = PlayerPrefs.GetInt("High Score");
        gameOver = false;
        _health = GetComponent<HealthScript>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }


    public void IncrementCurrentScore(int _add)
    {
        currentScore += _add;
    }

    private void DifficultyRefresh()
    {
        int _threshold = currentScore / 4000;
        if (_threshold > difficulty)
        {
            difficulty++;
        }
    }

    public void SaveScorePref()
    {
        if (currentScore < highestScore) //high score is not beat
        {
            
        }
        else //high score is beat
        {
            highestScore = currentScore;
            PlayerPrefs.SetInt("High Score", currentScore);
        }
    }

    public void PlayerHurt()
    {
        if (_health.isDead || _health.currentHealth <= 1)
        {
            StartCoroutine("ShowPlayerExplosion");
        }
        else
        {
            SFXManager.instance.PlaySFX(lifeLostSFX, transform, 1f);
            playerController.animator.SetTrigger("hit");
            _health.TakeDamage();
        }
    }
    IEnumerator ShowPlayerExplosion()
    {
        GameObject _playerExplode = Instantiate(playerController.explosionPrefab, playerController.transform.position, Quaternion.identity);
        gameoverMixerSnapshot.TransitionTo(0.4f);
        yield return new WaitForSeconds(0.15f);
        GameOver();

    }
    public void GameOver() //wondering how to show player game over (making the player stay still is boring, making tank explode would be funny)
    {
        gameOver = true;
        playerController.animator.SetBool("dead", true);
        playerController.gameObject.SetActive(false);
        Time.timeScale = 0f; //looks better tweened 
        SaveScorePref();
    }


    public void ResetGame()
{
        SceneManager.LoadSceneAsync(0);
        gameOver = false;
        _health.ResetHealth();
    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("High Score", 0);
        highestScore = 0;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerController = FindAnyObjectByType<PlayerController>();
        currentScore = 0;
        highestScore = PlayerPrefs.GetInt("High Score");
        gameOver = false;
        playerController.gameObject.SetActive(true);
        Time.timeScale = 1f;
        defaultMixerSnapshot.TransitionTo(0.1f);
        Debug.Log("Scene loaded");
        
    }
    //private void OnGUI()
    //{
    //    GUIStyle _guiStyle = new GUIStyle();
    //    _guiStyle.fontSize = 18;
    //    _guiStyle.fontStyle = FontStyle.Normal;
    //    _guiStyle.normal.textColor = Color.white;

    //    if (playerController != null)
    //    {
    //        GUI.Label(new Rect(10f, 130f, 10f, 10f), $"Speed: {playerController.rb.linearVelocityY}", _guiStyle);
    //    }

    //    GUI.Label(new Rect(10f, 150f, 10f, 10f), $"HP: {_health.currentHealth}", _guiStyle);
    //    GUI.Label(new Rect(10f, 170f, 10f, 10f), $"Score: {this.currentScore}", _guiStyle);
    //    GUI.Label(new Rect(10f, 190f, 10f, 10f), $"Highscore: {this.highestScore}", _guiStyle);
    //    GUI.Label(new Rect(10f, 210f, 10f, 10f), $"Gameover: {this.gameOver}", _guiStyle);

    //}

}
