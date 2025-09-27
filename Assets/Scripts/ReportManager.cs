using UnityEngine;
using UnityEngine.UI;

public class ReportManager : MonoBehaviour
{
    [Header("UI del Reporte")]
    public Text angleText;
    public Text forceText;
    public Text massText;
    public Text flightTimeText;
    public Text velocityText;
    public Text resultText;
    public Text distanceText;
    public Text cubesHitText;

    private int cubesHit = 0;

    
    public void CubeHit()
    {
        cubesHit++;
        if (cubesHitText != null)
            cubesHitText.text = "Cubes Hit: " + cubesHit;
    }

   
    public void ResetHits()
    {
        cubesHit = 0;
        if (cubesHitText != null)
            cubesHitText.text = "Cubes Hit: 0";

        ResetReport();
    }

    public void ShowReport(float angle, float force, float mass,
                           float flightTime, float velocity,
                           bool hit, float distance)
    {
        angleText.text = "Angle: " + angle.ToString("F1") + "°";
        forceText.text = "Force: " + force.ToString("F1") + " N";
        massText.text = "Mass: " + mass.ToString("F1") + " kg";

        flightTimeText.text = "Flight Time: " + flightTime.ToString("F2") + " s";
        velocityText.text = "Impact Velocity: " + velocity.ToString("F2") + " m/s";

        resultText.text = hit ? "Impact: YES" : "Impact: NO";
        distanceText.text = "Distance: " + distance.ToString("F2") + " m";
    }

    public void ResetReport()
    {
        angleText.text = "Angle: 0°";
        forceText.text = "Force: 0 N";
        massText.text = "Mass: 0 kg";
        flightTimeText.text = "Flight Time: 0 s";
        velocityText.text = "Impact Velocity: 0 m/s";
        resultText.text = "Impact: NO";
        distanceText.text = "Distance: 0 m";
    }
}
