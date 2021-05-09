using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class RhythmHeckinWwiseSync : MonoBehaviour
{

    AK.Wwise.Event rhythmHeckinEvent;

    [HideInInspector] public static float secondsPerBeat = 0.5f;
    [HideInInspector] public static float BPM = 120;

    //public UnityEvent OnB;
    //public UnityEvent OnJump;

    public UnityEvent OnLevelEnded;

    public UnityEvent OnEveryGrid;
    public UnityEvent OnEveryBeat;
    public UnityEvent OnEveryBar;
    public UnityEvent OnSongStart;
    public static Action<int, bool, int> TogglePerson;

   public PersonToggle gameSceneManager;

    public AK.Wwise.Event overrideEvent;
    [SerializeField] TutorialHandler th;

    //id of the wwise event - using this to get the playback position
    static uint playingID;

    private void Awake()
    {
        SetSong(GameManager.trackNum);
        gameSceneManager = FindObjectOfType<PersonToggle>();
    }

    void Start()
    {
        if (th == null) // very silly way of being like, if we're not in the tutorial scene, load the song immediately
        {
            StartCoroutine(LoadAndStartSong());
        }
    }

    public IEnumerator LoadAndStartSong(float waitingTime = 5f)
    {
        yield return new WaitForSeconds(waitingTime);
        playingID = rhythmHeckinEvent.Post(gameObject, (uint)(AkCallbackType.AK_MusicSyncAll | AkCallbackType.AK_EnableGetMusicPlayPosition), MusicCallbackFunction);
    }

    public void SetSong(int num)
    {
   /*     if (overrideEvent == null)
        {*/
            rhythmHeckinEvent = GameManager.tracksRef[num];
/*        }
        else
        {
            rhythmHeckinEvent = overrideEvent;
            GameManager.trackNum = 1;
        }*/
    }

    public void StopSong()
    {
        if (rhythmHeckinEvent == null) return;
        rhythmHeckinEvent.Stop(gameObject);
    }

    void MusicCallbackFunction(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {

        AkMusicSyncCallbackInfo _musicInfo = (AkMusicSyncCallbackInfo)in_info;

        switch (_musicInfo.musicSyncType)
        {
            case AkCallbackType.AK_MusicSyncUserCue:

                CustomCues(_musicInfo.userCueName, _musicInfo);

                secondsPerBeat = _musicInfo.segmentInfo_fBeatDuration;
                BPM = _musicInfo.segmentInfo_fBeatDuration * 60f;

                break;
            case AkCallbackType.AK_MusicSyncBeat:


                OnEveryBeat.Invoke();
                break;
            case AkCallbackType.AK_MusicSyncBar:
                //I want to make sure that the secondsPerBeat is defined on our first measure.
                secondsPerBeat = _musicInfo.segmentInfo_fBeatDuration;
                //Debug.Log("Seconds Per Beat: " + secondsPerBeat);

                OnEveryBar.Invoke();
                break;

            case AkCallbackType.AK_MusicSyncGrid:
                OnEveryGrid.Invoke();
                break;
            default:
                break;

        }

    }




    public void CustomCues(string cueName, AkMusicSyncCallbackInfo _musicInfo)
    {
        switch (cueName)
        {
            case "LevelEnded":
                OnLevelEnded.Invoke();
                break;
            case "StartSong":
                OnSongStart.Invoke();
                break;
            case "0p":
                TogglePerson.Invoke(0, true, 0);
                break;
            case "1p":
                TogglePerson.Invoke(1, true, 0);
                break;
            case "2p":
                TogglePerson.Invoke(2, true, 0);
                break;
            case "3p":
                TogglePerson.Invoke(3, true, 0);
                break;
            case "4p":
                TogglePerson.Invoke(4, true, 0);
                break;
            case "5p":
                TogglePerson.Invoke(5, true, 0);
                break;
            case "6p":
                TogglePerson.Invoke(6, true, 0);
                break;
            case "7p":
                TogglePerson.Invoke(7, true, 0);
                break;
            case "8p":
                TogglePerson.Invoke(8, true, 0);
                break;
            case "9p":
                TogglePerson.Invoke(9, true, 0);
                break;
            case "10p":
                TogglePerson.Invoke(10, true, 0);
                break;
            case "11p":
                TogglePerson.Invoke(11, true, 0);
                break;
            case "12p":
                TogglePerson.Invoke(12, true, 0);
                break;
            case "13p":
                TogglePerson.Invoke(13, true, 0);
                break;
            case "14p":
                TogglePerson.Invoke(14, true, 0);
                break;
            case "15p":
                TogglePerson.Invoke(15, true, 0);
                break;
            case "0d":
                TogglePerson.Invoke(0, true, 1);
                break;
            case "1d":
                TogglePerson.Invoke(1, true, 1);
                break;
            case "2d":
                TogglePerson.Invoke(2, true, 1);
                break;
            case "3d":
                TogglePerson.Invoke(3, true, 1);
                break;
            case "4d":
                TogglePerson.Invoke(4, true, 2);
                break;
            case "5d":
                TogglePerson.Invoke(5, true, 2);
                break;
            case "6d":
                TogglePerson.Invoke(6, true, 2);
                break;
            case "7d":
                TogglePerson.Invoke(7, true, 2);
                break;
            case "8d":
                TogglePerson.Invoke(8, true, 3);
                break;
            case "9d":
                TogglePerson.Invoke(9, true, 3);
                break;
            case "10d":
                TogglePerson.Invoke(10, true, 3);
                break;
            case "11d":
                TogglePerson.Invoke(11, true, 3);
                break;
            case "12d":
                TogglePerson.Invoke(12, true, 4);
                break;
            case "13d":
                TogglePerson.Invoke(13, true, 4);
                break;
            case "14d":
                TogglePerson.Invoke(14, true, 4);
                break;
            case "15d":
                TogglePerson.Invoke(15, true, 4);
                break;
            default:
                break;

        }
    }

    //this is pretty straightforward - get the elapsed time
    public static int GetMusicTimeInMS()
    {

        AkSegmentInfo segmentInfo = new AkSegmentInfo();

        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);

        return segmentInfo.iCurrentPosition;
    }

    //We're going to call this when we spawn a gem, in order to determine when it's crossing time should be
    //based on the current playback position, our beat duration, and our beat offset
    public int SetCrossingTimeInMS(int beatOffset)
    {
        AkSegmentInfo segmentInfo = new AkSegmentInfo();

        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);

        int offsetTime = Mathf.RoundToInt(1000 * secondsPerBeat * beatOffset);

        Debug.Log("setting time: " + segmentInfo.iCurrentPosition + offsetTime);

        return segmentInfo.iCurrentPosition + offsetTime;
    }


}
