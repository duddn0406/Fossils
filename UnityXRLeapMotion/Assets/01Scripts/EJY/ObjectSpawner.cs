using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("소환할 프리팹 리스트 (index별)")]
    public GameObject[] prefabList = new GameObject[5];

    [Header("프리팹을 소환할 위치")]
    public Transform spawnTarget; // Hierarchy에 있는 빈 오브젝트 위치

    private GameObject currentInstance;

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
        }

        // 회전 지정 (Y: 90도, Z: 90도)
        Quaternion rotation = Quaternion.Euler(0f, 90f, 90f);

        // 새 프리팹 소환
        currentInstance = Instantiate(prefabList[index], spawnTarget.position, rotation);
    }
}
