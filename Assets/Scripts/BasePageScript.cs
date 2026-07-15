using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePageScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _basePageUnderLines;
    public List<GameObject> _baseLines = new List<GameObject>();

    [SerializeField] private GameObject _baseLineObject;
    [SerializeField] private Transform _container;

    [SerializeField] private Transform _lineOnPosition;
    [SerializeField] private Transform _lineOffPosition;

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
        yield return new WaitForSeconds(0.5f);

        if (_baseLines[number] == null)
        {
            GameObject newLine = Instantiate(_baseLineObject, _container);
            _baseLines[number] = newLine;
        }

        GameObject line = _baseLines[number];
        LineObjectScript script = line.GetComponent<LineObjectScript>();

        line.transform.position = _lineOnPosition.position;
        script.ChangeOrderMainText(number);
    }

    public void PageReset()
    {
        Debug.Log("BASE PAGE ON: " + _lineOnPosition.position);
        Debug.Log("BASE PAGE OFF: " + _lineOffPosition.position);

        for (int i = 0; i < _baseLines.Count; i++)
        {
            if (_baseLines[i] == null) continue;

            Debug.Log("LINE BEFORE: " + _baseLines[i].transform.position);

            _baseLines[i].transform.position = _lineOffPosition.position;

            Debug.Log("LINE AFTER: " + _baseLines[i].transform.position);
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
