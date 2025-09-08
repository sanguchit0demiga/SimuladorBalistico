using UnityEngine;
using UnityEngine.UI;

public class ShooterUI : MonoBehaviour
{
    public Shooter shooter;
    public Slider angleSlider;
    public Slider forceSlider;
    public Dropdown massDropdown;

    [Header("Sliders de Rotaci�n")]
    public Slider horizontalSlider; // El slider que controla la rotaci�n horizontal

    void Start()
    {
        // Enlaces para la l�gica de disparo y movimiento vertical
        angleSlider.onValueChanged.AddListener(shooter.UpdateAngle);

        // Enlace para la l�gica de disparo (fuerza y masa)
        forceSlider.onValueChanged.AddListener(shooter.UpdateForce);
        massDropdown.onValueChanged.AddListener(shooter.UpdateMass);

        // Nuevo enlace para el slider de rotaci�n horizontal
        horizontalSlider.onValueChanged.AddListener(shooter.UpdateHorizontal);
    }

    // Las funciones de UpdateAngle, UpdateForce y UpdateMass ya no son necesarias aqu�
    // porque se llaman directamente a las funciones del script Shooter.
    // Solo si necesitas alguna l�gica extra en la UI, las dejar�as.
}