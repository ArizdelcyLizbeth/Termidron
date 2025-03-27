using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display1 : MonoBehaviour
{
    public Camera miniMapC;
    private UIDocument display;

    private RenderTexture miniMapRT;
    private Texture2D miniMapTexture;
    private VisualElement miniMap;

    private Label timer;
    private Label keys;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;

        miniMap = root.Q<VisualElement>("MiniMap");
        miniMapRT = miniMapC.targetTexture;
        miniMapTexture = new Texture2D(miniMapRT.width, miniMapRT.height, TextureFormat.RGBA32, false);


        timer = root.Q<Label>("Timer");
        /*keys = root.Q<Label>("Keys");*/
    }

    void Update()
    {
        RenderTexture.active = miniMapRT;
        miniMapTexture.ReadPixels(new Rect(0, 0, miniMapRT.width, miniMapRT.height), 0, 0);
        miniMapTexture.Apply();
        RenderTexture.active = null;
        miniMap.style.backgroundImage = new StyleBackground(miniMapTexture);
    }

    public void UpdateTime(string time)
    {
        timer.text = time;
    }

    public void UpdateKeysCounter(int number) {
        /**keys.text = number.ToString();*/
    }
}