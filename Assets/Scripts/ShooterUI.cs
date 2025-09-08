using UnityEngine;
using UnityEngine.UI;

public class ShooterUI : MonoBehaviour
{
    public Shooter shooter;
    public Slider angleSlider;
    public Slider forceSlider;
    public Dropdown massDropdown;

    [Header("Sliders de Rotación")]
    public Slider horizontalSlider; // El slider que controla la rotación horizontal

    void Start()
    {
        // Enlaces para la lógica de disparo y movimiento vertical
        angleSlider.onValueChanged.AddListener(shooter.UpdateAngle);

        // Enlace para la lógica de disparo (fuerza y masa)
        forceSlider.onValueChanged.AddListener(shooter.UpdateForce);
        massDropdown.onValueChanged.AddListener(shooter.UpdateMass);

        // Nuevo enlace para el slider de rotación horizontal
        horizontalSlider.onValueChanged.AddListener(shooter.UpdateHorizontal);
    }

    // Las funciones de UpdateAngle, UpdateForce y UpdateMass ya no son necesarias aquí
    // porque se llaman directamente a las funciones del script Shooter.
    // Solo si necesitas alguna lógica extra en la UI, las dejarías.
}