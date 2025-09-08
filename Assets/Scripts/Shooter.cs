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
    public float currentAngle = 30f; // Ángulo vertical

    [Header("Rotación horizontal")]
    public Transform cannonBase;
    public float rotationSpeed = 300f; // grados por segundo
    public float maxHorizontalAngle = 45f;
    private float currentHorizontalAngle = 0f;

    [Header("Rotación vertical")]
    public Transform cannonBarrel;

    void Update()
    {
        // Presionar Espacio para disparar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb == null) rb = proj.AddComponent<Rigidbody>();
        rb.mass = projectileMass;

        // La dirección del disparo se obtiene directamente del punto de disparo.
        // firePoint ya está rotado por los movimientos del cañón.
        Vector3 fireDirection = firePoint.forward;

        // Aplicamos la fuerza en la dirección del cañón.
        rb.AddForce(fireDirection * force, ForceMode.Impulse);
    }

    // --- Funciones de control ---
    public void UpdateAngle(float value)
    {
        currentAngle = value;
        if (cannonBarrel != null)
        {
            cannonBarrel.localRotation = Quaternion.Euler(value, 0, 0);
        }
    }

    public void UpdateForce(float value)
    {
        force = value;
    }

    public void UpdateMass(int index)
    {
        float[] masses = { 0.5f, 1f, 2f };
        if (index >= 0 && index < masses.Length)
        {
            projectileMass = masses[index];
        }
    }

    // Rotación horizontal por botones
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
}