using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    [field: SerializeField]
    public string Content { get; private set; }

    [field: SerializeField]
    public List<Answer> Answers { get; private set; } = new();
}
