using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimeLineScript : MonoBehaviour {

    public List <PlayableDirector> playableDirectors;
    public List<TimelineAsset> timeLines;

    public void Play()
    {
        foreach (PlayableDirector playableDirector in playableDirectors)
        {
            playableDirector.Play();
        }   
    }

    public void PlayFromTimeLines(int index)
    {
        Debug.Log("HERE");
        TimelineAsset selectedAsset;
        if(timeLines.Count <= index)
        {
            selectedAsset = timeLines[timeLines.Count - 1];
        }
        else
        {
            
            selectedAsset = timeLines[index];
        }

 playableDirectors[0].Play(selectedAsset); //play the clip
    }


  

    public bool ChangeBool(bool x)
    {
        bool timelineActive = false;

        if (timelineActive == true)
        {
            timelineActive = false;
        }
        else
        {
            Debug.Log("You shouldn't be here");
            timelineActive = false;
        }

        return timelineActive;
    }

}
