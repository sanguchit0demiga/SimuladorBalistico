using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float startTime;  

    void OnCollisionEnter(Collision collision)
    {
        CubeController cube = collision.gameObject.GetComponent<CubeController>();
        if (cube != null)
        {
            float flightTime = Time.time - startTime;

            Rigidbody rb = GetComponent<Rigidbody>();
            float relativeVelocity = rb != null ? rb.linearVelocity.magnitude : 0f;

            cube.ReportHit(flightTime, relativeVelocity);
        }

        Destroy(gameObject);
    }
}
