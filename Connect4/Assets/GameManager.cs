using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    private Button[] _button;
    private GameObject _mousePosObject;
    public Grid grid;

    private GameObject currentCoin;
    private void Start()
    {
        SetButtons();
        InstantiateObjects();
    }

    private void Update()
    {
        GetAndSetMousePos();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetTileOppucation(currentCoin);
        }
    }

    private void SetButtons()
    {
        _button = FindObjectsOfType<Button>();
        foreach (var button in _button)
        {
            Vector3 spawnPosition = button.transform.position - new Vector3(0, 0.5f, 0);
            button.onClick.AddListener(() => SpawnCoinOnPos(spawnPosition));
        }
    }

    private void InstantiateObjects()
    {
        _mousePosObject = Instantiate(new GameObject("MousePosObject"), Vector3.zero, Quaternion.identity);
        _mousePosObject.tag = "MousePosition";
    }

    public void SpawnCoinOnPos(Vector2 pos)
    {
        currentCoin =Instantiate(coin, pos, Quaternion.identity);
        
    }

    private void GetAndSetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = hit.point;
            Debug.Log("Mouse hit at: " + mousePos);
        }
    }

    private void GetTileOppucation(GameObject coin)
    {
        Vector2 gridpos = grid.GetTile(coin).Position;
        Debug.Log(gridpos);
    }
}
