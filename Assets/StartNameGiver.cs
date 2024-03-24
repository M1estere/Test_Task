using System.Collections.Generic;
using UnityEngine;

public class StartNameGiver : MonoBehaviour
{
    private static List<string> _names = new()
    {
        "Руслан", "Ильнур", "Никита", "Илья", "Федор", 
        "Петр", "Дмитрий", "Андрей", "Анатолий", "Мария", 
        "Василиса", "Василий", "Ангелина", "Екатерина", "Олег",
        "Эрнест", "Тим", "Ярослав", "Владислав", "Артем" 
    };

    private List<Human> _humans;

    private void Awake()
    {
        _humans = new(FindObjectsOfType<Human>());

        foreach (Human human in _humans)
        {
            string name = _names[Random.Range(0, _names.Count)];
            _names.Remove(name);

            human.SetName(name);
        }
    }
}
