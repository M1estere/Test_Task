using UnityEngine;

public class MainMenuPiece : MonoBehaviour
{
    [SerializeField] private Animator _thisAnimator;

    [SerializeField] private GameObject _optionsMenu;

    public void OpenSettings()
    {
        _thisAnimator.SetTrigger("CloseRight");
        _optionsMenu.SetActive(true);
    }

    public void DeactivateThis() => gameObject.SetActive(false);

    public void QuitGame() => Application.Quit();
}
