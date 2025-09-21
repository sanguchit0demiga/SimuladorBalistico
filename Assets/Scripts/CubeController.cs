using UnityEngine;

public class CubeController : MonoBehaviour
{
    private ReportManager reportManager;
    private bool hit = false;

    void Start()
    {
        reportManager = FindFirstObjectByType<ReportManager>();
    }

    public void ReportHit(float angle, float force, float mass, float flightTime, float projectileVelocity, bool hit, float distance)
    {
        if (!this.hit)
        {
            this.hit = true;
            if (reportManager != null)
            {
                reportManager.CubeHit();
            }
            reportManager.ShowReport(angle, force, mass, flightTime, projectileVelocity, hit, distance);
        }
    }
}