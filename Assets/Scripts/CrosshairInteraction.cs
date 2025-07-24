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

        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green);

            CustomerAI customer = hit.collider.GetComponent<CustomerAI>();
            if (customer != null && customer.CanReceiveOrder())
            {
                interactionText.text = "[E] Take Order";
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    customer.ReceiveOrder();
                    interactionText.gameObject.SetActive(false);
                }
                return;
            }
        }

        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }
}
