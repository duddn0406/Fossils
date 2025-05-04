using System.Collections.Generic;
using UnityEngine;

public class ChiselToolTrigger : MonoBehaviour
{
    public string rockTag = "Rock6"; // chisel이 상호작용할 암석 태그
    public bool isActivatedByHammer = false;

    private HashSet<Collider> triggeredRocks = new HashSet<Collider>();

    public void ActivateByHammer()
    {
        isActivatedByHammer = true;
        Debug.Log("[Chisel] 해머와 접촉 중 → 활성화됨");
    }

    public void DeactivateByHammer()
    {
        isActivatedByHammer = false;
        Debug.Log("[Chisel] 해머와 분리됨 → 비활성화됨");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivatedByHammer) return; // 해머에 의해 활성화된 상태가 아니면 무시

        if (other.CompareTag(rockTag) && !triggeredRocks.Contains(other))
        {
            triggeredRocks.Add(other);
            Debug.Log($"[Chisel] 암석({other.name})과 상호작용 발생!");

            // TODO: 상호작용 처리 코드 (파괴 등)
        }
    }
}
