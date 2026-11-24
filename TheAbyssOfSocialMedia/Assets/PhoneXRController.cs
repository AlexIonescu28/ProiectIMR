using UnityEngine;

public class PhoneXRController : MonoBehaviour
{
    public GameObject phoneCanvas; // Trage aici CanvasTelefon
    private bool isOpen = false;

    void Start()
    {
        if (phoneCanvas != null)
            phoneCanvas.SetActive(false);
    }

    // Aceasta functie va fi apelata automat cand apesi G (Select) pe telefon
    public void TogglePhoneScreen()
    {
        isOpen = !isOpen;
        if (phoneCanvas != null)
            phoneCanvas.SetActive(isOpen);
    }
}