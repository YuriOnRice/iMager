using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVideo : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;

    public bool isPlaying = false;

    void Start()
    {
        videoPlayer.clip = this.GetComponent<AlbumContainer>().Album.video;

        //videoPlayer.EnableAudioTrack(0, true);
        //if (!videoPlayer.isPlaying)
        //{
        //    Object.Destroy(Circle);
        //}
    }

    public void Update()
    {
        if (isPlaying == true)
        {
            PlayMovie();
        }
        else if (isPlaying == false)
        {
            StopMovie();
        }
    }

    public void PlayMovie()
    {
        if (videoPlayer != null)
            videoPlayer.Play();
        //gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
    }

    public void StopMovie()
    {
        videoPlayer.Pause();
        //gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);

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
