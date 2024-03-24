using System.Collections;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private Level2Config _levelConfiguration;

    [SerializeField] private TMPro.TMP_Text[] _stands;

    [SerializeField] private TMPro.TMP_Text _questionText;
    [Space(5)]

    [SerializeField] private GameObject _dayComingScreen;
    [SerializeField] private GameObject _nightComingScreen;

    [SerializeField] private Animator _questionCanvasAnimator;

    private int _roundIndex = -1;

    private Countdown _countDown;
    private SetupFastMove _setupFastMove;

    private void Awake()
    {
        _countDown = FindObjectOfType<Countdown>();
        _setupFastMove = FindObjectOfType<SetupFastMove>();
    }

    private void Start()
    {
        RoundStart();
    }

    private void RoundStart()
    {
        EnableDayCanvas();
    }

    private void EnableDayCanvas()
    {
        StartCoroutine(DayCanvasCoroutine());
    }

    private void EnableNightCanvas()
    {
        StartCoroutine(NightCanvasCoroutine());
    }

    private IEnumerator NightCanvasCoroutine()
    {
        Time.timeScale = 0;
        _nightComingScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 1;
        _nightComingScreen.GetComponent<Animator>().SetTrigger("Close");

        yield return new WaitForSecondsRealtime(.5f);

        _nightComingScreen.SetActive(false);

        CheckAnswers();
        CheckRoundStats();
    }

    private IEnumerator DayCanvasCoroutine()
    {
        Time.timeScale = 0;
        _dayComingScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 1;
        _dayComingScreen.GetComponent<Animator>().SetTrigger("Close");

        _roundIndex += 1;
        _questionText.SetText($"{_roundIndex + 1}. " + _levelConfiguration.Questions[_roundIndex].Content);
        SetStands();
        _setupFastMove.Set(_levelConfiguration.Questions[_roundIndex].Answers.ToArray());

        _countDown.StartCountdown();

        yield return new WaitForSecondsRealtime(.5f);

        _questionCanvasAnimator.SetTrigger("Open");
        _dayComingScreen.SetActive(false);
    }

    private void CheckRoundStats()
    {
        if (FindObjectsOfType<Human>().Length <= 0 || _roundIndex >= _levelConfiguration.Questions.Count)
        {
            DisplayGameOver();
        } else
        {
            print("Okay");
            RoundStart();
        }
    }

    private void DisplayGameOver() 
    { 
        
    }

    private void SetStands()
    {
        for (int i = 0; i < _stands.Length; i++)
        {
            _stands[i].SetText($"{i + 1}. " + _levelConfiguration.Questions[_roundIndex].Answers[i].Content);
        }
    }

    public void EndRound()
    {
        EnableNightCanvas();
    }

    public void CheckAnswers()
    {
        print("Checking answers...");
    }
}
