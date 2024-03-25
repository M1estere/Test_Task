using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicLevelController : MonoBehaviour
{
    [SerializeField] private Level1Config _levelConfiguration;
    [SerializeField] private GameObject _answerBlock;
    [Space(5)]

    [SerializeField] private Animator _questionAnimator;
    [SerializeField] private TMPro.TMP_Text _questionText;
    [Space(5)]

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private TMPro.TMP_Text _statsText;
    [Space(5)]

    [SerializeField] private TMPro.TMP_Text _scoreText;
    [SerializeField] private TMPro.TMP_Text _mistakesText;

    private int _score;
    private int _mistakes;

    public float[] Positions { get; private set; } = new float[] { 3.5f, 1.75f, 0, -1.75f, -3.5f };

    private List<Answer> _answers;

    private IEnumerator Start()
    {
        _answers = new(_levelConfiguration.Questions[0].Answers);

        _questionText.SetText(_levelConfiguration.Questions[0].Content);
        _questionAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(1.5f);

        while (_answers.Count > 0)
        {
            Spawn();
            yield return new WaitForSeconds(4);
        }

        yield return new WaitForSeconds(4f);

        Time.timeScale = 0;
        _statsText.SetText($"Правильно: {_score} | Ошибок: {_mistakes}");
        _gameOverCanvas.SetActive(true);

        print("Game ended");
    }

    private void Spawn()
    {
        List<float> positions = new(Positions);

        int amount = Random.Range(2, 6);
        for (int i = 0; i < amount; i++)
        {
            if (_answers.Count > 0)
            {
                float zPos = positions[Random.Range(0, positions.Count)];
                positions.Remove(zPos);

                int index = Random.Range(0, _answers.Count);
                Answer randomAnswer = _answers[index];
                _answers.RemoveAt(index);

                SetupSonicAnswerBlock block = Instantiate(_answerBlock, 
                                                          new Vector3(50 + Random.Range(-1, 1.5f), .6f, zPos),
                                                          _answerBlock.transform.rotation).GetComponent<SetupSonicAnswerBlock>();
                block.Set(randomAnswer.Content, randomAnswer.IsCorrect);
            }
        }
    }

    public void PlusCorrect()
    {
        _score++;
        _scoreText.SetText(_score.ToString() + " баллов");
    }

    public void PlusMistake()
    {
        _mistakes++;
        _mistakesText.SetText(_mistakes.ToString() + " ошибок");
    }
}
