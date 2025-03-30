using System.Collections.Generic;
using UnityEngine;

public class ToolContactTrigger : MonoBehaviour
{
    [Tooltip("�� ������ ������ ��� ������Ʈ�� �±�")]
    public string targetTag = "Rock1";  // ������ ���� �ٸ� �±� ����

    private HashSet<Collider> triggeredColliders = new HashSet<Collider>(); //�̹� ������ ��ȣ�ۿ��� ������Ʈ�� �� ��ȣ�ۿ� ���ϰ� ��

    private void OnTriggerEnter(Collider other)
    {
        // �̹� ó���� ������Ʈ�� ����
        if (triggeredColliders.Contains(other)) return;

        // �±װ� ��ġ�� ���� �۵�
        if (other.CompareTag(targetTag))
        {
            triggeredColliders.Add(other);

            Debug.Log($"[{name}] {targetTag} �±��� {other.name} ���� �浹 ����. �̺�Ʈ �߻�!");

            // TODO: ���⿡ ���� �̺�Ʈ �ڵ� �ۼ�
        }
    }
}

