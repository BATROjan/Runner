using UnityEngine;

public interface IAudioController
{
    void Play(AudioType nameSound, bool isLoop = false);
    void StopAll();
}
