using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float stoppingDistance = .1f;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    
    private Animator unitAnimator;
    protected override void Awake()
    {
        base.Awake();
        unitAnimator = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if(!isActive) return;
        
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
           transform.position += moveDirection * (moveSpeed * Time.deltaTime);

            unitAnimator.SetBool("IsWalking", true);
        } else
        {
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
            onActionComplete();

        }
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
    
    public override string GetActionName()
    {
        return "Move";
    }

    public void Move(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // Same Grid Position where the unit is already at
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid Position already occupied with another Unit
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

}
