using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 2 Config", menuName = "Config/Level 2 Config")]
public class Level2Config : ScriptableObject
{
    [field: SerializeField]
    public List<Question> Questions { get; private set; } = new();
}
