using UnityEngine;

public class MainMenuPiece : MonoBehaviour
{
    [SerializeField] private Animator _thisAnimator;

    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _levelsMenu;
  
    public void OpenSettings()
    {
        _thisAnimator.SetTrigger("CloseRight");
        _optionsMenu.SetActive(true);
    }

    public void OpenLevels()
    {
        _thisAnimator.SetTrigger("CloseUp");
        _levelsMenu.SetActive(true);
    }

    public void DeactivateThis() => gameObject.SetActive(false);

    public void QuitGame() => Application.Quit();
}
