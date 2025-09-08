using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShooterUI : MonoBehaviour
{
    public Shooter shooter;
    public Slider angleSlider;
    public Slider forceSlider;
    public Dropdown massDropdown;

    [Header("Botones de rotación horizontal")]
    public Button rotateLeftButton;
    public Button rotateRightButton;

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    void Start()
    {
        if (angleSlider != null)
            angleSlider.onValueChanged.AddListener(shooter.UpdateAngle);

        if (forceSlider != null)
            forceSlider.onValueChanged.AddListener(shooter.UpdateForce);

        if (massDropdown != null)
            massDropdown.onValueChanged.AddListener(shooter.UpdateMass);

        if (rotateLeftButton != null)
            AddPointerListeners(rotateLeftButton, "left");

        if (rotateRightButton != null)
            AddPointerListeners(rotateRightButton, "right");
    }

    private void AddPointerListeners(Button button, string direction)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Pointer Down
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) =>
        {
            if (direction == "left") isLeftPressed = true;
            else if (direction == "right") isRightPressed = true;
        });
        trigger.triggers.Add(pointerDown);

        // Pointer Up
        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((data) =>
        {
            if (direction == "left") isLeftPressed = false;
            else if (direction == "right") isRightPressed = false;
        });
        trigger.triggers.Add(pointerUp);
    }

    void Update()
    {
        if (isLeftPressed)
            shooter.RotateLeftContinuous();

        if (isRightPressed)
            shooter.RotateRightContinuous();
    }
}
