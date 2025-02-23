using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private void Start()
    {
        

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GridSystemVisual.Instance.HideAllGridPositions();
            
            GridSystemVisual.Instance.ShowGridPositionList(unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }

}
