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
    private VisualElement[] keys;
    private VisualElement[] hearts;

    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;

        miniMap = root.Q<VisualElement>("MiniMap");
        miniMapRT = miniMapC.targetTexture;
        miniMapTexture = new Texture2D(miniMapRT.width, miniMapRT.height, TextureFormat.RGBA32, false);


        timer = root.Q<Label>("Timer");
        keys = new VisualElement[3];
        keys[0] = root.Q<VisualElement>("Key1");
        keys[1] = root.Q<VisualElement>("Key2");
        keys[2] = root.Q<VisualElement>("Key3");
        DisableAllKeys();
        hearts = new VisualElement[5];
        hearts[0] = root.Q<VisualElement>("Heart1");
        hearts[1] = root.Q<VisualElement>("Heart2");
        hearts[2] = root.Q<VisualElement>("Heart3");
        hearts[3] = root.Q<VisualElement>("Heart4");
        hearts[4] = root.Q<VisualElement>("Heart5");
        EnableAllHearts();
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

    public void UpdateKeysCounter(int number) 
    {
        keys[number - 1].style.display = DisplayStyle.Flex;
    }

    public void UpdateHeartsCounter(int number)
    {
        hearts[4 - number].style.display = DisplayStyle.None;
    }

    private void DisableAllKeys()
    {
        foreach (var key in keys)
        {
            key.style.display = DisplayStyle.None;
        }
    }

    private void EnableAllHearts()
    {
        foreach (var heart in hearts)
        {
            heart.style.display = DisplayStyle.Flex;
        }
    }
}