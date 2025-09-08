using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint;         // Punto desde donde se dispara

    [Header("Parámetros de disparo")]
    public float angle = 30f;           // Ángulo en grados
    public float force = 20f;           // Fuerza de lanzamiento
    public float projectileMass = 1f;   // Masa del proyectil
    [SerializeField] private Slider angleSlider;
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
        // Crear el proyectil en el firePoint
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Asegurarse de que tenga Rigidbody
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb == null) rb = proj.AddComponent<Rigidbody>();

        // Configurar la masa
        rb.mass = projectileMass;

        // Calcular dirección según ángulo
        float rad = angle * Mathf.Deg2Rad;
        Vector3 dir = firePoint.forward * Mathf.Cos(rad) + firePoint.up * Mathf.Sin(rad);

        // Aplicar fuerza
        rb.AddForce(dir.normalized * force, ForceMode.Impulse);
    }
    public void CannonAngle()
    {
        float angle = angleSlider.value;
        transform.localRotation = Quaternion.Euler(angle, 0, 0);
    }
}
