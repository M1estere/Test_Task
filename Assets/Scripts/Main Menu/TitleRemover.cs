using UnityEngine;

public class TitleRemover : MonoBehaviour
{
    [SerializeField] private Animator _titleAnimator;

    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private Animator _menuScreenAnimator;

    private void Awake()
    {
        Application.targetFrameRate = -1;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _titleAnimator.SetTrigger("Close");
            Invoke(nameof(EnableMenu), .5f);
        }
    }

    public void DisableThis() => gameObject.SetActive(false);

    private void EnableMenu()
    {
        _menuScreen.SetActive(true);
        _menuScreenAnimator.SetTrigger("OpenScale");
    }
}
