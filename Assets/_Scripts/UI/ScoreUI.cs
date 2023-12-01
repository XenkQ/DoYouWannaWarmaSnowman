using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text _scoreLabel;
    private Stat _score;
    private int scoreOldValue;

    private void Awake()
    {
        _scoreLabel = GetComponent<TMP_Text>();
        _score = FindObjectOfType<PlayerStats>().score;
    }

    private void FixedUpdate()
    {
        if(_score.value != scoreOldValue)
        {
            scoreOldValue = _score.value;
            UpdateScore();
        }
    }

    public Stat GetScoreStat()
    {
        return _score;
    }

    public void UpdateScore()
    {
        _scoreLabel.text = _score.GetValue().ToString();
    }

}
