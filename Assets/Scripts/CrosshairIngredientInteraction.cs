using UnityEngine;
using TMPro;

public class CrosshairIngredientInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public TextMeshProUGUI interactionText;
    public LayerMask interactionLayer;

    public GameObject handHeldTacoPrefab;

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

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            PlayerIngredientInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerIngredientInventory>();

            // 1. Pick up ingredient
            IngredientPickup pickup = hit.collider.GetComponent<IngredientPickup>();
            if (pickup != null && !inventory.IsCarrying())
            {
                interactionText.text = "[E] Pick up: " + pickup.type.ToString();
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.PickUpIngredient(pickup.type, pickup.visualPrefab);
                    Debug.Log("Picked up: " + pickup.type);
                }
                return;
            }

            // 2. Place on prep counter
            PrepCounter prep = hit.collider.GetComponent<PrepCounter>();
            if (prep != null && inventory.IsCarrying())
            {
                interactionText.text = "[E] Place on counter";
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    IngredientType dropped = inventory.DropIngredient();
                    prep.builder.AddIngredient(dropped);
                    Debug.Log("Placed on prep counter: " + dropped);
                }
                return;
            }
             
            // 3. Discard in trash
            TrashCan trash = hit.collider.GetComponent<TrashCan>();
            if (trash != null && inventory.IsCarrying())
            {
                interactionText.text = "[E] Discard in trash";
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.DropIngredient();
                    Debug.Log("Ingredient discarded in trash");
                }
                return;
            }


            // 4. Pick up finished taco
            if (hit.collider.CompareTag("FinishedTaco") && !inventory.IsCarrying())
            {
                interactionText.text = "[E] Pick up Taco";
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.PickUpIngredient(IngredientType.None, handHeldTacoPrefab);
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Picked up finished taco");
                }
                return;
            }
        }

        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }
}
