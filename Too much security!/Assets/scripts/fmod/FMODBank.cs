using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMODbanks : MonoBehaviour
{
    public static FMODbanks Instance { get; private set; }
    public EventReference jumpSFX;
    public EventReference dashSFX;
    public EventReference crouchSFX;
    public EventReference walkingSFX;
    public EventReference snapshotSFX;
    public EventReference damageSFX;
    // public EventReference platformFlyingSFX;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    public void PlayJumpSFX(GameObject OriginOfSound)
    {
        RuntimeManager.PlayOneShotAttached(jumpSFX, OriginOfSound);
    }
    public void PlayDashSFX(GameObject OriginOfSound)
    {
        RuntimeManager.PlayOneShotAttached(dashSFX, OriginOfSound);
    }

    private EventInstance crouchInstance;
    public void PlayCrouchEvent()
    {
        crouchInstance = RuntimeManager.CreateInstance(crouchSFX);
        crouchInstance.start();
    }
    public void StopCrouchEvent()
    {
        crouchInstance.release();
        crouchInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    

    public void PlayWalkSFX(GameObject OriginOfSound)
    {
        RuntimeManager.PlayOneShotAttached(walkingSFX, OriginOfSound);
    }
    //
    private EventInstance snapshotInstance;
    public void StartSnapshotSFX()
    {
        // Create the EventInstance for hover sound and start it
        snapshotInstance = RuntimeManager.CreateInstance(snapshotSFX);
        snapshotInstance.start();
    }
    public void StopSnapshotSFX()
    {
        // If the hover sound is playing, stop it, and release the instance
        if (snapshotInstance.isValid())
        {
            snapshotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            snapshotInstance.release();
        }
    }
    
    public void PlayDamageSFX(GameObject OriginOfSound)
    {
        RuntimeManager.PlayOneShotAttached(damageSFX, OriginOfSound);
    }
    //
    //
    //
    // public void PlayVictoryTheme()
    // {
    //     RuntimeManager.PlayOneShotAttached(Victory, gameObject);
    // }
    //
    // private EventInstance musicInstance;
    // public void PlayMusic()
    // {
    //     // Create the EventInstance for hover sound and start it
    //     musicInstance = RuntimeManager.CreateInstance(Music);
    //     musicInstance.start();
    // }
    //
    // public void SetParameterOfMusicToOne()
    // {
    //     // Set the parameter value
    //     musicInstance.setParameterByName("Music", 1);
    // }
    //
    // public void OnSceneSwitch()
    // {
    //     StopHoverSFX();
    //     hoverInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //     hoverInstance.release();
    //     musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //     musicInstance.release();
    // }
    //
    // public void OnDestroy()
    // {
    //     // Make sure to release the instance when it's no longer needed
    //     musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //     musicInstance.release();
    // }
    //
    // public void DeathSound()
    // {
    //     RuntimeManager.PlayOneShotAttached(jumpSFX, gameObject);
    // }
    //
    
}
