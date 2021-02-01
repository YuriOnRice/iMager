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
    public void EnableAR()
    {
        AppManager.GetComponent<AppManager>().LoadSceneAdditive();
        AppManager.GetComponent<AppManager>().loadingScreen.SetActive(true);
    }

    public void DisableAR()
    {
        AppManager.GetComponent<AppManager>().ARBool = false;
        //AppManager.GetComponent<AppManager>().destroyTrackables = true;
        //AppManager.GetComponent<AppManager>().imageTargetName = null;
    }

    public void BecomeImage()
    {
        PickImage(512);
    }

    public void BecomeVideo()
    {
        PickVideo();
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

                image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
        }, "Select a PNG image", "image/png");

        Debug.Log("Permission result: " + permission);
    }

    public void PickVideo()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                // Play the selected video
                Handheld.PlayFullScreenMovie("file://" + path);
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
