using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    [Header("Rotaciones")]
    public Transform horizontalRot;  // Objeto que gira en Y (ej. Cannon)
    public Transform verticalRot;    // Objeto que gira en X (ej. Sphere004 con FirePoint)

    [Header("Controles UI")]
    public Slider horizontalSlider;  // controla izquierda/derecha
    public Slider verticalSlider;    // controla arriba/abajo

    void Start()
    {
        // Asegúrate de que los sliders tengan el rango correcto en el Inspector
        // horizontalSlider: MinValue = -45, MaxValue = 45
        // verticalSlider: MinValue = -10, MaxValue = 30 (ejemplo, ajusta los valores que necesites)

        horizontalSlider.onValueChanged.AddListener(UpdateHorizontal);
        verticalSlider.onValueChanged.AddListener(UpdateVertical);
    }

    void UpdateHorizontal(float value)
    {
        if (horizontalRot != null)
        {
            Vector3 rot = horizontalRot.localEulerAngles;
            rot.y = value;  // Corregido: girar en Y para el movimiento horizontal
            horizontalRot.localEulerAngles = rot;
        }
    }

    void UpdateVertical(float value)
    {
        if (verticalRot != null)
        {
            Vector3 rot = verticalRot.localEulerAngles;
            rot.x = value;  // Corregido: girar en X para el movimiento vertical
            verticalRot.localEulerAngles = rot;
        }
    }
}