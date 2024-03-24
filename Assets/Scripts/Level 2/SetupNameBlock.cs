using UnityEngine;

public class SetupNameBlock : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _nameText;

    public void Set(string name)
    {
        _nameText.SetText(name);
    }
}
