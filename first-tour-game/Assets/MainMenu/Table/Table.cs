using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public static Table instance = null;

    [SerializeField]
    private string[] Names;
    [SerializeField]
    private int[] Scores;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void newPlayer(string Name, int Score)
    {
        for (int i = 0; i < Names.Length; i++) {
            Names[i] = Name;
            Scores[i] = Score;
        }
    }

    public void SortTable()
    {
        Array.Sort(Scores);
        Array.Reverse(Scores);
        SortNames();
    }

    //Данная функция ищет имя по ключу Score, необходимо в конце каждой сессии сохранять никнейм игрока по Score 
    private void SortNames()
    {
        for (int i = 0; i < Names.Length; i++)
        {
            Names[i] = PlayerPrefs.GetString(Scores[i] + "");
        }
    }
}
