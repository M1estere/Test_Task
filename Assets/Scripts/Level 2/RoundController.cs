using System.Collections;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private Level2Config _levelConfiguration;
    [SerializeField] private Light _globalLight;

    [SerializeField] private TMPro.TMP_Text[] _stands;
    [SerializeField] private TMPro.TMP_Text _questionText;
    [Space(5)]

    [SerializeField] private GameObject _dayComingScreen;
    [SerializeField] private GameObject _nightComingScreen;
    [Space(5)]

    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TMPro.TMP_Text _statsText;
    [Space(5)]   

    [SerializeField] private Animator _questionCanvasAnimator;

    private int _roundIndex = -1;

    private Countdown _countDown;
    private SetupFastMove _setupFastMove;

    private LightmapData[] _startData;

    private void Awake()
    {
        _startData = LightmapSettings.lightmaps;

        _countDown = FindObjectOfType<Countdown>();
        _setupFastMove = FindObjectOfType<SetupFastMove>();
    }

    private void Start()
    {
        FindObjectOfType<LevelStats>().UpdateInfo(_roundIndex + 2, FindObjectsOfType<Human>().Length);
        RoundStart();
    }

    public float GetRoundTime() => _levelConfiguration.RoundTime;

    private void RoundStart()
    {
        LevelController.Instance.CanDrag = true;
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
        _countDown.CountdownGo = false;
        Time.timeScale = 0;
        _nightComingScreen.SetActive(true);

        LevelController.Instance.EndRound();
        LevelController.Instance.CanDrag = false;

        yield return new WaitForSecondsRealtime(1);

        LightmapSettings.lightmaps = new LightmapData[] { };
        _globalLight.intensity = 0;

        Time.timeScale = 1;
        _nightComingScreen.GetComponent<Animator>().SetTrigger("Close");

        yield return new WaitForSecondsRealtime(.5f);

        _nightComingScreen.SetActive(false);

        CheckAnswers();

        yield return new WaitForSecondsRealtime(6);

        CheckRoundStats();
    }

    private IEnumerator DayCanvasCoroutine()
    {
        Time.timeScale = 0;
        _dayComingScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        LightmapSettings.lightmaps = _startData;
        _globalLight.intensity = 2;

        _countDown.CountdownGo = true;
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
        int alive = FindObjectsOfType<Human>().Length;
        if (alive <= 0 || _roundIndex >= _levelConfiguration.Questions.Count - 1)
        {
            DisplayGameOver(alive);
        } else
        {
            FindObjectOfType<LevelStats>().UpdateInfo(_roundIndex + 2, alive);
            LevelController.Instance.SetHumansAmount(alive);
            RoundStart();
        }
    }

    private void DisplayGameOver(int amount) 
    {
        Time.timeScale = 0;

        _statsText.SetText($"Раундов: {_roundIndex + 1} | Человек: {amount} | Счет: {LevelController.Instance.Score}");
        _gameOverScreen.SetActive(true);
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
        House[] houses = FindObjectsOfType<House>();

        foreach (House house in houses)
        {
            if (house.PeopleInHouse.Count < 1) continue;

            if (_levelConfiguration.Questions[_roundIndex].Answers[house.GetIndex].IsCorrect == false) house.KillAllHumans();
            else house.ResurrectAllHumans();
        }
    }
}
