using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Animator _mainMenuAnimator;
    [SerializeField] private Animator _thisAnimator;
    [Space(5)]

    [SerializeField] private Color _inactiveColor;
    [SerializeField] private TMPro.TMP_Text[] _titles;
    [SerializeField] private GameObject[] _areas;

    [SerializeField] private Toggle _screenToggle;

    private void Awake()
    {
        _screenToggle.isOn = Screen.width == 1280 ? false : true;
    }

    public void CloseOptions()
    {
        _thisAnimator.SetTrigger("Close");
        _mainMenuAnimator.SetTrigger("OpenRight");
    }

    public void DisableThis() => gameObject.SetActive(false);

    public void OpenSection(int index)
    {
        for (int i = 0; i < _titles.Length; i++)
        {
            if (i != index)
            {
                _titles[i].color = _inactiveColor;
                _areas[i].SetActive(false);
            }
            else
            {
                _titles[i].color = Color.white;
                _areas[i].SetActive(true);
            }
        }
    }

    public void ChangeScreenType(bool state)
    {
        if (state)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
        }
    }
}
