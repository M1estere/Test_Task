﻿using System.Collections;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private Level2Config _levelConfiguration;

    [SerializeField] private TMPro.TMP_Text[] _stands;
    [SerializeField] private TMPro.TMP_Text _questionText;
    [Space(5)]

    [SerializeField] private GameObject _dayComingScreen;
    [SerializeField] private GameObject _nightComingScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TMPro.TMP_Text _statsText;

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
        FindObjectOfType<LevelStats>().UpdateInfo(_roundIndex + 2, FindObjectsOfType<Human>().Length);
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
        int alive = FindObjectsOfType<Human>().Length;
        if (alive <= 0 || _roundIndex >= _levelConfiguration.Questions.Count)
        {
            DisplayGameOver(alive);
        } else
        {
            print("Okay");
            FindObjectOfType<LevelStats>().UpdateInfo(_roundIndex + 2, alive);
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
            print(house.gameObject.name);
            if (house.PeopleInHouse.Count < 1) continue;

            if (_levelConfiguration.Questions[_roundIndex].Answers[house.GetIndex].IsCorrect == false) house.KillAllHumans();
            else house.ResurrectAllHumans();
        }
    }
}
