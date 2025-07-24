using UnityEngine;
using TMPro;

public class CrosshairInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public TextMeshProUGUI interactionText;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        interactionText.gameObject.SetActive(false); // Ba�ta g�r�nmesin
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green); // DEBUG �izgisi
            Debug.Log("Ray �arpt�: " + hit.collider.name); // NEYE �arpt���n� yazd�r

            CustomerAI customer = hit.collider.GetComponent<CustomerAI>();
            if (customer != null && customer.CanReceiveOrder())
            {
                interactionText.text = "[E] Sipari�i Al";
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    customer.ReceiveOrder();
                    interactionText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
