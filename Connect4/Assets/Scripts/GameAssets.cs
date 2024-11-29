using UnityEngine;

public static class GameAssets
{
    public static GameObject RedCoinPrefab { get; set; }
    public static GameObject YellowCoinPrefab { get; set; }
    public static AudioClip PlaceSound { get; set; }
    public static AudioClip ErrorSound { get; set; }

    public static void Initialize(GameObject redCoin, GameObject yellowCoin, AudioClip place, AudioClip error)
    {
        RedCoinPrefab = redCoin;
        YellowCoinPrefab = yellowCoin;
        PlaceSound = place;
        ErrorSound = error;
    }
}