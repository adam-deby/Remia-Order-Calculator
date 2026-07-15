using UnityEngine;
using System.IO;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _baseTables;
    [SerializeField] private Transform[] _basePages;
    
    [Header("Teleport Positions")]
    public Transform _pageOnPosition;
    public Transform _pageOffPosition;

    [Header("Buttons")]
    [SerializeField] private GameObject _escapeButton;

    private bool _hasSaved;
    private int _currentPage = -1;
    private BasePageScript pageScript;


    private void Start()
    {

        Debug.Log("ON: " + _pageOnPosition.position);
        Debug.Log("OFF: " + _pageOffPosition.position);
        InitializeBaseTables();
        InitializeBasePages();
        if (_hasSaved) InitializeLoad();
        _escapeButton.SetActive(false);
    }

    #region Initializers
    private void InitializeBaseTables()
    {
        int number = 0;

        for (int i = 0; i < _baseTables.Length; i++)
        {
            GameObject baseTable = _baseTables[i];
            BaseTableScript baseTableScript = baseTable.GetComponent<BaseTableScript>();
            baseTableScript._baseTableNumber = number;
            number++;
        }
    }

    public void EscapeButtonPressed()
    {
        if (_currentPage == -1) return;

        BasePageScript script = _basePages[_currentPage].GetComponent<BasePageScript>();

        script.PageReset();

        _basePages[_currentPage].position = _pageOffPosition.position;

        _currentPage = -1;
        _escapeButton.SetActive(false);
    }

    private void InitializeBasePages()
    {
        for (int i = 0; i < _basePages.Length; i++)
        {
            _basePages[i].position = _pageOffPosition.position;
        }
    }

    private void InitializeLoad()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");

        if (File.Exists(path)) // can be loaded
        {
            // code for loading
        }
    }

    #endregion

    public void OpenPage(int number)
    {
        _currentPage = number;

        _basePages[number].position = _pageOnPosition.position;
        _escapeButton.SetActive(true);
    }
}
