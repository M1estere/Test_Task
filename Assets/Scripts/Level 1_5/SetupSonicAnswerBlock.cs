using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SetupSonicAnswerBlock : MonoBehaviour
{
    private static readonly Color[] _colors = new Color[]
    {
        Color.red, Color.yellow, Color.blue, Color.green
    };

    [SerializeField] private Image _bgImage;
    [SerializeField] private TMPro.TMP_Text _answerText;

    private bool _isCorrect;

    private SonicLevelController _controller;

    public void Set(string text, bool isCorrect)
    {
        _isCorrect = isCorrect;

        _answerText.SetText(text);

        Color temp = _colors[Random.Range(0, _colors.Length)];
        temp.a = .8f;

        _bgImage.color = temp;
    }

    private void OnEnable()
    {
        _controller = FindObjectOfType<SonicLevelController>();
        //transform.DOMoveX(-25, 12).SetEase(Ease.Linear).onComplete = () => Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out SonicController controller))
        {
            if (_isCorrect)
            {
                _controller.PlusCorrect();
                print("Correct");
            }
            else
            {
                _controller.PlusMistake();
                print("Incorrect");
            }

            Destroy(gameObject);
        }
    }
}
