using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => SpawnCoinOnPos(_button.transform.position));
    }

    public void SpawnCoinOnPos(Vector2 pos)
    {
        Instantiate(coin, pos, Quaternion.identity);
    }
}
