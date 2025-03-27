
using MLib;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData_", menuName = "Skin Data")]
public class SOSkinData: ScriptableObject
{
    [SerializeField] private int idReference;
    [SerializeField] private int price = 5;
    [SerializeField, PreviewField] private Sprite sprPreview;

    public int IDReference => idReference;
    public Sprite SprPreview => sprPreview;
    public int Price => price;
    public bool IsUnlocked => DataManager.LocalData.SkinsUnlocked.Contains(idReference);
    public bool IsChoosing => DataManager.LocalData.SkinSelected == idReference;
}