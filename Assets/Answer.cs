using UnityEngine;

[System.Serializable]    
public class Answer
{
    [field: SerializeField]
    public string Content { get; private set; }

    [field: SerializeField]
    public bool IsCorrect { get; private set; }
}
