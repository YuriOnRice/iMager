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
    public List<AlbumImageVideos> albumContainers;
    public List<AlbumImageVideos> existingAlbumContainers;
    public List<GameObject> imageTargets = new List<GameObject>();

    [Space(10)]
    [Header("SEARCH ALBUM")]
    public string searchAlbumGroup; //group

    public bool createTarget = false;
    public bool getCards = false;
    public bool cycleCreate = false;
    public int index = 0;//used for adding next photocard script to each photocard object to teh photoinformation container
    public int indexAlbum = 0;
    public int numberInAlbum = 0;

    void Start()
    {
        //VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);
    }


    /*
    void CreateImageTargetFromSideloadedTexture()
    {
        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        // get the runtime image source and set the texture to load
        var runtimeImageSource = objectTracker.RuntimeImageSource;
        runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, "Vuforia/myTarget.jpg", 0.15f, "myTargetName");

        // create a new dataset and use the source to create a new trackable
        var dataset = objectTracker.CreateDataSet();
        var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, "myTargetName");

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
                        trackableBehaviour.gameObject.GetComponent<albumContainer>().Album = albumContainers[i];

                    }
                    //else if (searchSpecific != cardContainers[i].name)
                    //{
                    //    Destroy(trackableBehaviour.gameObject);
                    //}
                }
                //for (int i = 0; i < existingCardContainers.Count; i++)

                if (!existingAlbumContainers.Contains(trackableBehaviour.gameObject.GetComponent<albumContainer>().Album))
                {
                    existingAlbumContainers.Add(trackableBehaviour.gameObject.GetComponent<albumContainer>().Album);
                }
                else if (existingAlbumContainers.Contains(trackableBehaviour.gameObject.GetComponent<albumContainer>().Album))
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


