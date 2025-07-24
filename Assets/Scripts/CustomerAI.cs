using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class CustomerAI : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject targetSunbed;

    public GameObject orderUIPrefab;
    private GameObject orderUIInstance;

    public float stopDistance = 2f;
    private bool hasArrived = false;

    private OrderType currentOrder;

    public float orderWaitDuration = 5f;
    private float orderWaitTimer = 0f;
    private bool isWaitingForOrder = false;
    private bool orderGiven = false;
    private bool isUnhappy = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject[] sunbeds = GameObject.FindGameObjectsWithTag("Sunbed");
        if (sunbeds.Length > 0)
        {
            targetSunbed = FindClosestSunbed(sunbeds);
            agent.SetDestination(targetSunbed.transform.position);
        }

        // Rastgele sipari� t�r� belirle (sipari� hen�z verilmedi!)
        currentOrder = (OrderType)Random.Range(0, System.Enum.GetValues(typeof(OrderType)).Length);
    }

    void Update()
    {
        // �ezlonga vard���nda dur
        if (!hasArrived && targetSunbed != null)
        {
            float dist = Vector3.Distance(transform.position, targetSunbed.transform.position);
            if (dist <= stopDistance)
            {
                agent.isStopped = true;
                hasArrived = true;
            }
        }

        // UI sabit dursun
        if (orderUIInstance != null)
        {
            orderUIInstance.transform.position = transform.position + Vector3.up * 2f;
        }

        // Sipari� ald�ktan sonra bekleme s�resi �al��s�n
        if (isWaitingForOrder && orderGiven == false)
        {
            orderWaitTimer -= Time.deltaTime;

            Slider slider = orderUIInstance.GetComponentInChildren<Slider>();
            if (slider != null)
            {
                slider.value = orderWaitTimer / orderWaitDuration;
            }

            if (orderWaitTimer <= 0f)
            {
                isWaitingForOrder = false;
                isUnhappy = true;
                Debug.Log("Sipari� zaman�nda al�nmad� ? m��teri mutsuz.");
            }
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

    public bool CanReceiveOrder()
    {
        return hasArrived && !orderGiven && !isUnhappy;
    }

    public void ReceiveOrder()
    {
        orderGiven = true;
        isWaitingForOrder = true;
        orderWaitTimer = orderWaitDuration;

        Debug.Log("Sipari� al�nd�: " + currentOrder);

        // UI olu�tur
        if (orderUIPrefab != null)
        {
            orderUIInstance = Instantiate(orderUIPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);

            TextMeshProUGUI txt = orderUIInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (txt != null)
            {
                txt.text = "Order: " + currentOrder.ToString();
            }

            Slider slider = orderUIInstance.GetComponentInChildren<Slider>();
            if (slider != null)
            {
                slider.maxValue = 1f;
                slider.value = 1f;
            }
        }

        // (�stersen ileride burada StartDeliveryTimer �a��rabiliriz)
    }

    void StartDeliveryTimer()
    {
        // TODO: Sipari�i yeti�tirme s�resi eklenecek
    }
}
