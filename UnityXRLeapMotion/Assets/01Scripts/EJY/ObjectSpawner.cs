using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("��ȯ�� ������ ����Ʈ (index��)")]
    public GameObject[] prefabList = new GameObject[7];

    [Header("�������� ��ȯ�� ��ġ")]
    public Transform spawnTarget; // Hierarchy�� �ִ� �� ������Ʈ ��ġ

    private GameObject currentInstance;
    private List<GameObject> currentInstances = new List<GameObject>();

    public void SpawnObject(int index)
    {
        if (index < 0 || index >= prefabList.Length || prefabList[index] == null)
        {
            Debug.LogWarning("�߸��� ������ �ε��� �Ǵ� �̵�ϵ� ������!");
            return;
        }

        // ���� ������Ʈ ����
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
        }

        // ���� �ν��Ͻ� ����Ʈ �ʱ�ȭ
        foreach (var obj in currentInstances)
        {
            if (obj != null)
                Destroy(obj);
        }
        currentInstances.Clear();

        // index 5 �� 6��°�� 7��° ������ �� �� ��ȯ
        if (index == 5)
        {
            if (prefabList.Length < 7 || prefabList[5] == null || prefabList[6] == null)
            {
                Debug.LogWarning("6��/7�� �������� �����ϴ�.");
                return;
            }

            // ù ��° ������ ��ȯ
            GameObject first = Instantiate(prefabList[5], spawnTarget.position, Quaternion.Euler(0f, 90f, 90f));

            // �� ��° ������ ��ȯ (��ġ ������)
            Vector3 offsetPos = spawnTarget.position + new Vector3(0.1f, 0f, 0f); // x������ 0.1m ��
            GameObject second = Instantiate(prefabList[6], offsetPos, Quaternion.Euler(0f, 90f, 90f));

            // ù ��° �͸� currentInstance�� ���� (���Ѵٸ� ����Ʈ�� ���� ����)
            currentInstance = first;

            // ����Ʈ�� �߰�!
            currentInstances.Add(first);
            currentInstances.Add(second);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0f, 90f, 90f);
            GameObject obj = Instantiate(prefabList[index], spawnTarget.position, rotation);
            currentInstance = obj;
            currentInstances.Add(obj); // ����Ʈ�� �߰�!
        }

        /*// ȸ�� ���� (Y: 90��, Z: 90��)
        Quaternion rotation = Quaternion.Euler(0f, 90f, 90f);

        // �� ������ ��ȯ
        currentInstance = Instantiate(prefabList[index], spawnTarget.position, rotation);*/
    }

    public List<GameObject> GetCurrentSpawnedObjects()
    {
        return currentInstances;
    }
}
