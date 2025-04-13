using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("��ȯ�� ������ ����Ʈ (index��)")]
    public GameObject[] prefabList = new GameObject[5];

    [Header("�������� ��ȯ�� ��ġ")]
    public Transform spawnTarget; // Hierarchy�� �ִ� �� ������Ʈ ��ġ

    private GameObject currentInstance;

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
        }

        // ȸ�� ���� (Y: 90��, Z: 90��)
        Quaternion rotation = Quaternion.Euler(0f, 90f, 90f);

        // �� ������ ��ȯ
        currentInstance = Instantiate(prefabList[index], spawnTarget.position, rotation);
    }
}
