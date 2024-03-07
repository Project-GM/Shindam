using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 채집물 오브젝트 스폰을 위한 스폰 매니저
/// <summary>
[System.Serializable]
public class SpawnPercentage
{
    public GameObject plantPrefab;
    [Range(0, 1)] public float percentage;
}
public class PlantSpawnManager : MonoBehaviour
{
    public int maxSpawnCount;
    public GameObject spawnPointsParent;
    public List<SpawnPercentage> spawnTable = new List<SpawnPercentage>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    private GameObject spawnPlant;
    private List<float> probs = new List<float>();
    private void Start()
    {
        for (int i = 0; i < spawnTable.Count; i++) //설정한 스폰 테이블의 확률에 따라 확률 테이블 생성
        {
            probs.Add(spawnTable[i].percentage);
        }
        for (int i = 0; i < spawnPointsParent.transform.childCount; i++) //스폰 포인트 등록
        {
            spawnPoints.Add(spawnPointsParent.transform.GetChild(i).gameObject);
        }
        SpawnPlant();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //테스트용 채집물 리스폰 키
        {
            ResetPlant();
            SpawnPlant();
        }
    }
    public void SpawnPlant() //채집물 스폰 함수
    {
        for (int i = 0; i < maxSpawnCount; i++) 
        {
            int index = Random.Range(0, 5);
            if (spawnPoints[index].activeSelf) //이미 스폰된 위치면 랜덤 값 다시 뽑기
            {
                i--;
                continue;
            }
            spawnPlant = spawnTable[Choose()].plantPrefab; //확률에 따른 스폰될 채집물 선택
            spawnPoints[index].SetActive(true);
            GameObject plant = Instantiate(spawnPlant, spawnPoints[index].transform.position, Quaternion.identity, spawnPoints[index].transform); //채집물 스폰
        }
    }
    public void ResetPlant() //스폰 리셋(테스트 용)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i].activeSelf) 
            {
                Destroy(spawnPoints[i].transform.GetChild(0).gameObject);
                spawnPoints[i].SetActive(false);
            }
        }
    }
    public int Choose() //확률 테이블을 통해 인덱스를 반환하는 함수
    {
        float total = 0;

        foreach(float element in probs) total += element; //총합이 1이 아니어도 됨

        float randomPoint = Random.value * total;

        for(int i=0;i< probs.Count; i++) //확률 테이블 돌면서 조건을 만족하는 인덱스 반환
        {
            if (randomPoint <= probs[i]) return i;
            else randomPoint -= probs[i];  
        }
        return probs.Count - 1;
    }
}
