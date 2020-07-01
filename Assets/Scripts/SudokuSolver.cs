using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SudokuSolver : MonoBehaviour
{
    [SerializeField]
    Transform sudokuGrid;
    InputField[,] gridSquares = new InputField[9,9];
    [SerializeField]
    public int[,] gridValues = new int[9, 9];

    void Start()
    {
        int index = 0;
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                gridSquares[i,j] = sudokuGrid.GetChild(index).GetComponent<InputField>();
                index++;
            }
        }
    }

    bool isPossible(int y, int x, int n)
    {
        for(int i = 0; i < 9; i++)
        {
            if(gridValues[y,i] == n)
            {
                return false;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (gridValues[i,x] == n)
            {
                return false;
            }
        }
        int x_ = x / 3 * 3;
        int y_ = y / 3 * 3;

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(gridValues[y_ + j, x_ + i] == n)
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool Solve()
    {
        int row, col;

        if(!FindUnassigned(out row, out col))
        {
            return true;
        }

        for (int num = 1; num < 10; num++)
        {
            if (isPossible(row, col, num))
            {
                gridValues[row, col] = num;

                if (Solve())
                {
                    return true;
                }
            }
        }
        gridValues[row, col] = 0;
        return false;
    }

    bool FindUnassigned(out int row, out int col)
    {
        for(row = 0; row < 9; row++)
        {
            for(col = 0; col < 9; col++)
            {
                if(gridValues[row,col] == 0)
                {
                    return true;
                }
            }
        }

        row = col = -1;
        return false;
    }

    public void OnSolveBtnClick()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (gridSquares[i, j].text != "")
                {
                    gridValues[i, j] = int.Parse(gridSquares[i, j].text);
                }
            }
        }
        Solve();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (gridSquares[i, j].text == "")
                {
                    gridSquares[i, j].textComponent.color = Color.gray;
                    gridSquares[i, j].text = gridValues[i, j].ToString();
                }
            }
        }
    }
}
