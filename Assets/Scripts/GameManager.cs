using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class GameManager : Singleton<GameManager>
{
    //public event IncrementCurrentScore onScoreGained;
    public int currentScore = 0;


    private int highestScore;
    public bool gameOver = false;
    [SerializeField] private HealthScript _health;
    [SerializeField] private PlayerController _playerController;

    void Start()
    {
        highestScore = PlayerPrefs.GetInt("High Score");
        _playerController = FindAnyObjectByType<PlayerController>();
        gameOver = false;
        _health = GetComponent<HealthScript>();
    }

    public void IncrementCurrentScore(int _add)
    {
        currentScore += _add;

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
        _health.TakeDamage();
        if (_health.isDead)
        {
            //death condition

            GameOver();
        }
    }
    public void GameOver()
    {
        gameOver = true;
    }


    public void ResetGame()
{
        SceneManager.LoadScene(0);
    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("High Score", 0);
        highestScore = 0;
    }

    private void OnGUI()
    {
        GUIStyle _guiStyle = new GUIStyle();
        _guiStyle.fontSize = 18;
        _guiStyle.fontStyle = FontStyle.Normal;
        _guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10f, 130f, 10f, 10f), $"Speed: {_playerController.rb.linearVelocityY}", _guiStyle);
        GUI.Label(new Rect(10f, 150f, 10f, 10f), $"HP: {_health.currentHealth}", _guiStyle);
        GUI.Label(new Rect(10f, 170f, 10f, 10f), $"Score: {this.currentScore}", _guiStyle);
        GUI.Label(new Rect(10f, 190f, 10f, 10f), $"Highscore: {this.highestScore}", _guiStyle);
        GUI.Label(new Rect(10f, 210f, 10f, 10f), $"Gameover: {this.gameOver}", _guiStyle);

    }

}
