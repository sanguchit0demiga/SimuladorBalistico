using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float startTime;
    [SerializeField] public float angle;
    [SerializeField] public float force;
    [SerializeField] public float mass;
    [SerializeField] public Vector3 startPosition;
    public FirebaseManager firebaseManager;

    private void OnCollisionEnter(Collision collision)
    {
        float flightTime = Time.time - startTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        float velocity = rb != null ? rb.linearVelocity.magnitude : 0f;

        float distance = Vector3.Distance(transform.position, startPosition);

        CubeController cube = collision.gameObject.GetComponent<CubeController>();
        if (cube != null)
        {
            cube.ReportHit(angle, force, mass, flightTime, velocity, true, distance);
        }
        else
        {
            ReportManager report = FindFirstObjectByType<ReportManager>();
            if (report != null)
            {
                report.ShowReport(angle, force, mass, flightTime, velocity, false, distance);
            }
        }

        Destroy(gameObject);
    }
}