using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class House : MonoBehaviour
{
    public List<Human> PeopleInHouse { get; set; } = new();
    public int GetIndex => _houseIndex - 1;

    [SerializeField] private int _houseIndex;
    [Space(5)]

    [SerializeField] private GameObject _namePrefabBlock;
    [SerializeField] private Transform _namesSpawnPosition;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.materials[1].SetFloat("_Scale", 0);
    }

    private void OnMouseOver()
    {
        if (PeopleInHouse.Count > 0)
        {
            _meshRenderer.materials[1].SetFloat("_Scale", 0.06f);
            _meshRenderer.materials[1].SetColor("_Color", Color.green);
        }
        else if (LevelController.Instance.CurrentHuman != null)
        {
            _meshRenderer.materials[1].SetFloat("_Scale", 0.06f);
            _meshRenderer.materials[1].SetColor("_Color", Color.red);
        }
    }

    private void OnMouseExit() => _meshRenderer.materials[1].SetFloat("_Scale", 0);

    private void OnMouseDown()
    {
        if (PeopleInHouse.Count > 0)
        {
            PopHuman();
            _meshRenderer.materials[^1].SetFloat("_Scale", 0);
        }
        else if (LevelController.Instance.CurrentHuman != null)
        {
            FindObjectOfType<SetupFastMove>().TransferToHouse(GetIndex);
            _meshRenderer.materials[^1].SetFloat("_Scale", 0);
        }
    }

    public void AddHuman(Human human)
    {
        PeopleInHouse.Add(human);

        print($"{human.Name} human added to {gameObject.name} ({_houseIndex - 1})");
    }

    public void PopHuman()
    {
        if (PeopleInHouse.Count > 0)
        {
            Human tHuman = PeopleInHouse[^1];

            tHuman.gameObject.SetActive(true);
            PeopleInHouse.RemoveAt(PeopleInHouse.Count - 1);

            print($"{tHuman.Name} human removed from {gameObject.name} ({_houseIndex - 1})");
        }
    }

    public void KillAllHumans()
    {
        StartCoroutine(KillAllCoroutine());
    }

    public void ResurrectAllHumans()
    {
        foreach (Human human in PeopleInHouse)
            human.gameObject.SetActive(true);

        LevelController.Instance.Score += PeopleInHouse.Count;
        PeopleInHouse.Clear();
        print("All people resurrected");
    }

    private IEnumerator KillAllCoroutine()
    {
        foreach (Human human in PeopleInHouse)
        {
            SetupNameBlock name = Instantiate(_namePrefabBlock, 
                                              _namesSpawnPosition.position + new Vector3(Random.Range(-.2f, .2f), 0, 0), 
                                              Quaternion.identity).GetComponent<SetupNameBlock>();
            name.Set(human.Name);
            Destroy(name, Random.Range(2f, 3f));

            yield return new WaitForSeconds(.5f);
        }

        PeopleInHouse.Clear();
        print("All people dead");
    }
}
