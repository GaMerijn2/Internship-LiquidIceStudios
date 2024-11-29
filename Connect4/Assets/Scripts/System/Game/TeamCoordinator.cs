using UnityEngine;

public class TeamCoordinator
{
    public GameObject _currentCoinPrefab;
    public int CurrentTeam { get; private set; }

    public void StartWithTeam(int startingTeam)
    {
        CurrentTeam = startingTeam;
        UpdateTeam();
    }

    public GameObject GetActiveCoinPrefab() => _currentCoinPrefab;

    public void SwitchTeam()
    {
        CurrentTeam = CurrentTeam == 1 ? 2 : 1;
        UpdateTeam();
    }

    private void UpdateTeam()
    {
        _currentCoinPrefab = CurrentTeam == 1 
            ? GameAssets.RedCoinPrefab 
            : GameAssets.YellowCoinPrefab;

        if (_currentCoinPrefab == null) return;
        TeamInfo teamInfo = _currentCoinPrefab.GetComponent<TeamInfo>();
        teamInfo.teamID = CurrentTeam;
        teamInfo.teamName = CurrentTeam == 1 ? "Peach" : "Teal";
    }
}