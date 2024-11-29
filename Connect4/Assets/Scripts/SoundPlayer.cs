using UnityEngine;

public class SoundPlayer
{
    private readonly PlaySound _playSound;

    public SoundPlayer(PlaySound playSound)
    {
        _playSound = playSound;
    }

    public void PlayPlacementSound()
    {
        _playSound.PlaySoundFile(GameAssets.PlaceSound);
    }

    public void PlayErrorSound()
    {
        _playSound.PlaySoundFile(GameAssets.ErrorSound, 0.4f);
    }
}