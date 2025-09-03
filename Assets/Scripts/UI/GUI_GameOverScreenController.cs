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
        UpdateScoreTexts();
    }

    private void UpdateScoreTexts()
    {
        //condition for showing the new highscore message
        if (GameManager.instance.currentScore > GameManager.instance.highestScore)
        {
            newHighscoreMessage.SetActive(true);
        }
        else
        {
            newHighscoreMessage.SetActive(false);
        }

        //score text
        finalScoreText.text = "Score: " + GameManager.instance.currentScore.ToString();
        //if the highscore is already 0 or not defined then set as the current score
        if (GameManager.instance.highestScore > 0)
        {
            highscoreText.text = "Highscore: " + GameManager.instance.highestScore.ToString();

        }
        else
        {
            highscoreText.text = "Highscore: " + GameManager.instance.currentScore.ToString();

        }
    }

    private void OnDisable()
    {
        //not sure what to do here on disabling 
    }

    public void RetryButtonPressed()
    {
        GameManager.instance.ResetGame();
    }
    public void ResetScoreButtonPressed()
    {
        GameManager.instance.ResetScore();
    }
}
