using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelOneController : MonoBehaviour
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

    public float[] Positions { get; private set; } = new float[] { -8, -4, 0, 4, 8 };

    private List<Answer> _answers;

    private IEnumerator Start()
    {
        int iteration = 0;
        _answers = new(_levelConfiguration.Questions[0].Answers);

        _questionText.SetText(_levelConfiguration.Questions[0].Content);
        _questionAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(1.5f);

        while (_answers.Count > 0)
        {
            Spawn(iteration++ == 0);
            yield return new WaitForSeconds(4);
        }

        yield return new WaitForSeconds(3.5f);

        Time.timeScale = 0;
        _statsText.SetText($"Правильно: {_score} | Ошибок: {_mistakes}");
        _gameOverCanvas.SetActive(true);

        print("Game ended");
    }

    private void Spawn(bool onlyWrong)
    {
        List<float> positions = new(Positions);
        List<Answer> wrongAnswers = _answers.Where(answer => answer.IsCorrect == false).ToList();

        int amount = Random.Range(2, onlyWrong ? 4 : 6);
        for (int i = 0; i < amount; i++)
        {
            if (_answers.Count <= 0) continue;

            float xPos = positions[Random.Range(0, positions.Count)];
            positions.Remove(xPos);

            if (onlyWrong)
            {
                int index = Random.Range(0, wrongAnswers.Count);
                Answer randomAnswer = wrongAnswers[index];
                wrongAnswers.RemoveAt(index);

                SetupAnswerBlockOne block = Instantiate(_answerBlock, new Vector2(xPos, 8 + Random.Range(-.75f, .75f)), Quaternion.identity).GetComponent<SetupAnswerBlockOne>();
                block.Set(randomAnswer.Content, randomAnswer.IsCorrect);
            } else
            {
                int index = Random.Range(0, _answers.Count);
                Answer randomAnswer = _answers[index];
                _answers.RemoveAt(index);

                SetupAnswerBlockOne block = Instantiate(_answerBlock, new Vector2(xPos, 8 + Random.Range(-.75f, .75f)), Quaternion.identity).GetComponent<SetupAnswerBlockOne>();
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
