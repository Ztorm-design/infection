using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphScript : MonoBehaviour
{
    public int interval = 5; // Tid mellan datainsamlingar
    public LineRenderer healthyLine, sickLine, immuneLine, deadLine;

    private List<int> healthyHistory = new List<int>();
    private List<int> sickHistory = new List<int>();
    private List<int> immuneHistory = new List<int>();
    private List<int> deadHistory = new List<int>();

    void Start()
    {
        StartCoroutine(CollectData());
    }

    IEnumerator CollectData()
    {
        while (true)
        {
            UpdateStatistics();
            DrawGraph();
            yield return new WaitForSeconds(interval);
        }
    }

    void UpdateStatistics()
    {
        // Räkna antalet i varje status
        int healthy = 0, sick = 0, immune = 0, dead = 0;
        foreach (HumanController human in FindObjectsOfType<HumanController>())
        {
            switch (human.status)
            {
                case Status.Friska: healthy++; break;
                case Status.Sjuka: sick++; break;
                case Status.Immuna: immune++; break;
                case Status.Döda: dead++; break;
            }
        }

        healthyHistory.Add(healthy);
        sickHistory.Add(sick);
        immuneHistory.Add(immune);
        deadHistory.Add(dead);
    }

    void DrawGraph()
    {
        // Rita grafer baserat på historik (implementera med LineRenderer)
    }
}