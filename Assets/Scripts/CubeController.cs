using UnityEngine;

public class CubeController : MonoBehaviour
{
    private ReportManager reportManager;
    private bool hit = false;

    void Start()
    {
        reportManager = FindFirstObjectByType<ReportManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hit && collision.gameObject.CompareTag("Projectile"))
        {
            hit = true;
            reportManager.CubeHit();

           
            Projectile proj = collision.gameObject.GetComponent<Projectile>();
            if (proj != null)
            {
                float flightTime = Time.time - proj.startTime;
                float velocity = proj.GetComponent<Rigidbody>().linearVelocity.magnitude;

                reportManager.ShowReport(flightTime, velocity);
            }
        }
    }
}
