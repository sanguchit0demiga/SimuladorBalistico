using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShooterUI : MonoBehaviour
{
    public Shooter shooter;
    public Slider angleSlider;
    public Slider forceSlider; // Declaraci�n correcta
    public Dropdown massDropdown;

    [Header("Botones de rotaci�n horizontal")]
    public Button rotateLeftButton;
    public Button rotateRightButton;

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    void Start()
    {
        // Vinculaci�n de sliders y dropdown
        if (angleSlider != null)
            angleSlider.onValueChanged.AddListener(shooter.UpdateAngle);

        if (forceSlider != null)
            forceSlider.onValueChanged.AddListener(shooter.UpdateForce);

        if (massDropdown != null)
            massDropdown.onValueChanged.AddListener(shooter.UpdateMass);

        // Vinculaci�n de botones con eventos de puntero
        if (rotateLeftButton != null)
            AddPointerListeners(rotateLeftButton, "left");

        if (rotateRightButton != null)
            AddPointerListeners(rotateRightButton, "right");
    }

    private void AddPointerListeners(Button button, string direction)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        // Si el objeto no tiene EventTrigger, se lo a�adimos
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        // L�gica de Pointer Down
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) =>
        {
            if (direction == "left") isLeftPressed = true;
            else if (direction == "right") isRightPressed = true;
        });
        trigger.triggers.Add(pointerDown);

        // L�gica de Pointer Up
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
        // Llamada continua mientras los botones est�n presionados
        if (isLeftPressed)
            shooter.RotateLeftContinuous();

        if (isRightPressed)
            shooter.RotateRightContinuous();
    }
}