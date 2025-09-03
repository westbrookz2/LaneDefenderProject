using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI_GameOverScreenController : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private GameObject newHighscoreMessage;
    [SerializeField] private Button retryButton;
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (GameManager.instance.currentScore > GameManager.instance.highestScore)
        {
            newHighscoreMessage.SetActive(true);
        }
        else
        {
            newHighscoreMessage.SetActive(false);
        }
        finalScoreText.text = "Score: " + GameManager.instance.currentScore.ToString();
        highscoreText.text = "Highscore: " + GameManager.instance.highestScore.ToString();
    }

    private void OnDisable()
    {
        
    }

    public void RetryButtonPressed()
    {
        GameManager.instance.ResetGame();
    }
}
