using MLib;
using UnityEngine;

public class SetupTilingMaterial: MonoBehaviour
{
    [SerializeField] private Vector2 tiling = Vector2.one;
    [SerializeField] private int slot = 0;
    [SerializeField] private string nameProperty = "_BaseMap";
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        UpdateTiling();
    }

    private void Reset()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    [MButton]
    private void UpdateTiling()
    {
        meshRenderer.materials[slot].mainTextureScale = tiling;
    }
}