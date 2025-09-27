using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShooterUI : MonoBehaviour
{
    public Shooter shooter;
    public Slider angleSlider;
    public Slider forceSlider; 
    public Dropdown massDropdown;

   
    public Button rotateLeftButton;
    public Button rotateRightButton;

    private bool isLeftPressed = false;
    private bool isRightPressed = false;
    public GameObject resultsPanel;
    public Button closeButton;

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

        if (resultsPanel !=null)
            resultsPanel.SetActive(false);
        if (closeButton != null)
            closeButton.onClick.AddListener(() => resultsPanel.SetActive(false));
    }
    public void ShowResultsPanel()
    {
        if (resultsPanel != null)
            resultsPanel.SetActive(true);
    }
    public void HideResultsPanel()
    {
        if (resultsPanel != null)
            resultsPanel.SetActive(false);
    }
    private void AddPointerListeners(Button button, string direction)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

      
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) =>
        {
            if (direction == "left") isLeftPressed = true;
            else if (direction == "right") isRightPressed = true;
        });
        trigger.triggers.Add(pointerDown);

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
        if (isLeftPressed)
            shooter.RotateLeftContinuous();

        if (isRightPressed)
            shooter.RotateRightContinuous();
    }
}