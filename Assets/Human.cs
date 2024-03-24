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
            print($"Clicked left on {gameObject.name} as ({Name})");
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            print($"Clicked right on {gameObject.name} as ({Name})");

            FindObjectOfType<NameGiverController>().OpenGiver(this);
        }
    }
}
