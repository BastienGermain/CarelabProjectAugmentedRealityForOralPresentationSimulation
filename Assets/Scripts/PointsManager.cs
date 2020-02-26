using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Simple class to manage point system

public class PointsManager : MonoBehaviour
{

    public Text scoreText;
    private int points;

    public void Start()
    {
        points = 0;
        scoreText.text = "Score: " + points;
    }

    public void AddPoints(int amount)
    {
        points += amount;
        scoreText.text = "Score: " + points;
    }
}
