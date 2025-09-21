using UnityEngine;

public class CubeController : MonoBehaviour
{
    private ReportManager reportManager;
    private bool hit = false;

    void Start()
    {
        reportManager = FindFirstObjectByType<ReportManager>();
    }

    public void ReportHit(float flightTime, float projectileVelocity)
    {
        if (!hit)
        {
            hit = true;
            reportManager.CubeHit();
            reportManager.ShowReport(flightTime, projectileVelocity);
        }
    }
}
