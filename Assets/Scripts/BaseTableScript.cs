using UnityEngine;
using TMPro;

public class BaseTableScript : MonoBehaviour
{
    public ManagerScript manager;
    public int _baseTableNumber;
    [SerializeField] private TMP_Text[] _orderNumberText;

    public void OpenTableButton()
    {
        manager.OpenPage(_baseTableNumber);
    }
}
