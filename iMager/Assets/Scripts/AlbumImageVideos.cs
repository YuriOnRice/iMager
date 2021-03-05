using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "G_N_A_00", menuName = "Card/Default")]
[System.Serializable]
public class AlbumImageVideos : ScriptableObject
{
    public string group;
    public string album;
    public string imageUrl;
    public UnityEngine.Video.VideoClip video;
}
