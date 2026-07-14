using UnityEngine;

public class BaseTableScript : MonoBehaviour
{
    public ManagerScript manager;
    public int _baseTableNumber;


    public void OpenTableButton()
    {
        manager.OpenPage(_baseTableNumber);
    }
}
