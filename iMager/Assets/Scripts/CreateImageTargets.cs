using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Vuforia;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class CreateImageTargets : MonoBehaviour
{
    public List<GameObject> imageTargets = new List<GameObject>();
    [Header("Albums")]
    public AlbumImageVideos currentAlbumContainer;
    public List<AlbumImageVideos> existingAlbumContainers;
    public List<AlbumImageVideos> filteredAlbumContainers;
    [Space(10)]
    [Header("FILESTREAM")]
    public bool cycleCreate = false; //go through list one by one
    public bool getCards = false; //transfer list to camera from manager
    public bool getCurrentStream;
    public List<string> currentStreams;
    public bool createTarget = false; //create an image target based on current stream index
    public string currentStream; //trackable name
    public List<string> streamList; //look at search stream list

    [Space(10)]
    [Header("OTHER")]
    public int index = 0;//used for adding next photocard script to each photocard object to teh photoinformation container
    public int indexAlbum = 0;
    public int numberInAlbum = 0;
    public int maxAmount;
    public GameObject Holder;
    public GameObject AppManager;
    public Slider LoadingBar;
    public GameObject LoadingBarText;
    public bool textingbool;
    public bool videoCamera = false;

    void Startsds()
    {
        AppManager = AppManager = GameObject.Find("AppManager");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ARScene"));
        filteredAlbumContainers = AppManager.GetComponent<AppManager>().searchAlbumListFiltered;
        currentAlbumContainer = AppManager.GetComponent<AppManager>().searchAlbumListFiltered[0];
        index = 0;
        currentStream = null;

        for (int i = 0; i < filteredAlbumContainers.Count; i++)
        {
            streamList.Add(filteredAlbumContainers[i].imageUrl);
        }
        streamList = new List<string>(streamList).Distinct().ToList();
        maxAmount = filteredAlbumContainers.Count();
        //foreach (AlbumImageVideos url in filteredAlbumContainers)
        //{
        //    streamList.Add(url.imageUrl);
        //}
    }

    public void StartMe()
    {
        AppManager = AppManager = GameObject.Find("AppManager");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ARScene"));
        filteredAlbumContainers = AppManager.GetComponent<AppManager>().searchAlbumListFiltered;
        currentAlbumContainer = filteredAlbumContainers[0];
        index = 0;
        currentStream = null;

        for (int i = 0; i < filteredAlbumContainers.Count; i++)
        {
            streamList.Add(filteredAlbumContainers[i].imageUrl);
        }
        streamList = new List<string>(streamList).Distinct().ToList();
        maxAmount = filteredAlbumContainers.Count();
        GetComponent<VuforiaBehaviour>().enabled = false;
        //VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        //VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
        AppManager = GameObject.Find("AppManager");

    }
    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
                CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }

    public void DestroyDataSets()
    {
        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        var dataset = objectTracker.GetDataSets();
        //Enumerable<Trackable> trackables = objectTracker.GetTrackables();
        objectTracker.DestroyAllDataSets(true);

        foreach (var item in imageTargets)
        {

        }

    }
    
    void Update()
    {
        if (cycleCreate == true)
        {
            getCurrentStream = true;
            //GetComponent<VuforiaBehaviour>().enabled = true;
            if (index >= filteredAlbumContainers.Count)
            {
                cycleCreate = false;
                getCurrentStream = false;
                VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
                VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);

                foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    
                }

                //GetComponent<VuforiaBehaviour>().enabled = true;
            }


        }
        if (getCurrentStream == true)
        {
            GetCurrentStream();
            //createTarget = true;
        }
        if (createTarget == true)
        {
            //Debug.Log(searchSpecificName);
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);
            //GetComponent<VuforiaBehaviour>().enabled = true;
            createTarget = false;
        }
    }

    public void GetCurrentStream()
    {
        currentStream = streamList[index];
        currentAlbumContainer = filteredAlbumContainers[index];
        createTarget = true;
        LoadingBar.GetComponent<Slider>().value = index;

        LoadingBarText.GetComponent<TextMeshProUGUI>().text = "Albums: " + index + " / " + maxAmount;
        // = true;
        //VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);

        index++;

        currentStreams.Add(currentStream);
        getCurrentStream = false;
        //VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);

        if (index >= filteredAlbumContainers.Count)
        {
            getCurrentStream = false;
            //createTarget = false;
            //index = 0;
            LoadingBar.gameObject.SetActive(false);
            GetComponent<VuforiaBehaviour>().enabled = true; 
            foreach (var item in imageTargets)
            {
                item.GetComponent<CustomTrackableBehaviour>().enabled = true;
                //foreach (var album in filteredAlbumContainers)
                //{
                //    if(item.name != album.name)
                //    {
                //        //Destroy(item);
                //        Debug.Log(item.name);
                //    }

                //}
            }
            StateManager sm = TrackerManager.Instance.GetStateManager();
            IEnumerable<TrackableBehaviour> activeTrackables = sm.GetTrackableBehaviours();

            Debug.Log(currentStream.ToString());
        }
    }


    void CreateImageTargetFromSideloadedTexture()
    {
        {
            var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

            // get the runtime image source and set the texture to load
            var runtimeImageSource = objectTracker.RuntimeImageSource;
            runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, currentStream, 1f, currentAlbumContainer.name);

            // create a new dataset and use the source to create a new trackable
            var dataset = objectTracker.CreateDataSet();
            var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, currentAlbumContainer.name);

            // add the DefaultTrackableEventHandler to the newly created game object
            trackableBehaviour.gameObject.AddComponent<CustomTrackableBehaviour>();
            trackableBehaviour.gameObject.AddComponent<TurnOffBehaviour>();
            trackableBehaviour.gameObject.AddComponent<AlbumContainer>();
            trackableBehaviour.gameObject.GetComponent<CustomTrackableBehaviour>().enabled = true;
            trackableBehaviour.gameObject.AddComponent<playVideo>();
            imageTargets.Add(trackableBehaviour.gameObject);

            for (int i = 0; i < filteredAlbumContainers.Count; i++)
            {
                if (currentAlbumContainer.name == filteredAlbumContainers[i].name)
                {
                    trackableBehaviour.gameObject.GetComponent<AlbumContainer>().Album = filteredAlbumContainers[i];

                }
            }
            //for (int i = 0; i < existingCardContainers.Count; i++)

            if (!existingAlbumContainers.Contains(trackableBehaviour.gameObject.GetComponent<AlbumContainer>().Album))
            {
                existingAlbumContainers.Add(trackableBehaviour.gameObject.GetComponent<AlbumContainer>().Album);
            }
            else if (existingAlbumContainers.Contains(trackableBehaviour.gameObject.GetComponent<AlbumContainer>().Album))
            {
                TrackerManager.Instance.GetStateManager().DestroyTrackableBehavioursForTrackable(trackableBehaviour.GetComponent<ImageTargetBehaviour>().Trackable, true);
            }


            // activate the dataset


            // TODO: add virtual content as child object(s)
            GameObject holder = Instantiate(Holder);
            holder.transform.SetParent(trackableBehaviour.gameObject.transform);
            holder.name = "HolderVideo";

            trackableBehaviour.GetComponent<playVideo>().videoPlayer = holder.transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>();
            objectTracker.ActivateDataSet(dataset);
        }
    }



    public void GetLoadingBar()
    {
        if (LoadingBar == null)
        {
            //getLoadingBar = true;
            //if (getLoadingBar == true && LoadingBar == null)
            {
                //if (LoadingBar == null)
                {
                    //LoadingBar = GameObject.FindGameObjectWithTag("PageTemplate").GetComponentInChildren<Slider>();
                    //LoadingBar = AppManager.GetComponent<AppManager>().loadingScreen.gameObject.GetComponent<LevelLoader>().ARCANVASspawn.gameObject.transform
                    //    .GetChild(AppManager.GetComponent<AppManager>().loadingScreen.gameObject.GetComponent<LevelLoader>().ARCANVASspawn.gameObject.transform.childCount - 1).gameObject.GetComponent<Slider>();


                    LoadingBar = AppManager.GetComponent<AppManager>().loadingScreen.gameObject.GetComponent<LevelLoader>().ARCANVASspawn.gameObject.transform
    .GetChild(AppManager.GetComponent<AppManager>().loadingScreen.gameObject.GetComponent<LevelLoader>().ARCANVASspawn.gameObject.transform.childCount - 1).gameObject.GetComponent<Slider>();
                    LoadingBarText = LoadingBar.gameObject.GetComponentInChildren<TextMeshProUGUI>().gameObject;


                    //LoadingBar = AppManager.GetComponent<AppManager>().loadingScreen.gameObject.GetComponent<LevelLoader>().slider;
                    LoadingBarText = LoadingBar.gameObject.GetComponentInChildren<TextMeshProUGUI>().gameObject;
                    LoadingBar.GetComponent<Slider>().minValue = 0;
                    LoadingBar.GetComponent<Slider>().maxValue = maxAmount;
                    //return maxAmount;
                    cycleCreate = true;
                    //GetComponent<VuforiaBehaviour>().enabled = true;

                    //getLoadingBar = false;
                }
            }
        }

    }
}


