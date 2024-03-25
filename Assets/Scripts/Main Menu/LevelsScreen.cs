using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsScreen : MonoBehaviour
{
    [SerializeField] private MainMenuConfig _mainMenuConfiguration;
    [Space(5)]

    [SerializeField] private Animator _thisAnimator;
    [SerializeField] private Animator _mainMenuAnimator;

    [SerializeField] private Image[] _indicators;

    [SerializeField] private float _activeWidth;
    [SerializeField] private Color _activeColor;

    [SerializeField] private float _disabledWidth;
    [SerializeField] private Color _disabledColor;
    [Space(5)]

    [SerializeField] private TMPro.TMP_Text _topArrow;
    [SerializeField] private TMPro.TMP_Text _bottomArrow;
    [Space(5)]

    [SerializeField] private TMPro.TMP_Text _levelTitle;
    [SerializeField] private TMPro.TMP_Text _levelDescription;
    [SerializeField] private Image _levelImage;

    private int _currentLevelIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OpenNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OpenPreviousLevel();
        }
    }

    public void OpenNextLevel()
    {
        if (_currentLevelIndex >= _mainMenuConfiguration.Levels.Count - 1) return;

        _currentLevelIndex++;
        OpenLevel();
    }

    public void OpenPreviousLevel()
    {
        if (_currentLevelIndex <= 0) return;

        _currentLevelIndex--;
        OpenLevel();
    }

    private void OpenLevel()
    {
        if (_currentLevelIndex >= _mainMenuConfiguration.Levels.Count - 1)
        {
            ChangeOpacity(_topArrow, 0);
            ChangeOpacity(_bottomArrow, 1);
        } else if (_currentLevelIndex <= 0)
        {
            ChangeOpacity(_topArrow, 1);
            ChangeOpacity(_bottomArrow, 0);
        } else
        {
            ChangeOpacity(_topArrow, 1);
            ChangeOpacity(_bottomArrow, 1);
        }

        for (int i = 0; i < _indicators.Length; i++)
        {
            if (i == _currentLevelIndex)
            {
                _indicators[i].rectTransform.sizeDelta = new Vector2(_activeWidth, 9.1f);
                _indicators[i].color = _activeColor;
            } else
            {
                _indicators[i].rectTransform.sizeDelta = new Vector2(_disabledWidth, 9.1f);
                _indicators[i].color = _disabledColor;
            }
        }

        _levelTitle.SetText(_mainMenuConfiguration.Levels[_currentLevelIndex].LevelName);
        _levelDescription.SetText(_mainMenuConfiguration.Levels[_currentLevelIndex].LevelDescription);

        _levelImage.sprite = _mainMenuConfiguration.Levels[_currentLevelIndex].LevelImage;
    }

    private void ChangeOpacity(TMPro.TMP_Text text, float opacity)
    {
        Color t = text.color;
        t.a = opacity;
        text.color = t;
    }

    public void OpenScene() => SceneManager.LoadScene(_mainMenuConfiguration.Levels[_currentLevelIndex].LevelIndex + 1);

    public void CloseOptions()
    {
        _thisAnimator.SetTrigger("Close");
        _mainMenuAnimator.SetTrigger("OpenUp");
    }

    public void DisableThis() => gameObject.SetActive(false);
}
