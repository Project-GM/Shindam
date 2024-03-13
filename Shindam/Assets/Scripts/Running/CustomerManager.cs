using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public float spawnCooldown = 7f;

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
        StartSpawning();
    }

    void StartSpawning()
    {
        if (RunningManager.instance.isOpen && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnCustomerAfterDelay(spawnCooldown));
        }
    }

    IEnumerator SpawnCustomerAfterDelay(float delay)
    {
        while (RunningManager.instance.isOpen)
        {
            yield return new WaitForSeconds(delay);
            if (spawnPoint.transform.childCount < 3)
            {
                // 손님 생성
                customer = Instantiate(customerPrefab) as GameObject;
                customer.transform.parent = spawnPoint;
                customer.transform.position = spawnPoint.position;
            }
        }
    }
}

