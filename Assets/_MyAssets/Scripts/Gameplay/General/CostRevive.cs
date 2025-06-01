using UnityEngine;

[System.Serializable]
public class CostRevive
{
    [SerializeField] private int begin = 2;
    [SerializeField] private int multiplePerStep = 2;
    [SerializeField] private SOIntVariable shareCoinRevive;
    public int Value => shareCoinRevive.Value;
    public void Init()
    {
        shareCoinRevive.Value = begin;
    }
    public void Increase()
    {
        shareCoinRevive.Value *= multiplePerStep;
    }
}