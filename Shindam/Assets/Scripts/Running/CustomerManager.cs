using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public float spawnCooldown = 1f;

    private GameObject customer;
    bool isSpawning = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {

    }

    void Update()
    {
        if (!isSpawning)
        {
            StartSpawning();
        }
    }

    void StartSpawning()
    {
        if (RunningManager.instance.isOpen && !isSpawning)
        {
            isSpawning = true;
            Debug.Log("코루틴실행");
            StartCoroutine(SpawnCustomerAfterDelay(spawnCooldown));
        }
    }

    IEnumerator SpawnCustomerAfterDelay(float delay)
    {
        while (RunningManager.instance.isOpen)
        {
            if (spawnPoint.transform.childCount < 5)
            {
                yield return new WaitForSeconds(delay);
                // 손님 생성
                customer = Instantiate(customerPrefab) as GameObject;
                Debug.Log(spawnPoint.transform.childCount + "번째 손님 생성");
                customer.transform.parent = spawnPoint;
                customer.transform.position = spawnPoint.position;
            }
            else
            {
                yield return null;
            }
        }
    }
}

