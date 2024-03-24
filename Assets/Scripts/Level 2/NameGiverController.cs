using UnityEngine;

public class NameGiverController : MonoBehaviour
{
    [SerializeField] private GameObject _field;

    [SerializeField] private TMPro.TMP_InputField _nameInputField;

    private Human _currentHuman;
    private CameraController _cameraController;

    private void Awake() => _cameraController = FindObjectOfType<CameraController>();

    public void ResetValues()
    {
        _currentHuman = null;
        _nameInputField.text = "";
        _cameraController.enabled = true;
    }

    public void OpenGiver(Human human)
    {
        _currentHuman = human;

        _field.SetActive(true);
        _nameInputField.text = human.Name;

        _cameraController.enabled = false;
    }

    public void Apply()
    {
        string oldName = _currentHuman.Name;
        _currentHuman.SetName(_nameInputField.text);

        print($"Changed human from {oldName} to {_currentHuman.Name}");

        _field.SetActive(false);
        ResetValues();
    }
}
