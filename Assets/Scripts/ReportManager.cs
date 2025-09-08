using UnityEngine;
using UnityEngine.UI;

public class ReportManager : MonoBehaviour
{
    [Header("UI del Reporte")]
    public Text flightTimeText;
    public Text velocityText;
    public Text scoreText;

    private int cubesHit = 0;

  
    public void CubeHit()
    {
        cubesHit++;
    }

    
    public void ShowReport(float flightTime, float velocity)
    {
        int score = cubesHit * 100;

        velocityText.text = "Relative Velocity: " + velocity.ToString("F2") + " m/s";
        scoreText.text = "Score: " + score + "\nCubes Hit: " + cubesHit;
        flightTimeText.text = "Flight Time: " + flightTime.ToString("F2") + " s";
    }

    
    public void ResetHits()
    {
        cubesHit = 0;

       
        velocityText.text = "Relative Velocity: 0 m/s";
        scoreText.text = "Score: 0\nCubes Hit: 0";
        flightTimeText.text = "Flight Time: 0 s";
    }
}
