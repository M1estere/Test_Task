using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SetupAnswerBlockOne : MonoBehaviour
{
    private static readonly Color[] _colors = new Color[]
    {
        Color.red, Color.yellow, Color.blue, Color.green
    };

    [SerializeField] private Image _bgImage;
    [SerializeField] private TMPro.TMP_Text _answerText;

    private bool _isCorrect;

    private LevelOneController _controller;

    public void Set(string text, bool isCorrect)
    {
        _isCorrect = isCorrect;

        _answerText.SetText(text);

        Color temp = _colors[Random.Range(0, _colors.Length)];
        temp.a = .3f;

        _bgImage.color = temp;
    }

    private void OnEnable()
    {
        _controller = FindObjectOfType<LevelOneController>();
        transform.DOMoveY(-10, 8).SetEase(Ease.Linear).onComplete = () => Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_isCorrect)
            {
                _controller.PlusCorrect();
                print("Correct");
            } else
            {
                _controller.PlusMistake();
                print("Incorrect");
            }

            Destroy(gameObject);
        }
    }
}
