using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint;        // Punto desde donde se dispara

    [Header("Parámetros de disparo")]
    public float force = 20f;          // Fuerza de lanzamiento
    public float projectileMass = 1f;  // Masa del proyectil
    public float currentAngle = 30f;   // Ángulo de disparo actual

    [Header("Controles UI")]
    // Estos campos no son necesarios si los Sliders están en otro script (ShooterUI)
    // Pero si los mantienes, asegúrate de que no haya conflictos en el Inspector.
    [SerializeField] private Slider angleSlider;
    [SerializeField] private Slider horizontalSlider;

    [Header("Objetos del cañón")]
    public Transform cannonBase;        // El objeto que gira horizontalmente (padre)
    public Transform cannonBarrel;      // El objeto que se inclina verticalmente (hijo)

    void Start()
    {
        // Enlaces de los Sliders con las funciones de rotación del cañón
        // Estos listeners pueden ir en ShooterUI, pero también funcionan aquí
        // si los Sliders están en el mismo objeto que este script.
        if (angleSlider != null)
        {
            angleSlider.onValueChanged.AddListener(UpdateAngle);
        }
        if (horizontalSlider != null)
        {
            horizontalSlider.onValueChanged.AddListener(UpdateHorizontal);
        }
    }

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

        // Calcula la dirección del proyectil usando el ángulo actual
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 dir = firePoint.forward * Mathf.Cos(rad) + firePoint.up * Mathf.Sin(rad);

        rb.AddForce(dir.normalized * force, ForceMode.Impulse);
    }

    // Funciones para actualizar los parámetros del cañón
    // Estas funciones son llamadas por los Sliders y el Dropdown en el script ShooterUI
    public void UpdateAngle(float value)
    {
        currentAngle = value;
        if (cannonBarrel != null)
        {
            cannonBarrel.localRotation = Quaternion.Euler(value, 0, 0);
        }
    }

    public void UpdateHorizontal(float value)
    {
        if (cannonBase != null)
        {
            cannonBase.localRotation = Quaternion.Euler(0, value, 0);
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
}