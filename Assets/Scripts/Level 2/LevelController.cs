using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; set; }

    public Human CurrentHuman { get; set; }

    public int Score { get; set; }

    public bool CanDrag { get; set; } = true;

    private SetupFastMove _setupFastMove;
    private NameGiverController _nameGiverController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        _setupFastMove = FindObjectOfType<SetupFastMove>();
        _nameGiverController = FindObjectOfType<NameGiverController>();
    }

    public void EndRound()
    {
        _setupFastMove.Close();
        _nameGiverController.ResetValues();

        CurrentHuman = null;
    }
}
