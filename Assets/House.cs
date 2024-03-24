using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public List<Human> PeopleInHouse { get; set; } = new();

    [SerializeField] private int _houseIndex;

    public int GetIndex => _houseIndex - 1;

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
            PeopleInHouse.RemoveAt(PeopleInHouse.Count - 1);

            print($"{tHuman.Name} human removed from {gameObject.name} ({_houseIndex - 1})");
        }
    }

    public void KillAllHumans()
    {
        PeopleInHouse.Clear();
        print($"Now there is no person in {gameObject.name} ({_houseIndex - 1})");
    }
}
