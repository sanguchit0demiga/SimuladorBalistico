using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShooterUI : MonoBehaviour
{
    public Shooter shooter;
    public Slider angleSlider;
    public Slider forceSlider; // Declaración correcta
    public Dropdown massDropdown;

    [Header("Botones de rotación horizontal")]
    public Button rotateLeftButton;
    public Button rotateRightButton;

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    void Start()
    {
        // Vinculación de sliders y dropdown
        if (angleSlider != null)
            angleSlider.onValueChanged.AddListener(shooter.UpdateAngle);

        if (forceSlider != null)
            forceSlider.onValueChanged.AddListener(shooter.UpdateForce);

        if (massDropdown != null)
            massDropdown.onValueChanged.AddListener(shooter.UpdateMass);

        // Vinculación de botones con eventos de puntero
        if (rotateLeftButton != null)
            AddPointerListeners(rotateLeftButton, "left");

        if (rotateRightButton != null)
            AddPointerListeners(rotateRightButton, "right");
    }

    private void AddPointerListeners(Button button, string direction)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        // Si el objeto no tiene EventTrigger, se lo añadimos
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        // Lógica de Pointer Down
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) =>
        {
            if (direction == "left") isLeftPressed = true;
            else if (direction == "right") isRightPressed = true;
        });
        trigger.triggers.Add(pointerDown);

        // Lógica de Pointer Up
        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((data) =>
        {
            isLeftPressed = false;
            isRightPressed = false;
        });
        trigger.triggers.Add(pointerUp);
    }

    void Update()
    {
        // Llamada continua mientras los botones están presionados
        if (isLeftPressed)
            shooter.RotateLeftContinuous();

        if (isRightPressed)
            shooter.RotateRightContinuous();
    }
}