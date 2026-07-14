using UnityEngine;
using System.IO;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _baseTables;
    [SerializeField] private Transform[] _basePages;
    
    [Header("Teleport Positions")]
    [SerializeField] private Transform _pageOnPosition;
    [SerializeField] private Transform _pageOffPosition;

    [Header("Buttons")]
    [SerializeField] private GameObject _escapeButton;

    private bool _hasSaved;

    public enum Stage { Main, Table, Page }
    public Stage _stage;

    private void Start()
    {
        InitializeBaseTables();
        InitializeBasePages();
        if (_hasSaved) InitializeLoad();
        _escapeButton.SetActive(false);
        _stage = Stage.Main;
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
        switch (_stage)
        {
            case Stage.Page:

                break;
            case Stage.Table:
                //InitializeBasePages();
                //_escapeButton.SetActive(false);
                break;
            default:
                InitializeBasePages();
                _escapeButton.SetActive(false); break;
        }
    }
    private void InitializeBasePages()
    {
        for (int i=0;i<_basePages.Length;i++)
        {
            BasePageScript script = _basePages[i].GetComponent<BasePageScript>();
            script.PageReset();
            _basePages[i].gameObject.transform.position = _pageOffPosition.position;

            
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
        _basePages[number].transform.position = _pageOnPosition.position;
        _escapeButton.SetActive(true);
    }
}
