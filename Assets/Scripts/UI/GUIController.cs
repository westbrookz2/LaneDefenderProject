using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Animator _scoreAnimator;

    
    void Start()
    {
        _scoreAnimator = _scoreText.GetComponent<Animator>();
        GameManager _gm = FindFirstObjectByType<GameManager>();
        if (_gm != null)
        {
            //_gm.onScoreGained += RefreshScore;
        }
    }
    private void OnEnable()
    {
        RefreshScore();
    }
    private void OnDestroy()
    {
        //GameManager.instance.onScoreGained -= RefreshScore;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RefreshScore()
    {
        if (GameManager.instance != null)
        {
            _scoreText.text = "Score: " + GameManager.instance.currentScore.ToString();

        }
    }
}
