using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint;        // Punto desde donde se dispara

    [Header("Par�metros de disparo")]
    public float force = 20f;          // Fuerza de lanzamiento
    public float projectileMass = 1f;  // Masa del proyectil
    public float currentAngle = 30f;   // �ngulo de disparo actual

    [Header("Controles UI")]
    // Estos campos no son necesarios si los Sliders est�n en otro script (ShooterUI)
    // Pero si los mantienes, aseg�rate de que no haya conflictos en el Inspector.
    [SerializeField] private Slider angleSlider;
    [SerializeField] private Slider horizontalSlider;

    [Header("Objetos del ca��n")]
    public Transform cannonBase;        // El objeto que gira horizontalmente (padre)
    public Transform cannonBarrel;      // El objeto que se inclina verticalmente (hijo)

    void Start()
    {
        // Enlaces de los Sliders con las funciones de rotaci�n del ca��n
        // Estos listeners pueden ir en ShooterUI, pero tambi�n funcionan aqu�
        // si los Sliders est�n en el mismo objeto que este script.
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

        // Calcula la direcci�n del proyectil usando el �ngulo actual
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 dir = firePoint.forward * Mathf.Cos(rad) + firePoint.up * Mathf.Sin(rad);

        rb.AddForce(dir.normalized * force, ForceMode.Impulse);
    }

    // Funciones para actualizar los par�metros del ca��n
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