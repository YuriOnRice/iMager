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
    public List<AlbumImageVideos> filteredAlbumContainers;
    [Space(10)]
    [Header("FILESTREAM")]
    public string currentStream; //trackable name
    public List<string> streamList; //look at search card list

    [Space(10)]
    [Header("OTHER")]
    public bool createTarget = false;
    public bool getCards = false;
    public bool cycleCreate = false;
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


    public void Update()
    {
        if (cycleCreate == true)// && LoadingBar != null)
        {
            CycleCreate();
            //LoadingBar = GameObject.FindGameObjectWithTag("PageTemplate").GetComponentInChildren<Slider>();
        }
        if (cycleCreate == false)
        {
            index = 0;
            //searchSpecific = searchCardList[index].name;


            if (index > filteredAlbumContainers.Count)//if (index > searchCardList.Count)
            {
                index = filteredAlbumContainers.Count - 1;//index = searchCardList.Count - 1;
            }
            //int delete = searchSpecific.LastIndexOf(" ");
            //if (delete > 0)
            //{
            //    searchSpecific = searchSpecific.Substring(0, delete);
            //}
        }

        if (createTarget == true)
        {
            //Debug.Log(searchSpecificName);
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);
            createTarget = false;
        }

        if (getCards == true)
        {
            //AssignCardsToEmptyPhotos();
            CreateImageTargetsGo();
        }
    }

    public void CycleCreate()
    {
        if (index != filteredAlbumContainers.Count)//if (index != searchCardList.Count)
        {
            CreateImageTargetsGo();
            createTarget = true;
            LoadingBar.GetComponent<Slider>().value = index;

            LoadingBarText.GetComponent<TextMeshProUGUI>().text = "Albums: " + index + " / " + maxAmount;

            //LoadingBar.GetComponent<Slider>().value = index;
            //loadingBar.GetComponent<Slider>().minValue = 0;
            //loadingBar.GetComponent<Slider>().maxValue = 0;
        }

        //if (index == 1)
        //{
        //    LoadingBar = GameObject.FindGameObjectWithTag("PageTemplate").GetComponentInChildren<Slider>();
        //}
        else if (index >= filteredAlbumContainers.Count)//else if (index >= searchCardList.Count)
        {
            cycleCreate = false;
            index = 0;
            imageTargets = GameObject.FindGameObjectsWithTag("ImageTarget").ToList();
            LoadingBar.gameObject.SetActive(false); ///////////////////////////////////////////////////////////////////////////
            //filteredImageTargets = new List<GameObject>(imageTargets);
            imageTargets = new List<GameObject>(imageTargets).Distinct().ToList();
            GetComponent<VuforiaBehaviour>().enabled = true;

            foreach (var item in imageTargets)
            {
                item.GetComponent<CustomTrackableBehaviour>().enabled = true;
            }

            //VuforiaBehaviour.Instance.enabled = true;

            StateManager sm = TrackerManager.Instance.GetStateManager();
            IEnumerable<TrackableBehaviour> activeTrackables = sm.GetTrackableBehaviours();



            //Tracker imageTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            //imageTracker.Start();

            Debug.Log("List of trackables currently active (tracked): ");
            foreach (TrackableBehaviour tb in activeTrackables)
            {
                Debug.Log("Trackable: " + tb.TrackableName);

                //if (tb.)

            }
            return;
        }
    }
    public void CreateImageTargetsGo()
    {
        getCards = true;
        if (getCards == true)
        {
            currentStream = filteredAlbumContainers[index].imageUrl;
            //searchSpecificName = "Vuforia/DREAMCATCHER/ALBUMS/album_dreamcatcher_nightmare_01.jpg";
            //searchSpecificName = "Vuforia/DREAMCATCHER/DAMI/dreamcatcher_dami_nightmare_01.jpg";
            streamList.Add(currentStream);

            //searchSpecific = searchCardList[index].ToString();
            currentStream = filteredAlbumContainers[index].ToString();


            int delete = currentStream.LastIndexOf(" ");
            if (delete > 0)
            {
                currentStream = currentStream.Substring(0, delete);//.Substring(searchSpecific.IndexOf("/")).Replace("/", ""); ;
            }

            index++;

        }
        getCards = false;
    }
    public void InitStart()
    {
        StartMe();
    }

    void StartMe()
    {

        index = 0;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ARScene"));

        GetComponent<VuforiaBehaviour>().enabled = false;

        AppManager = GameObject.Find("AppManager");
        filteredAlbumContainers =
    AppManager.GetComponent<AppManager>().searchAlbumListFiltered;

        Debug.Log("poggers");

        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
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
        objectTracker.DestroyAllDataSets(false);

        foreach (var item in imageTargets)
        {

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
                    LoadingBar = AppManager.GetComponent<AppManager>().loadingScreen.gameObject.GetComponent<LevelLoader>().ARCANVASspawn.gameObject.transform
                        .GetChild(AppManager.GetComponent<AppManager>().loadingScreen.gameObject.GetComponent<LevelLoader>().ARCANVASspawn.gameObject.transform.childCount - 1).gameObject.GetComponent<Slider>();
                    LoadingBarText = LoadingBar.gameObject.GetComponentInChildren<TextMeshProUGUI>().gameObject;
                    LoadingBar.GetComponent<Slider>().minValue = 0;
                    LoadingBar.GetComponent<Slider>().maxValue = maxAmount;
                    //return maxAmount;
                    cycleCreate = true;
                    //getLoadingBar = false;
                }
            }
        }

    }


    void CreateImageTargetFromSideloadedTexture()
    {
        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        // get the runtime image source and set the texture to load
        var runtimeImageSource = objectTracker.RuntimeImageSource;
        runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, currentStream, 0.15f, currentAlbumContainer.name);

        // create a new dataset and use the source to create a new trackable
        var dataset = objectTracker.CreateDataSet();
        var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, currentStream);

        // add the DefaultTrackableEventHandler to the newly created game object
        trackableBehaviour.gameObject.AddComponent<CustomTrackableBehaviour>();
        trackableBehaviour.gameObject.AddComponent<TurnOffBehaviour>();
        trackableBehaviour.gameObject.AddComponent<videoImageInfo>();
        trackableBehaviour.gameObject.GetComponent<CustomTrackableBehaviour>().enabled = false;
        trackableBehaviour.gameObject.AddComponent<playVideo>();
        trackableBehaviour.gameObject.tag = "ImageTarget";

        // activate the dataset
        GameObject holder = Instantiate(Holder);
        holder.transform.SetParent(trackableBehaviour.gameObject.transform);
        holder.name = "HolderVideo";

        trackableBehaviour.GetComponent<playVideo>().videoPlayer = holder.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>();

        holder.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>().clip = trackableBehaviour.gameObject.GetComponent<videoImageInfo>().video;

        //videoPlayer.clip = this.GetComponent<albumContainer>().Album.albumMusicVideo;
        //holder.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>().audioOutputMode 
        objectTracker.ActivateDataSet(dataset);

        // TODO: add virtual content as child object(s)


        /*

         void CreateImageTargetFromSideloadedTexture()
        {
            //if (AppManager.GetComponent<AppManager>().ARBool == true)
            {
                var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

                // get the runtime image source and set the texture to load
                var runtimeImageSource = objectTracker.RuntimeImageSource;
                runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, searchSpecificName, 1f, searchSpecific);

                // create a new dataset and use the source to create a new trackable
                var dataset = objectTracker.CreateDataSet();
                var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, searchSpecific);

                // add the DefaultTrackableEventHandler to the newly created game object
                trackableBehaviour.gameObject.AddComponent<CustomTrackableBehaviour>();
                trackableBehaviour.gameObject.AddComponent<TurnOffBehaviour>();
                trackableBehaviour.gameObject.AddComponent<videoImageInfo>();
                trackableBehaviour.gameObject.GetComponent<CustomTrackableBehaviour>().enabled = false;
                trackableBehaviour.gameObject.AddComponent<playVideo>();
                trackableBehaviour.gameObject.tag = "ImageTarget";

                for (int i = 0; i < albumContainers.Count; i++)
                {
                    if (searchSpecific == albumContainers[i].name)
                    {
                        searchAlbumContainer = albumContainers[i];
                        trackableBehaviour.gameObject.GetComponent<AlbumImageVideos>().Album = albumContainers[i];

                    }
                    //else if (searchSpecific != cardContainers[i].name)
                    //{
                    //    Destroy(trackableBehaviour.gameObject);
                    //}
                }
                //for (int i = 0; i < existingCardContainers.Count; i++)

                if (!existingAlbumContainers.Contains(trackableBehaviour.gameObject.GetComponent<AlbumImageVideos>().Album))
                {
                    existingAlbumContainers.Add(trackableBehaviour.gameObject.GetComponent<AlbumImageVideos>().Album);
                }
                else if (existingAlbumContainers.Contains(trackableBehaviour.gameObject.GetComponent<AlbumImageVideos>().Album))
                {
                    TrackerManager.Instance.GetStateManager().DestroyTrackableBehavioursForTrackable(trackableBehaviour.GetComponent<ImageTargetBehaviour>().Trackable, true);
                }


                // activate the dataset


                // TODO: add virtual content as child object(s)
                GameObject holder = Instantiate(Holder);
                holder.transform.SetParent(trackableBehaviour.gameObject.transform);
                holder.name = "HolderVideo";

                trackableBehaviour.GetComponent<playVideo>().videoPlayer = holder.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>();

                holder.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>().clip = trackableBehaviour.gameObject.GetComponent<albumContainer>().Album.albumMusicVideo;

                //videoPlayer.clip = this.GetComponent<albumContainer>().Album.albumMusicVideo;
                //holder.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>().audioOutputMode 
                objectTracker.ActivateDataSet(dataset);

                //Debug.Log(objectTracker.GetActiveDataSets());

                //Debug.Log("List of trackables currently active (tracked): ");
                //foreach (TrackableBehaviour tb in activeTrackables)
                //{
                //    Debug.Log("Trackable: " + tb.TrackableName);

                //    //if (tb.)

                //}



                //if (AppManager.GetComponent<AppManager>().ARBool == false)
                //{
                //    TrackerManager.Instance.GetStateManager().DestroyTrackableBehavioursForTrackable(trackableBehaviour.GetComponent<ImageTargetBehaviour>().Trackable, true);
                //}
            }
        
    }
    */

        /*

        IEnumerator CreateImageTargetFromDownloadedTexture()
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(URLS[index]))
            {

                yield return uwr.SendWebRequest();

                if (uwr.isDone)
                {
                    if (uwr.isNetworkError || uwr.isHttpError)
                    {
                        Debug.Log(uwr.error);
                    }
                    else
                    {

                        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

                        var texture = DownloadHandlerTexture.GetContent(uwr);

                        // get the runtime image source and set the texture to load
                        var runtimeImageSource = objectTracker.RuntimeImageSource;
                        runtimeImageSource.SetImage(texture, 1f, searchSpecific);
                        //runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, searchSpecificName, 1f, searchSpecific);

                        // create a new dataset and use the source to create a new trackable
                        var dataset = objectTracker.CreateDataSet();
                        var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, searchSpecific);
                        //trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
                        imageTargets.Add(trackableBehaviour.gameObject);

                        // /*

                        // add the DefaultTrackableEventHandler to the newly created game object
                        trackableBehaviour.gameObject.AddComponent<CustomTrackableBehaviour>();
                        trackableBehaviour.gameObject.AddComponent<TurnOffBehaviour>();
                        trackableBehaviour.gameObject.AddComponent<imageTargetPhotoCheck>();
                        trackableBehaviour.gameObject.AddComponent<photoCardContainer>();
                        trackableBehaviour.gameObject.GetComponent<CustomTrackableBehaviour>().enabled = false;
                        //AppManager.GetComponent<AppManager>().loadingScreen.GetComponent<levelLoader>().ARCANVASspawn.GetComponent<scanAndCollect>().index = index;
                        //AppManager.GetComponent<AppManager>().loadingScreen.GetComponent<levelLoader>().ARCANVASspawn.GetComponent<scanAndCollect>().SpawnScannedCards();


                        for (int i = 0; i < cardContainers.Count; i++)
                        {
                            if (searchSpecific == cardContainers[i].name)
                            {
                                searchCard = cardContainers[i];
                                trackableBehaviour.gameObject.GetComponent<photoCardContainer>().Card = cardContainers[i];

                            }
                            //else if (searchSpecific != cardContainers[i].name)
                            //{
                            //    Destroy(trackableBehaviour.gameObject);
                            //}
                        }



                        //for (int i = 0; i < existingCardContainers.Count; i++)

                        if (!existingCardContainers.Contains(trackableBehaviour.gameObject.GetComponent<photoCardContainer>().Card))
                        {
                            existingCardContainers.Add(trackableBehaviour.gameObject.GetComponent<photoCardContainer>().Card);
                        }
                        else if (existingCardContainers.Contains(trackableBehaviour.gameObject.GetComponent<photoCardContainer>().Card))
                        {
                            TrackerManager.Instance.GetStateManager().DestroyTrackableBehavioursForTrackable(trackableBehaviour.GetComponent<ImageTargetBehaviour>().Trackable, true);
                        }

                        // 


                        // activate the dataset


                        // TODO: add virtual content as child object(s)
                        GameObject holder = Instantiate(Holder);
                        holder.transform.SetParent(trackableBehaviour.gameObject.transform);
                        holder.name = "Holder";
                        trackableBehaviour.gameObject.GetComponent<imageTargetPhotoCheck>().ARCanvasImage
        = AppManager.GetComponent<AppManager>().loadingScreen.GetComponent<levelLoader>().ARCANVASspawn.gameObject.transform.GetChild(0).gameObject;


                        objectTracker.ActivateDataSet(dataset);
                        AppManager.GetComponent<AppManager>().loadingScreen.GetComponent<levelLoader>().ARCANVASspawn.gameObject.transform.GetChild(0).gameObject.GetComponent<scanAndCollect>().SpawnScannedCards();

                        index++;
                        Debug.Log("index =" + index);

                        if (index <= maxAmount)
                        {
                            testTarget = true;
                        }

                    }
                }

            }
    */
    }
}


