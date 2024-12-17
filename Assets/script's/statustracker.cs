using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections;
using System.Collections.Generic;

public class StatusTracker : MonoBehaviour
{
    public TextMeshProUGUI statusText; // For TextMeshPro
    private int healthy, sick, immune, dead;

    void Update()
    {
        // Reset counts
        healthy = sick = immune = dead = 0;

        // Count objects with specific tags
        healthy = GameObject.FindGameObjectsWithTag("Friska").Length;
        sick = GameObject.FindGameObjectsWithTag("Sjuka").Length;
        immune = GameObject.FindGameObjectsWithTag("Immuna").Length;
        dead = GameObject.FindGameObjectsWithTag("Döda").Length;

        // Update the UI text with the current stats
        statusText.text = $"Friska: {healthy}  Sjuka: {sick}  Immuna: {immune}  Döda: {dead}";
    }
}