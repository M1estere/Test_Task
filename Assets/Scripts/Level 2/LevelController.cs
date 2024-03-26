using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; set; }

    public Human CurrentHuman { get; set; }

    public int Score { get; set; }

    public bool CanDrag { get; set; } = true;

    private SetupFastMove _setupFastMove;
    private NameGiverController _nameGiverController;

    private RoundController _roundController;

    private int _humans = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        _roundController = FindObjectOfType<RoundController>();
        _setupFastMove = FindObjectOfType<SetupFastMove>();
        _nameGiverController = FindObjectOfType<NameGiverController>();
    }

    public void RemoveHuman()
    {
        _humans--;
        if (_humans <= 0)
        {
            StartCoroutine(DelayedCall());
        }
    }

    private IEnumerator DelayedCall()
    {
        yield return new WaitForSecondsRealtime(1);
        _roundController.EndRound();
    }

    public void SetHumansAmount(int amount) => _humans = amount;

    public void EndRound()
    {
        _setupFastMove.Close();
        _nameGiverController.ResetValues();

        CurrentHuman = null;
    }
}
