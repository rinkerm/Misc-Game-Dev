using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    public void Create()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PlayerPrefs.Save();

        string levelName = PlayerPrefs.GetInt("Level").ToString();
        Scene newScene = SceneManager.CreateScene(levelName);
        Scene OldScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(newScene);
        GameObject cammy = new GameObject("Cammy",typeof(Camera));
        cammy.GetComponent<Camera>().backgroundColor = Color.black;

        GameObject canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvas.transform.SetParent(cammy.transform);
        canvas.transform.position = new Vector3(0, 0, 0);
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        canvas.GetComponent<Canvas>().worldCamera = cammy.GetComponent<Camera>();

        GameObject events = new GameObject("Events", typeof(EventSystem), typeof(StandaloneInputModule));

        GameObject texty = new GameObject("Texty", typeof(Text));
        texty.transform.SetParent(canvas.transform);
        texty.GetComponent<RectTransform>().localPosition = new Vector3(0, 300, 1000);
        texty.GetComponent<Text>().text = levelName;
        Font arial = Resources.FindObjectsOfTypeAll(typeof(Font))[0] as Font;
        texty.GetComponent<Text>().font = arial;
        texty.GetComponent<Text>().resizeTextForBestFit = true;
        texty.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        GameObject booton = new GameObject("Booton", typeof(Button),typeof(Image));
        booton.transform.SetParent(canvas.transform);
        booton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIButton");
        booton.GetComponent<RectTransform>().localPosition = new Vector3(0, -60, 1500);
        booton.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        booton.GetComponent<Button>().onClick.AddListener(Create);
        booton.GetComponent<Button>().targetGraphic = booton.GetComponent<Image>();

        GameObject bootonText = new GameObject("BootonText", typeof(Text));
        bootonText.transform.SetParent(booton.transform);
        bootonText.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        bootonText.GetComponent<Text>().text = "Next";
        bootonText.GetComponent<Text>().color = Color.black;
        bootonText.GetComponent<Text>().font = arial;
        bootonText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        
        //SceneManager.UnloadSceneAsync(OldScene);
        Debug.Log(PlayerPrefs.GetInt("Level").ToString());
    }
}
