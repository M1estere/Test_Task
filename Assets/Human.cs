using UnityEngine;

public class Human : MonoBehaviour
{
    public string Name { get; set; }

    [SerializeField] private TMPro.TMP_Text _nameField;

    public void SetName(string name)
    {
        Name = name;

        _nameField.SetText(name);
    }

    private void OnMouseOver()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<SetupFastMove>().Open(this);
            FindObjectOfType<LevelController>().CurrentHuman = this;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<NameGiverController>().OpenGiver(this);
        }
    }
}
