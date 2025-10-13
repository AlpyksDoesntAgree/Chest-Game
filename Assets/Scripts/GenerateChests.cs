using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChests : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private GameObject _chest;
    private float _distance = 230f;
    [SerializeField] private int _amount = 3;
    private RectTransform _rectTransform;
    void Awake()
    {
        for (int i = 0; i < _amount; i++)
        {
            GameObject spawnedChest = Instantiate(_chest, _startPos.position, Quaternion.identity);
            _rectTransform = spawnedChest.GetComponent<RectTransform>();
            _rectTransform.SetParent(_startPos);
            _rectTransform.localScale = Vector3.one;
            _rectTransform.anchoredPosition = new Vector2(_distance*i, 0);
        }
    }
}
