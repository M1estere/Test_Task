using UnityEngine;

public class NameGiverController : MonoBehaviour
{
    [SerializeField] private GameObject _field;

    [SerializeField] private TMPro.TMP_InputField _nameInputField;

    private Human _currentHuman;

    private void ResetValues()
    {
        _currentHuman = null;
        _nameInputField.text = "";
    }

    public void OpenGiver(Human human)
    {
        _currentHuman = human;
        _field.SetActive(true);
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
