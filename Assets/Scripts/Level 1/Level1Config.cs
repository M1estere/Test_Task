using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 1 Config", menuName = "Config/Level 1 Config")]
public class Level1Config : ScriptableObject
{
    [field: SerializeField]
    public List<Question> Questions { get; private set; } = new();
}
