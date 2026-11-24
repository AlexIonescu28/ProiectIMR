using UnityEngine;

public class PhoneInteract : MonoBehaviour
{
    [Header("Setari Telefon")]
    public GameObject phoneCanvas; // Trage aici PhoneScreenCanvas
    public float interactDistance = 3f; // Distanta maxima de la care poti apasa G
    public KeyCode interactKey = KeyCode.G;

    private Camera playerCamera;
    private Collider phoneCollider;
    private bool isPhoneOpen = false;

    void Start()
    {
        playerCamera = Camera.main;
        phoneCollider = GetComponent<Collider>();

        if (phoneCanvas != null)
            phoneCanvas.SetActive(false);
    }

    void Update()
    {
        // Daca telefonul e deja deschis si apesi G sau Escape, il inchide
        if (isPhoneOpen && (Input.GetKeyDown(interactKey) || Input.GetKeyDown(KeyCode.Escape)))
        {
            TogglePhone(false);
            return;
        }

        // Verifica daca te uiti la telefon si apesi G
        if (!isPhoneOpen && Input.GetKeyDown(interactKey))
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.collider == phoneCollider)
                {
                    TogglePhone(true);
                }
            }
        }
    }

    public void TogglePhone(bool state)
    {
        isPhoneOpen = state;
        if (phoneCanvas != null)
            phoneCanvas.SetActive(isPhoneOpen);

        // Deblocheaza cursorul mouse-ului ca sa poti apasa pe butoanele din telefon
        Cursor.lockState = isPhoneOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPhoneOpen;
    }
}