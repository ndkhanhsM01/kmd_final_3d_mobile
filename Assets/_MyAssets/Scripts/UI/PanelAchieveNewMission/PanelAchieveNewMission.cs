using UnityEngine;
using MLib;
using TMPro;

public class PanelAchieveNewMission : MPanel
{
    [SerializeField] private TMP_Text tmpTitle;
    [SerializeField] private TMP_Text tmpDescription;
    [SerializeField] private TMP_Text tmpProgress;

    [SerializeField] private Animation animationShow;

    private void OnEnable()
    {
        SOMission.Register_Success(OnNewMissionSuccess);
    }
    private void OnDisable()
    {
        SOMission.Unregister_Success(OnNewMissionSuccess);
    }

    private void OnNewMissionSuccess(SOMission mission)
    {
        tmpTitle.text = mission.GetTitle();
        tmpDescription.text = mission.GetDescription();
        tmpProgress.text = $"{mission.Current.ToString("00")}/{mission.Require.ToString("00")}";

        Show();
        animationShow.Stop();
        animationShow.Play();
    }
}
