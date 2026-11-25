using UnityEngine;

public class PhoneXRController : MonoBehaviour
{
    public GameObject phoneCanvas; 
    private bool isOpen = false;

    void Start()
    {
        if (phoneCanvas != null)
            phoneCanvas.SetActive(false);
    }

    
    public void TogglePhoneScreen()
    {
        isOpen = !isOpen;
        if (phoneCanvas != null)
            phoneCanvas.SetActive(isOpen);
    }
}