using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;
using System.Linq;
using TMPro;


public class ButtonScript : MonoBehaviour
{
    public GameObject AppManager;
    public bool addAlbum = false;
    public bool addGroup = false;
    public GameObject openWindowprefab;
    public GameObject arCamera;


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

    public void DestroyTrackables()
    {
        StartCoroutine((DestroyTrackablesInScene()));
    }

    public IEnumerator DestroyTrackablesInScene()
    {
        arCamera = GameObject.Find("ARCamera");
        arCamera.GetComponent<VuforiaBehaviour>().enabled = false;
        yield return new WaitForSecondsRealtime(0.2f);
        DisableAR();
        yield return new WaitForSecondsRealtime(0.2f);
        Scene[] scenes = SceneManager.GetAllScenes();
        string level = scenes[1].name;
        AppManager.GetComponent<AppManager>().searchAlbumListFiltered.Clear();
        AppManager.GetComponent<AppManager>().searchGroup = null;
        arCamera.GetComponent<CreateImageTargets>().filteredAlbumContainers.Clear();
        arCamera.GetComponent<CreateImageTargets>().streamList.Clear();
        OpenWindow(openWindowprefab);
        UnLoadSceneAdditive(level);
        //yield return new WaitForSecondsRealtime(4.5f);
    }

    public void FilterAlbums()
    {
        AppManager.GetComponent<AppManager>().FilterAlbums();
    }
    public void EnableAR(string SceneName)
    {
        AppManager.GetComponent<AppManager>().level = SceneName;
        //LoadSceneAdditive(SceneName);
        AppManager.GetComponent<AppManager>().LoadSceneAdditive();
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
        arCamera.GetComponent<CreateImageTargets>().DestroyDataSets();
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


}
