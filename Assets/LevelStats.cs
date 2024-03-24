using UnityEngine;

public class LevelStats : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _roundText;
    [SerializeField] private TMPro.TMP_Text _aliveText;
    [SerializeField] private TMPro.TMP_Text _scoreText;

    public void UpdateInfo(int roundIndex, int aliveAmount)
    {
        _roundText.SetText($"Раунд: {roundIndex}");
        _aliveText.SetText($"В живых: {aliveAmount}");
        _scoreText.SetText($"Счет: {LevelController.Instance.Score}");
    }
}
