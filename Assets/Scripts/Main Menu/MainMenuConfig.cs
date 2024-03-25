using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Main Menu Config", menuName = "Config/Main Menu Config")]
public class MainMenuConfig : ScriptableObject
{
    [field: SerializeField]
    public List<LevelInformation> Levels { get; private set; } = new();
}

[System.Serializable]
public class LevelInformation
{
    [field: SerializeField]
    public string LevelName;

    [field: SerializeField, TextArea]
    public string LevelDescription;

    [field: SerializeField]
    public int LevelIndex;

    [field: SerializeField]
    public Sprite LevelImage;
}
