using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer MeshRenderer;

    public void Show()
    {
        MeshRenderer.enabled = true;
    }

    public void Hide()
    {
        MeshRenderer.enabled = false;
    }
}
