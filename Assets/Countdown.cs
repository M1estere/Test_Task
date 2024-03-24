using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public bool CountdownGo { get; set; } = false;

    [SerializeField] private float _roundTime = 60;
    [SerializeField] private Image _progressCircle;

    private float _currentRoundTime;
    private int _finished = 0;

    public void StartCountdown()
    {
        ResetValues();
        CountdownGo = true;
    }

    private void ResetValues()
    {
        _currentRoundTime = _roundTime;
        _progressCircle.fillAmount = 0;
        _finished = 0;
    }

    private void Update()
    {
        if (CountdownGo == false) return;

        _currentRoundTime -= Time.deltaTime;
        _progressCircle.fillAmount = 1 - (_currentRoundTime / _roundTime);

        if (_progressCircle.fillAmount >= .998f && _finished++ == 0)
        {
            FindObjectOfType<RoundController>().EndRound();
        }
    }
}
