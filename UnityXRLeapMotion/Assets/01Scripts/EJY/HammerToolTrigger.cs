using System.Collections.Generic;
using UnityEngine;

public class HammerToolTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var chisel = other.GetComponent<ChiselToolTrigger>();
        if (chisel != null)
        {
            chisel.ActivateByHammer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var chisel = other.GetComponent<ChiselToolTrigger>();
        if (chisel != null)
        {
            chisel.DeactivateByHammer();
        }
    }
}
