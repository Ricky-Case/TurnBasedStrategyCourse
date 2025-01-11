using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;


    private void Awake()
    {
        meshRenderer.enabled = false;
    }
    
    //**************************//
    //**** HELPER FUNCTIONS ****//
    //**************************//
    
    public void Show() { meshRenderer.enabled = true; }
    public void Hide() { meshRenderer.enabled = false; }
}
