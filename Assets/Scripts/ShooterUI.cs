using UnityEngine;
using UnityEngine.UI;

public class ShooterUI : MonoBehaviour
{
    public Shooter shooter;
    public Slider angleSlider;
    public Slider forceSlider;
    public Dropdown massDropdown;

    void Start()
    {
        // Inicializar sliders y dropdown
        angleSlider.onValueChanged.AddListener(UpdateAngle);
        forceSlider.onValueChanged.AddListener(UpdateForce);
        massDropdown.onValueChanged.AddListener(UpdateMass);
    }

    void UpdateAngle(float value)
    {
        shooter.angle = value;
    }

    void UpdateForce(float value)
    {
        shooter.force = value;
    }

    void UpdateMass(int index)
    {
        // Ejemplo: opciones de masa 0.5, 1, 2
        float[] masses = { 0.5f, 1f, 2f };
        shooter.projectileMass = masses[index];
    }
}
