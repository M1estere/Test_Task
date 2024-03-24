using UnityEngine;
using UnityEngine.AI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _pauseButtonText;

    private bool _paused = false;
    private float _startSpeed = 12;

    public void Toggle()
    {
        if (_paused) Continue();
        else Pause();

        _paused = !_paused;
    }

    private void Continue()
    {
        NavMeshAgent[] agents = FindObjectsOfType<NavMeshAgent>(true);

        foreach (NavMeshAgent agent in agents)
            agent.speed = _startSpeed;

        _pauseButtonText.SetText("пауза");
    }

    private void Pause()
    {
        NavMeshAgent[] agents = FindObjectsOfType<NavMeshAgent>(true);
        _startSpeed = agents[0].speed;

        foreach (NavMeshAgent agent in agents)
            agent.speed = 0;

        _pauseButtonText.SetText("продолжить");
    }
}
