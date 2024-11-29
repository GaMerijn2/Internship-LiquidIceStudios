using UnityEngine;

public class SoundPlayer
{
    private readonly SoundExtensions _soundExtensions;

    public SoundPlayer(SoundExtensions soundExtensions)
    {
        _soundExtensions = soundExtensions;
    }

    public void PlayPlacementSound()
    {
        _soundExtensions.PlaySoundFile(GameAssets.PlaceSound);
    }

    public void PlayErrorSound()
    {
        _soundExtensions.PlaySoundFile(GameAssets.ErrorSound, 0.4f);
    }
}