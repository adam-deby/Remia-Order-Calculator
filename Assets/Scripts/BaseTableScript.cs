using UnityEngine;
using TMPro;

public class BaseTableScript : MonoBehaviour
{
    public ManagerScript manager;
    public int _baseTableNumber;
    public BasePageScript _basePage;
    public TMP_Text[] _orderNumberText;

    public void OpenTableButton()
    {
        manager.OpenPage(_baseTableNumber);
    }
}
