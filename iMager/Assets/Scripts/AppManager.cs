using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vuforia;
using UnityEditor;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using NativeGalleryNamespace;


[ExecuteAlways]
public class AppManager : MonoBehaviour
{
    public GameObject mainCamera;
    //public GameObject MAINMENU;
    //public GameObject arCamera;
    public bool ARBool = false;
    public GameObject loadingScreen;
    //public GameObject gallery;
    public string level;


    public bool filterAlbumsBool = false;
    public string searchGroup; //filter by this  group name
    public List<AlbumImageVideos> albumImageVideoList; //entire album list
    public List<AlbumImageVideos> searchAlbumListFiltered; //filtered album list

    //public GameObject VideoImage;
    //public List<videoImageInfo> videoImageInfoList;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public void Update()
    {
        if (ARBool == true)
        {
            mainCamera.SetActive(false);
            //MAINMENU.SetActive(false);
            //arCamera.SetActive(true);
        }
        else if (ARBool == false)
        {
            mainCamera.SetActive(true);
            //MAINMENU.SetActive(true);
            //arCamera.SetActive(false);
        }
        if (filterAlbumsBool == true)
        {
            FilterAlbums();
        }
    }



    public void Start()
    {
        DisableAR();
        GetComponent<AppManager>().enabled = true;
    }

    public void FilterAlbums()
    {
        foreach (var item in albumImageVideoList)
        {
            if (item.group.ToUpper() == (searchGroup.ToUpper()) && item.video != null)
            {
              searchAlbumListFiltered.Add(item);
                searchAlbumListFiltered = new List<AlbumImageVideos>(searchAlbumListFiltered).Distinct().ToList();

            }
            else if (item.group.ToUpper() != (searchGroup.ToUpper()))
            {
                searchAlbumListFiltered.Remove(item);
            }

        }
    }

    //public void GalleryOpen()
    //{
    //    PickImage(512);
    //}



    public void DisableAR()
    {
        if (mainCamera.GetComponent<VuforiaBehaviour>())
        {
            mainCamera.GetComponent<VuforiaBehaviour>().enabled = false;

            if (mainCamera.transform.childCount > 0)
            {
                mainCamera.transform.GetChild(0).GetComponent<BackgroundPlaneBehaviour>().enabled = false;
            }
            mainCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = false;
            mainCamera.GetComponent<VideoBackgroundBehaviour>().enabled = false;
        }
        //mainCamera.GetComponent<VuforiaBehaviour>().enabled = false;
    }

    public void EnableAR()
    {
        if (mainCamera.GetComponent<VuforiaBehaviour>())
        {
            mainCamera.GetComponent<VuforiaBehaviour>().enabled = true;
            mainCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = true;
            //scannedPhotoCards = (arImage.GetComponent<scanAndCollect>().scannedPhotoCards);
            //addedPhotoCards = (arImage.GetComponent<scanAndCollect>().addedPhotoCards);
            if (mainCamera.transform.childCount > 0)
            {
                mainCamera.transform.GetChild(0).GetComponent<BackgroundPlaneBehaviour>().enabled = true;
            }
            mainCamera.GetComponent<VideoBackgroundBehaviour>().enabled = true;
        }
        ARBool = true;
    }


    public void LoadSceneAdditive()
    {
        loadingScreen.GetComponent<LevelLoader>().LoadSceneAdditive(level);
        Debug.Log("rreee");
    }
    public void LoadScene()
    {
        loadingScreen.GetComponent<LevelLoader>().LoadScene(level);
    }
    public void UnLoadSceneAdditive()
    {
        //var prefab = Instantiate(loadingScreen.GetComponent<levelLoader>().CHOOSEGROUPALBUMSMENU);
        //loadingScreen.GetComponent<levelLoader>().ARCANVASspawn = prefab;
        loadingScreen.GetComponent<LevelLoader>().LoadSceneAdditive("MainScene");
    }
    public void UnLoadScene()
    {
        loadingScreen.GetComponent<LevelLoader>().LoadScene("MainScene");
    }
}
