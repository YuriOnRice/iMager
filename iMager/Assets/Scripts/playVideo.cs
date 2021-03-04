using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVideo : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.clip = this.GetComponent<videoImageInfo>().video;

        //videoPlayer.EnableAudioTrack(0, true);
        //if (!videoPlayer.isPlaying)
        //{
        //    Object.Destroy(Circle);
        //}
    }

    public void PlayMovie()
    {
        if (videoPlayer != null)
            videoPlayer.Play();
    }

    public void StopMovie()
    {
        videoPlayer.Pause();
    }

    public void ScanAndMovie()
    {
        PlayMovie();
    }

    /*

    public IEnumerator Scanning()
    {

        yield return new WaitForSecondsRealtime(.1f);
        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //PlayMovie();
        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    */


}
