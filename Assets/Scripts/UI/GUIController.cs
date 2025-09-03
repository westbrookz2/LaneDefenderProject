using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private Animator _scoreAnimator;
    [SerializeField] private GameObject _hitpointContainer;
    public Canvas GameOverScreen;
    
    void Start()
    {
        _scoreAnimator = _scoreText.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        RefreshScore();
        RefreshHighscore();
        RefreshHitpoints();
    }
    private void OnDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RefreshScore();
        if (GameManager.instance.gameOver)
        {
            GameOverScreen.gameObject.SetActive(true);
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
        //GameObject[] _hitpoints = _hitpointContainer.GetComponentsInChildren<GameObject>();
        //Debug.Log(_hitpoints.Length);

    }
}
