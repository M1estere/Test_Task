using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; set; }

    public Human CurrentHuman { get; set; }

    public int Score { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}
