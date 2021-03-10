using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;
using System.Linq;
using TMPro;
using NativeGalleryNamespace;


public class ButtonScript : MonoBehaviour
{
    public GameObject AppManager;


    public void Start()
    {
        AppManager = GameObject.Find("AppManager");
    }

    public void OpenScene(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void LoadSceneAdditive(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync
    }
    public void UnLoadSceneAdditive(string level)
    {
        Scene[] scenes = SceneManager.GetAllScenes();

        level = scenes[1].name;

        SceneManager.UnloadSceneAsync(level, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }

    public void FilterAlbums()
    {
        AppManager.GetComponent<AppManager>().FilterAlbums();
    }
    public void EnableAR(string SceneName)
    {
        AppManager.GetComponent<AppManager>().level = SceneName;
        LoadSceneAdditive(SceneName);
        //AppManager.GetComponent<AppManager>().LoadSceneAdditive();
        AppManager.GetComponent<AppManager>().loadingScreen.SetActive(true);


        //AppManager.GetComponent<AppManager>().level = ImageTargetName;
        //AppManager.GetComponent<AppManager>().LoadSceneAdditive(ImageTargetName);
        ////AppManager.GetComponent<AppManager>().loadingScreen.SetActive(true);

        ////AppManager.GetComponent<AppManager>().loadingScreen.
        ////    GetComponent<LevelLoader>().
        ////    LoadSceneAdditive(ImageTargetName);
        ////AppManager.GetComponent<AppManager>().LoadSceneAdditive();
        //AppManager.GetComponent<AppManager>().loadingScreen.SetActive(true);
    }

    public void DisableAR()
    {
        AppManager.GetComponent<AppManager>().ARBool = false;
        AppManager.GetComponent<AppManager>().level = "MainScene";
        //AppManager.GetComponent<AppManager>().loadingScreen.SetActive(true);
        //AppManager.GetComponent<AppManager>().destroyTrackables = true;
        //AppManager.GetComponent<AppManager>().imageTargetName = null;
    }

    public void OpenWindow(GameObject WindowName)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
        GameObject go = Instantiate(WindowName) as GameObject;
        go.name = WindowName.name;

        Destroy(transform.root.gameObject);
        //AppManager.GetComponent<AppManager>().ARBool = false;
        //Debug.Log(transform.parent.gameObject.name);
    }

    public void CloseWindow()
    {
        Destroy(transform.root.gameObject);
    }

    public void AlbumNameToManager()
    {
        AppManager.GetComponent<AppManager>().searchGroup = this.gameObject.name;
    }

    public void BecomeImage()
    {
        PickImage(512);
    }

    public void BecomeVideo()
    {
        PickVideo(512);
    }


    public void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds

                UnityEngine.UI.Image image = this.gameObject.GetComponent<UnityEngine.UI.Image>();

                this.gameObject.transform.parent.GetComponent<videoImageInfo>().image = image;

                this.gameObject.transform.parent.GetComponent<videoImageInfo>().imagePath = path;

                image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), 
                    new Vector2(0.5f, 0.5f), 100.0f);
            }
        }, "Select a PNG image", "image/png");

        Debug.Log("Permission result: " + permission);
    }

    public void PickVideo(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                Texture2D texture = NativeGallery.GetVideoThumbnail("file://" + path, maxSize);

                this.gameObject.transform.parent.GetComponent<videoImageInfo>().videoPath = path;

                //Handheld.PlayFullScreenMovie("file://" + path);

                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                // Play the selected video
                //Handheld.PlayFullScreenMovie("file://" + path);
                UnityEngine.UI.Image image = this.gameObject.GetComponent<UnityEngine.UI.Image>();

                UnityEngine.Video.VideoClip video = this.gameObject.GetComponent<UnityEngine.Video.VideoClip>();

                UnityEngine.Video.VideoPlayer videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();

                

                this.gameObject.transform.parent.GetComponent<videoImageInfo>().video = video;
               

                image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), 
                    new Vector2(0.5f, 0.5f), 100.0f);


                //videoPlayer.time = 0;
                //Plays the video for one frame
                //videoPlayer.Play();
                //Sets the frame to display on the RawImage
                //image.sprite = videoPlayer.texture;
                //Pauses the video after one frame so that the first frame
                //of the video is displayed during idle
                //videoPlayer.Pause();



            }
        }, "Select a video");


        Debug.Log("Permission result: " + permission);
    }

    // Example code doesn't use this function but it is here for reference
    public void PickImageOrVideo()
    {
        if (NativeGallery.CanSelectMultipleMediaTypesFromGallery())
        {
            NativeGallery.Permission permission = NativeGallery.GetMixedMediaFromGallery((path) =>
            {
                Debug.Log("Media path: " + path);
                if (path != null)
                {
                    // Determine if user has picked an image, video or neither of these
                    switch (NativeGallery.GetMediaTypeOfFile(path))
                    {
                        case NativeGallery.MediaType.Image: Debug.Log("Picked image"); break;
                        case NativeGallery.MediaType.Video: Debug.Log("Picked video"); break;
                        default: Debug.Log("Probably picked something else"); break;
                    }
                }
            }, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video, "Select an image or video");

            Debug.Log("Permission result: " + permission);
        }
    }
}
