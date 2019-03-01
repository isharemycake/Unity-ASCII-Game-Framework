﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    [Tooltip ("Add gird with GridCamera component")]
    public GridCamera GridCam;

    private Vector2 _gridOffset;
    private Vector2 _cellSize=new Vector2(1,1);
    private Vector2 _cellScale;
    private GameObject [,] _grid;

    public int CellSize
    {
        get { return (int)_cellSize.x; }
    }

    public void SetCellSize(int cellSize)
    {
        _cellSize = new Vector2(cellSize,cellSize);
    }

    public void UpdateView(int X, int Y)
    {
        GameObject cellObject = new GameObject();
        cellObject.name = "Cell";
        
        _grid = new GameObject[X,Y];

        _cellScale.x = 1 / _cellSize.x;
        _cellScale.y = 1 / _cellSize.y;

        cellObject.transform.localScale = new Vector2(_cellScale.x, _cellScale.y);

        _gridOffset.x = -(X/ 2) + _cellSize.x / 2;
        _gridOffset.y = (Y / 2) - _cellSize.y / 2;

        for (int row = 0; row < Y; row++)
        {
            for (int col = 0; col <X; col++)
            {
                Vector2 pos = new Vector2(col * _cellSize.x + _gridOffset.x, -row * _cellSize.y + _gridOffset.y);

                _grid[col, row] = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                _grid[col, row].transform.parent = gameObject.transform;
            }
        }

        Destroy(cellObject);

    }

    public void Write(int X, int Y, string symbol, Color color)
    {
        GameObject cellSymbol = new GameObject();
        cellSymbol.name = "Symbol";

        cellSymbol.AddComponent<TextMeshPro>().text = symbol;
        TextMeshPro tmp = cellSymbol.GetComponent<TextMeshPro>();

        tmp.rectTransform.sizeDelta= new Vector2(_cellSize.x,_cellSize.y*1.5f);

        cellSymbol.transform.SetParent(_grid[X, Y].transform);
        cellSymbol.transform.localPosition= Vector2.zero;

        tmp.enableAutoSizing = true;
        tmp.color = color;
        tmp.fontSizeMin = 0f;
        tmp.alignment = TextAlignmentOptions.Center;
    }
}