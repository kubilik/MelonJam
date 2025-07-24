using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject targetSunbed;

    public GameObject orderUIPrefab; // drag in Inspector
    private GameObject orderUIInstance;

    public float stopDistance = 1f;
    private bool hasOrdered = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject[] sunbeds = GameObject.FindGameObjectsWithTag("Sunbed");

        if (sunbeds.Length > 0)
        {
            targetSunbed = FindClosestSunbed(sunbeds);
            agent.SetDestination(targetSunbed.transform.position);
        }
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

        // UI’yi kafaya sabitle
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
        }
    }
}
