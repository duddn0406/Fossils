using UnityEngine;

public class StampTest : MonoBehaviour
{
    public PromptController promptController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            promptController.ShowStampByLevel(0); // »ó
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            promptController.ShowStampByLevel(1); // Áß
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            promptController.ShowStampByLevel(2); // ÇÏ
        }
    }
}
