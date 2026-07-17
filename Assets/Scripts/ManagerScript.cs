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

    private int _currentPage = -1;
    private BasePageScript pageScript;
    private SaveData[,] _loadedPages;


    private void Start()
    {
        InitializeBaseTables();
        InitializeBasePages();
        InitializeLoad();
        FillOrderNumbers(false);
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

    private void InitializeBasePages()
    {
        for (int i = 0; i < _basePages.Length; i++)
        {
            _basePages[i].position = _pageOffPosition.position;
        }
    }

    public void EscapeButtonPressed()
    {
        if (_currentPage == -1) return;

        BasePageSave();

        BasePageScript script = _basePages[_currentPage].GetComponent<BasePageScript>();

        script.PageReset();

        _basePages[_currentPage].position = _pageOffPosition.position;

        _currentPage = -1;
        FillOrderNumbers(false);
        _escapeButton.SetActive(false);
    }

    public void InitializeLoad()
    {
        _loadedPages = new SaveData[_baseTables.Length,_basePages.Length];

        for (int i = 0; i < _basePages.Length; i++)
        {
            BasePageScript page = _basePages[i].GetComponent<BasePageScript>();
            BaseTableScript table = _baseTables[i].GetComponent<BaseTableScript>();
            string path = Path.Combine(Application.persistentDataPath, $"BaseTable{table._baseTableNumber}BasePage{page._pageIndex}.json");

            if (!File.Exists(path)) continue;

            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            for (int j = 0; j < page._baseLines.Count; j++)
            {
                if (page._baseLines[j] == null) continue;

                LineObjectScript line = page._baseLines[j].GetComponent<LineObjectScript>();
                line._orderNumberText.text = saveData._orderNumberText[j];
                line._bucketsText.text = saveData._bucketsText[j];
                line._capsText.text = saveData._capsText[j];
                line._boxesText.text = saveData._boxesText[j];
                line._pallets_done_text.text = saveData._palletsDoneText[j];
                line._pallets_total_text.text = saveData._palletsTotalText[j];
                line._buckets_total_text.text = saveData._bucketsTotalText[j];
                line._buckets_left_text.text = saveData._bucketsLeftText[j];
                line._modify_bucket_input_text.text = saveData._modify_bucket_input_text[j];
            }

            File.WriteAllText(path, json);

            _loadedPages[table._baseTableNumber, page._pageIndex] = JsonUtility.FromJson<SaveData>(json);
        }
    }

    public void LoadLine(int tableIndex, int pageIndex, int lineIndex, LineObjectScript line)
    {
        SaveData saveData = _loadedPages[tableIndex,pageIndex];

        if (saveData == null) return;

        if (saveData._orderNumberText[lineIndex] != "") line._orderNumberText.text = saveData._orderNumberText[lineIndex];
        if (saveData._bucketsText[lineIndex] != "") line._bucketsText.text = saveData._bucketsText[lineIndex];
        if (saveData._capsText[lineIndex] != "") line._capsText.text = saveData._capsText[lineIndex];
        if (saveData._boxesText[lineIndex] != "") line._boxesText.text = saveData._boxesText[lineIndex];
        if (saveData._palletsDoneText[lineIndex] != "") line._pallets_done_text.text = saveData._palletsDoneText[lineIndex];
        if (saveData._palletsTotalText[lineIndex] != "") line._pallets_total_text.text = saveData._palletsTotalText[lineIndex];
        if (saveData._bucketsTotalText[lineIndex] != "") line._buckets_total_text.text = saveData._bucketsTotalText[lineIndex];
        if (saveData._bucketsLeftText[lineIndex] != "") line._buckets_left_text.text = saveData._bucketsLeftText[lineIndex];
        if (saveData._modify_bucket_input_text[lineIndex] != "") line._modify_bucket_input_text.text = saveData._modify_bucket_input_text[lineIndex];
    }

    #endregion

    public void OpenPage(int number)
    {
        _currentPage = number;
        _basePages[number].position = _pageOnPosition.position;
        _escapeButton.SetActive(true);
    }

    public void BasePageSave()
    {
        for (int i = 0; i < _basePages.Length; i++)
        {
            BasePageScript page = _basePages[i].GetComponent<BasePageScript>();
            BaseTableScript table = _baseTables[i].GetComponent<BaseTableScript>();
            SaveData saveData = _loadedPages[table._baseTableNumber,page._pageIndex];

            if (saveData == null) saveData = new SaveData();

            for (int j = 0; j < page._baseLines.Count; j++)
            {
                if (page._baseLines[j] == null) continue;

                LineObjectScript line = page._baseLines[j].GetComponent<LineObjectScript>();

                saveData._orderNumberText[j] = line._orderNumberText.text;
                saveData._bucketsText[j] = line._bucketsText.text;
                saveData._capsText[j] = line._capsText.text;
                saveData._boxesText[j] = line._boxesText.text;
                saveData._palletsDoneText[j] = line._pallets_done_text.text;
                saveData._palletsTotalText[j] = line._pallets_total_text.text;
                saveData._bucketsTotalText[j] = line._buckets_total_text.text;
                saveData._bucketsLeftText[j] = line._buckets_left_text.text;
                saveData._modify_bucket_input_text[j] = line._modify_bucket_input_text.text;
            }

            _loadedPages[table._baseTableNumber, page._pageIndex] = saveData;
            string json = JsonUtility.ToJson(saveData);

            string path = Path.Combine(Application.persistentDataPath, $"BaseTable{table._baseTableNumber}BasePage{page._pageIndex}.json");

            File.WriteAllText(path, json);
        }
    }

    public void FillOrderNumbers(bool deleted)
    {
        for (int i = 0; i < _baseTables.Length; i++)
        {
            BaseTableScript table = _baseTables[i].GetComponent<BaseTableScript>();
            BasePageScript page = table._basePage.GetComponent<BasePageScript>();
            SaveData saveData = _loadedPages[table._baseTableNumber, table._basePage._pageIndex];

            if (saveData == null) continue;

            if (!deleted)
            {
                for (int j = 0; j < table._orderNumberText.Length; j++)
                {
                    table._orderNumberText[j].text = string.IsNullOrEmpty(saveData._orderNumberText[j])
                        ? "_____.___" : saveData._orderNumberText[j];
                }
            }
            else
            {
                for (int j = 0; j < table._orderNumberText.Length; j++)
                {
                    if (page._baseLines[j] == null) table._orderNumberText[j].text = "_____.___";
                    else table._orderNumberText[j].text = saveData._orderNumberText[j];
                }
            }
        }
    }
}
