using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Human : MonoBehaviour
{
    public string Name { get; set; }

    [SerializeField] private TMPro.TMP_Text _nameField;

    private GameObject[] _goalLocations;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _goalLocations = GameObject.FindGameObjectsWithTag("Goal");
        _agent.SetDestination(_goalLocations[Random.Range(0, _goalLocations.Length)].transform.position);
    }

    private void Update()
    {
        if (_agent.remainingDistance < 1)
        {
            _agent.SetDestination(_goalLocations[Random.Range(0, _goalLocations.Length)].transform.position);
        }
    }

    public void SetName(string name)
    {
        Name = name;

        _nameField.SetText(name);
    }

    private void OnMouseOver()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<SetupFastMove>().Open(this);
            FindObjectOfType<LevelController>().CurrentHuman = this;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<NameGiverController>().OpenGiver(this);
        }
    }
}
