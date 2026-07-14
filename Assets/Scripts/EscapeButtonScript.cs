using UnityEngine;

public class EscapeButtonScript : MonoBehaviour
{
    public ManagerScript manager;

    public void EscapeInitialized()
    {
        manager.EscapeButtonPressed();
    }
}
