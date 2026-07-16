using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePageScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _basePageUnderLines;
    public List<GameObject> _baseLines = new List<GameObject>();

    [SerializeField] private GameObject _baseLineObject;
    [SerializeField] private Transform _container;
    public int _pageIndex;

    private ManagerScript manager;

    private void Start()
    {
        GameObject ms = GameObject.Find("Manager");
        manager = ms.GetComponent<ManagerScript>();

        for (int i = 0; i < 4; i++)
        {

            _baseLines.Add(null);
        }
    }

    public void UnderLinerButtonPressed(int underLinerNumber)
    {
        StartCoroutine(UnderLinerButtonPressedNumerator(underLinerNumber));
    }

    private IEnumerator UnderLinerButtonPressedNumerator(int number)
    {

        if (_baseLines[number] == null)
        {
            GameObject newLine = Instantiate(_baseLineObject, _container);
            _baseLines[number] = newLine;
        }

        GameObject line = _baseLines[number];
        LineObjectScript script = line.GetComponent<LineObjectScript>();
        manager.LoadLine(_pageIndex, number, script);
        
        yield return new WaitForSeconds(0.5f);
        
        line.transform.position = manager._pageOnPosition.position;
        script.ChangeOrderMainText(number);

    }

    public void PageReset()
    {
        for (int i = 0; i < _baseLines.Count; i++)
        {
            if (_baseLines[i] == null) continue;
            _baseLines[i].transform.position = manager._pageOffPosition.position;
        }
    }

    public void BaseLinesSort()
    {
        StartCoroutine(BaseLineSortNumerator());
    }

    private IEnumerator BaseLineSortNumerator()
    {
        yield return new WaitForSeconds(0.1f);

        _baseLines.RemoveAll(gameObject => gameObject == null);

        while (_baseLines.Count < 4) _baseLines.Add(null);
    }
}
