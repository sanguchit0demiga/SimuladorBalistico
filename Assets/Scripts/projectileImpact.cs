using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float startTime;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
      
        Destroy(gameObject);
    }
}
