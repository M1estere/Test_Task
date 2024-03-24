using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private Level2Config _levelConfiguration;

    [SerializeField] private TMPro.TMP_Text[] _stands;

    [SerializeField] private TMPro.TMP_Text _questionText;

    private int _roundIndex = -1;

    private Countdown _countDown;

    private void Awake()
    {
        _countDown = FindObjectOfType<Countdown>();
    }

    private void Start()
    {
        RoundStart();
    }

    private void RoundStart()
    {
        EnableDayCanvas();

        _roundIndex += 1;
        _questionText.SetText(_levelConfiguration.Questions[_roundIndex].Content);
        SetStands();



        _countDown.StartCountdown();
    }

    private void EnableDayCanvas()
    {

    }

    private void EnableNightCanvas()
    {

    }

    private void CheckRoundStats()
    {
        if (FindObjectsOfType<Human>().Length <= 0 || _roundIndex >= _levelConfiguration.Questions.Count)
        {
            DisplayGameOver();
        } else
        {
            print("Okay");
        }
    }

    private void DisplayGameOver() 
    { 
    
    }

    private void SetStands()
    {
        for (int i = 0; i < _stands.Length; i++)
        {
            _stands[i].SetText(_levelConfiguration.Questions[_roundIndex].Answers[i].Content);
        }
    }

    public void EndRound()
    {
        EnableNightCanvas();
        CheckRoundStats();
    }

    public void CheckAnswers()
    {

    }
}
