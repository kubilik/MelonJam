using UnityEngine;
using UnityEngine.AI; 
using TMPro;

public class CustomerAI : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject targetSunbed;

    public GameObject orderUIPrefab;
    private GameObject orderUIInstance;

    public float stopDistance = 1f;
    private bool hasOrdered = false;

    private OrderType currentOrder;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject[] sunbeds = GameObject.FindGameObjectsWithTag("Sunbed");

        if (sunbeds.Length > 0)
        {
            targetSunbed = FindClosestSunbed(sunbeds);
            agent.SetDestination(targetSunbed.transform.position);
        }

        // Sipari�i rastgele se�
        currentOrder = (OrderType)Random.Range(0, System.Enum.GetValues(typeof(OrderType)).Length);
    }

    void Update()
    {
        if (!hasOrdered && targetSunbed != null)
        {
            float dist = Vector3.Distance(transform.position, targetSunbed.transform.position);
            if (dist <= stopDistance)
            {
                agent.isStopped = true;
                ShowOrderUI();
                hasOrdered = true;
            }
        }

        if (orderUIInstance != null)
        {
            orderUIInstance.transform.position = transform.position + Vector3.up * 2f;
        }
    }

    GameObject FindClosestSunbed(GameObject[] sunbeds)
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject sunbed in sunbeds)
        {
            float dist = Vector3.Distance(sunbed.transform.position, currentPos);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = sunbed;
            }
        }

        return closest;
    }

    void ShowOrderUI()
    {
        if (orderUIPrefab != null)
        {
            orderUIInstance = Instantiate(orderUIPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);

            // OrderUI prefab�� i�inde "OrderIcon" ad�nda bir Text veya Image olmal�
            TextMeshProUGUI txt = orderUIInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (txt != null)
            {
                txt.text = currentOrder.ToString();
            }

        }
    }
}
