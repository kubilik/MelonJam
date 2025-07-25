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

            // 1. Pick up ingredient (excluding empty cup and finished taco)
            IngredientPickup pickup = hit.collider.GetComponent<IngredientPickup>();
            if (pickup != null && pickup.type != IngredientType.EmptyCup && pickup.type != IngredientType.FinishedTaco && !inventory.IsCarrying())
            {
                interactionText.text = "[E] Pick up: " + pickup.type.ToString();
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.PickUpIngredient(pickup.type, pickup.visualPrefab);
                    Debug.Log("Picked up: " + pickup.type);
                    // Do NOT destroy here — only EmptyCup should be destroyed later
                }
                return;
            }


            // 2. Place on prep counter (only if type is accepted)
            PrepCounter prep = hit.collider.GetComponent<PrepCounter>();
            if (prep != null && inventory.IsCarrying())
            {
                IngredientType carried = inventory.PeekIngredient();

                if (System.Array.Exists(prep.acceptedTypes, t => t == carried))
                {
                    interactionText.text = "[E] Place on counter";
                    interactionText.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        inventory.DropIngredient();
                        prep.builder.AddIngredient(carried);
                        Debug.Log("Placed on prep counter: " + carried);
                    }
                }
                else
                {
                    interactionText.text = "Cannot place this item here";
                    interactionText.gameObject.SetActive(true);
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

            // 4. Pick up finished taco (NEW)
            if (hit.collider.CompareTag("FinishedTaco") && !inventory.IsCarrying())
            {
                interactionText.text = "[E] Pick up Taco";
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.PickUpIngredient(IngredientType.FinishedTaco, handHeldTacoPrefab);
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Picked up finished taco");
                }
                return;
            }

            // 5. Take cup from dispenser
            CupDispenser dispenser = hit.collider.GetComponent<CupDispenser>();
            if (dispenser != null && !inventory.IsCarrying())
            {
                if (!dispenser.HasCupReady())
                {
                    interactionText.text = "[E] Take cup";
                    interactionText.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        dispenser.DispenseCup();
                        Debug.Log("Dispensed empty cup");
                    }
                }
                return;
            }

            // 6. Pick up empty cup from scene
            IngredientPickup cup = hit.collider.GetComponent<IngredientPickup>();
            if (cup != null && cup.type == IngredientType.EmptyCup && !inventory.IsCarrying())
            {
                interactionText.text = "[E] Take cup";
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.PickUpIngredient(cup.type, cup.visualPrefab);

                    if (cup.originatingDispenser != null)
                    {
                        cup.originatingDispenser.ClearCup();
                    }

                    CupPlacementPoint placement = cup.GetComponentInParent<CupPlacementPoint>();
                    if (placement != null)
                    {
                        placement.ClearCup();
                    }

                    Destroy(cup.gameObject);
                    Debug.Log("Picked up empty cup");
                }
                return;
            }
        }

        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }
}
