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
        List<string> tNames = new(_names);
        _humans = new(FindObjectsOfType<Human>());

        foreach (Human human in _humans)
        {
            string name = tNames[Random.Range(0, tNames.Count)];
            tNames.Remove(name);

            human.SetName(name);
        }
    }
}
