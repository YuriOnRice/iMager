using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public string loadLevel;
    public GameObject ARCANVAS;
    public GameObject ARCANVASspawn;
    public GameObject ARCamera;

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public GameObject appManager;

    

    public void LoadSceneAdditive(string level)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(level));

    }

    public void LoadScene(string level)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronouslyOne(level));

    }
    IEnumerator LoadAsynchronouslyOne(string level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            sliderText.text = progress * 100f + "%";
            yield return null;
        }

        if (operation.isDone)
        {
            loadingScreen.SetActive(false);
        }
    }
    IEnumerator LoadAsynchronously(string level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            sliderText.text = progress * 100f + "%";
            yield return null;
        }

        if (operation.isDone)
        {
            Debug.Log("lol");
            loadingScreen.SetActive(false);
            //if (level == "ARScene")
            {
                ARCANVASspawn = Instantiate(ARCANVAS);

                
                yield return new WaitForSecondsRealtime(0.1f);
                if (ARCamera.GetComponent<CreateImageTargets>())
                {
                    appManager.GetComponent<AppManager>().mainCamera.SetActive(false);
                    appManager.GetComponent<AppManager>().ARBool = true;
                    yield return new WaitForSecondsRealtime(0.1f);
                    ARCANVASspawn.gameObject.transform.GetChild(5).gameObject.AddComponent<UnityEngine.UI.Image>();
                    //ARCANVASspawn.gameObject.transform.GetChild(0).gameObject.GetComponent<scanAndCollect>().InitStart();
                    yield return new WaitForSecondsRealtime(0.1f);
                    //ARCamera = ARCANVASspawn.gameObject.transform.GetChild(0).gameObject.GetComponent<scanAndCollect>().ARCamera;
                    yield return new WaitForSecondsRealtime(0.1f);
                    ARCamera.GetComponent<CreateImageTargets>().filteredAlbumContainers =
                        appManager.GetComponent<AppManager>().searchAlbumListFiltered;
                    yield return new WaitForSecondsRealtime(0.1f);
                    ARCamera.GetComponent<CreateImageTargets>().InitStart();
                    yield return new WaitForSecondsRealtime(0.1f);
                    ARCamera.GetComponent<CreateImageTargets>().GetLoadingBar();
                    ARCamera.GetComponent<CreateImageTargets>().LoadingBarText.GetComponent<TextMeshProUGUI>().text 
                = "Albums: " + ARCamera.GetComponent<CreateImageTargets>().index + " / " + 
                ARCamera.GetComponent<CreateImageTargets>().maxAmount;

                }
                else if (!ARCamera.GetComponent<CreateImageTargets>())
                {
                    ARCANVASspawn.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                }
                
                

            }
        }
    }
}
