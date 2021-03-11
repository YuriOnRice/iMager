using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Vuforia;
using System.Text.RegularExpressions;


public class EditorScript 
{
    [MenuItem("Tools/All/AssignPaths")]
    public static void AssignPaths()
    {
        AlbumImageVideos[] scripts = (AlbumImageVideos[])Resources.FindObjectsOfTypeAll(typeof(AlbumImageVideos));

        foreach (AlbumImageVideos p in scripts)
        {
            // p.imageUrl = "Assets/StreamingAssets/Vuforia/" + p.group.ToUpper().Replace(" ", "")  +"/album_"
            // + p.group.ToLower().Replace(" ", "") + "_"+p.album.Replace(" ", "").Replace(":", "") + ".jpg";


            p.imageUrl = "Vuforia/" 
                + p.group.ToUpper().Replace(" ", "") + "/"
            + p.name
            + ".jpg";
        }
    }
}
