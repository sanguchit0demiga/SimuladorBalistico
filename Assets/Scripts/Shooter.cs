using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Parámetros de disparo")]
    public float force = 20f;
    public float projectileMass = 1f;
    public float currentAngle = 30f;

    [Header("Rotación horizontal")]
    public Transform cannonBase;
    public float rotationSpeed = 300f;
    public float maxHorizontalAngle = 45f;
    private float currentHorizontalAngle = 0f;

    [Header("Rotación vertical")]
    public Transform cannonBarrel;

    [Header("Línea Predictiva")]
    public LineRenderer lineRenderer;
    public int linePoints = 50;
    public float timeBetweenPoints = 0.1f;

    void Update()
    {
        // Solo dibuja la trayectoria si no se está disparando
        DrawTrajectory();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = proj.AddComponent<Rigidbody>();
        }
        rb.mass = projectileMass;

        Vector3 fireDirection = firePoint.forward;
        rb.AddForce(fireDirection * force, ForceMode.Impulse);

        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
        {
            p.startTime = Time.time;
            p.angle = currentAngle;
            p.force = force;
            p.mass = projectileMass;
            p.startPosition = firePoint.position;
        }
    }

    public void UpdateAngle(float value)
    {
        currentAngle = value;
        if (cannonBarrel != null)
        {
            cannonBarrel.localRotation = Quaternion.Euler(value, 0, 0);
        }
        // Dibuja la trayectoria cada vez que se actualiza el ángulo
        DrawTrajectory();
    }

    public void UpdateForce(float value)
    {
        force = value;
        // Dibuja la trayectoria cada vez que se actualiza la fuerza
        DrawTrajectory();
    }

    public void UpdateMass(int index)
    {
        float[] masses = { 1f, 5f, 10f };
        if (index >= 0 && index < masses.Length)
        {
            projectileMass = masses[index];
        }
        // Dibuja la trayectoria cada vez que se actualiza la masa
        DrawTrajectory();
    }

    public void RotateLeftContinuous()
    {
        currentHorizontalAngle -= rotationSpeed * Time.deltaTime;
        currentHorizontalAngle = Mathf.Clamp(currentHorizontalAngle, -maxHorizontalAngle, maxHorizontalAngle);
        UpdateHorizontalRotation();
    }

    public void RotateRightContinuous()
    {
        currentHorizontalAngle += rotationSpeed * Time.deltaTime;
        currentHorizontalAngle = Mathf.Clamp(currentHorizontalAngle, -maxHorizontalAngle, maxHorizontalAngle);
        UpdateHorizontalRotation();
    }

    private void UpdateHorizontalRotation()
    {
        if (cannonBase != null)
        {
            cannonBase.localRotation = Quaternion.Euler(0, currentHorizontalAngle, 0);
        }
    }

    void DrawTrajectory()
    {
        Vector3 startPosition = firePoint.position;
        Vector3 startVelocity = firePoint.forward * force / projectileMass;

        lineRenderer.positionCount = linePoints;

        for (int i = 0; i < linePoints; i++)
        {
            float time = i * timeBetweenPoints;
            Vector3 position = startPosition + startVelocity * time + Physics.gravity * time * time / 2f;
            lineRenderer.SetPosition(i, position);
        }
    }
}