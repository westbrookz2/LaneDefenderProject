using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private Animator _scoreAnimator;
    [SerializeField] private GameObject[] _hitpoints;
    public Canvas GameOverScreen;
    
    void Start()
    {
        _scoreAnimator = _scoreText.GetComponent<Animator>();
        RefreshScore();
        RefreshHighscore();
        RefreshHitpoints();
    }
    private void OnEnable()
    {

    }
    private void OnDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null)
        {
            RefreshScore();
            if (GameManager.instance.gameOver)
            {
                GameOverScreen.gameObject.SetActive(true);
            }
            RefreshHitpoints();
        }

    }

    private void RefreshScore()
    {
        if (GameManager.instance != null)
        {
            _scoreText.text = "Score: " + GameManager.instance.currentScore.ToString();

        }
    }
    private void RefreshHighscore()
    {
        if (GameManager.instance != null)
        {
            _highScoreText.text = "Highscore: " + GameManager.instance.highestScore.ToString();
        }
    }
    private void RefreshHitpoints()
    {
        switch (GameManager.instance._health.currentHealth)
        {
            case 0:
                _hitpoints[0].SetActive(false);
                _hitpoints[1].SetActive(false);
                _hitpoints[2].SetActive(false);
                break;
            case 1:
                _hitpoints[0].SetActive(true);
                _hitpoints[1].SetActive(false);
                _hitpoints[2].SetActive(false);
                break;
            case 2:
                _hitpoints[0].SetActive(true);
                _hitpoints[1].SetActive(true);
                _hitpoints[2].SetActive(false);
                break;
            case 3:
                _hitpoints[0].SetActive(true);
                _hitpoints[1].SetActive(true);
                _hitpoints[2].SetActive(true);
                break;
        }
    }
}
