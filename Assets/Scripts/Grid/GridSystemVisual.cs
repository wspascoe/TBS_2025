using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform gridVisualPrefab;

    private GridSystemVisualSingle[,] gridSystemVisualArray;
    
    public static GridSystemVisual Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one GridSystemVisual! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        gridSystemVisualArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
          for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
          {
              GridPosition gridPosition = new GridPosition(x, z);
              Transform gridSystemVisualTransform = 
                  Instantiate(gridVisualPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
              
              gridSystemVisualArray[x, z] = gridSystemVisualTransform.GetComponent<GridSystemVisualSingle>();
          }
        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }
      public void HideAllGridPositions()
      {
          for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
          {
              for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
              {
                 gridSystemVisualArray[x, z].Hide();
              }
          }
      }

      public void ShowGridPositionList(List<GridPosition> gridPositions)
      {
          foreach (var gridPosition in gridPositions)
          {
              gridSystemVisualArray[gridPosition.x, gridPosition.z].Show();
          }
      }

      private void UpdateGridVisual()
      {
          Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
          HideAllGridPositions();
          ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
      }
}
