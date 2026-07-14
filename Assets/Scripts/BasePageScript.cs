using UnityEngine;
using System.Collections.Generic;

public class BasePageScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _basePageUnderLines;
    public List<GameObject> _baseLines = new List<GameObject>();

    [SerializeField] private GameObject _baseLineObject;
    [SerializeField] private Transform _container;

    [Header("Teleport Positions")]
    [SerializeField] private Transform _pageOnPosition;
    [SerializeField] private Transform _pageOffPosition;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            _baseLines.Add(null);
        }
    }

    public void UnderLinerButtonPressed(int underLinerNumber)
    {
        if (_baseLines[underLinerNumber] == null)
        {
            GameObject line = Instantiate(_baseLineObject.gameObject, _container);
            _baseLines[underLinerNumber] = line;
        }
        else
        {
            _baseLines[underLinerNumber].gameObject.transform.position = _pageOnPosition.position;
        }
    }

    public void PageReset()
    {
        for (int i=0;i<_baseLines.Count;i++)
        {
            if (_baseLines[i] == null) continue;
            _baseLines[i].gameObject.transform.position = _pageOffPosition.position;
        }
    }
}
