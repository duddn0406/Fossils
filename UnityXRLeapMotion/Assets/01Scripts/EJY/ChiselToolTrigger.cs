using System.Collections.Generic;
using UnityEngine;

public class ChiselToolTrigger : MonoBehaviour
{
    public string rockTag = "Rock6"; // chisel�� ��ȣ�ۿ��� �ϼ� �±�
    public bool isActivatedByHammer = false;

    private HashSet<Collider> triggeredRocks = new HashSet<Collider>();

    public void ActivateByHammer()
    {
        isActivatedByHammer = true;
        Debug.Log("[Chisel] �ظӿ� ���� �� �� Ȱ��ȭ��");
    }

    public void DeactivateByHammer()
    {
        isActivatedByHammer = false;
        Debug.Log("[Chisel] �ظӿ� �и��� �� ��Ȱ��ȭ��");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivatedByHammer) return; // �ظӿ� ���� Ȱ��ȭ�� ���°� �ƴϸ� ����

        if (other.CompareTag(rockTag) && !triggeredRocks.Contains(other))
        {
            triggeredRocks.Add(other);
            Debug.Log($"[Chisel] �ϼ�({other.name})�� ��ȣ�ۿ� �߻�!");

            // TODO: ��ȣ�ۿ� ó�� �ڵ� (�ı� ��)
        }
    }
}
