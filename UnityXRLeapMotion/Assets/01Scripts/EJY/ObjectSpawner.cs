using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("소환할 프리팹 리스트 (index별)")]
    public GameObject[] prefabList = new GameObject[7];

    [Header("프리팹을 소환할 위치")]
    public Transform spawnTarget; // Hierarchy에 있는 빈 오브젝트 위치

    private GameObject currentInstance;
    private List<GameObject> currentInstances = new List<GameObject>();

    public void SpawnObject(int index)
    {
        if (index < 0 || index >= prefabList.Length || prefabList[index] == null)
        {
            Debug.LogWarning("잘못된 프리팹 인덱스 또는 미등록된 프리팹!");
            return;
        }

        // 기존 오브젝트 제거
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
        }

        // 이전 인스턴스 리스트 초기화
        foreach (var obj in currentInstances)
        {
            if (obj != null)
                Destroy(obj);
        }
        currentInstances.Clear();

        // index 5 → 6번째와 7번째 프리팹 둘 다 소환
        if (index == 5)
        {
            if (prefabList.Length < 7 || prefabList[5] == null || prefabList[6] == null)
            {
                Debug.LogWarning("6번/7번 프리팹이 없습니다.");
                return;
            }

            // 첫 번째 프리팹 소환
            GameObject first = Instantiate(prefabList[5], spawnTarget.position, Quaternion.Euler(0f, 90f, 90f));

            // 두 번째 프리팹 소환 (위치 오프셋)
            Vector3 offsetPos = spawnTarget.position + new Vector3(0.1f, 0f, 0f); // x축으로 0.1m 옆
            GameObject second = Instantiate(prefabList[6], offsetPos, Quaternion.Euler(0f, 90f, 90f));

            // 첫 번째 것만 currentInstance로 관리 (원한다면 리스트로 관리 가능)
            currentInstance = first;

            // 리스트에 추가!
            currentInstances.Add(first);
            currentInstances.Add(second);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0f, 90f, 90f);
            GameObject obj = Instantiate(prefabList[index], spawnTarget.position, rotation);
            currentInstance = obj;
            currentInstances.Add(obj); // 리스트에 추가!
        }

        /*// 회전 지정 (Y: 90도, Z: 90도)
        Quaternion rotation = Quaternion.Euler(0f, 90f, 90f);

        // 새 프리팹 소환
        currentInstance = Instantiate(prefabList[index], spawnTarget.position, rotation);*/
    }

    public List<GameObject> GetCurrentSpawnedObjects()
    {
        return currentInstances;
    }
}
