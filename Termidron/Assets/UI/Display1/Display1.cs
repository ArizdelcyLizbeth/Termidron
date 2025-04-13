using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Script que maneja la interfaz de usuario para la pantalla de juego, mostrando el minimapa,
/// el temporizador, las llaves y los corazones del jugador.
/// </summary>
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

    /// <summary>
    /// Método llamado cuando el objeto se habilita en la escena.
    /// Inicializa todos los elementos visuales de la UI, incluidos el minimapa, temporizador,
    /// llaves y corazones.
    /// </summary>
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

    /// <summary>
    /// Método que se llama cada cuadro para actualizar la textura del minimapa con la imagen
    /// renderizada de la cámara.
    /// </summary>
    void Update()
    {
        RenderTexture.active = miniMapRT;
        miniMapTexture.ReadPixels(new Rect(0, 0, miniMapRT.width, miniMapRT.height), 0, 0);
        miniMapTexture.Apply();
        RenderTexture.active = null;
        miniMap.style.backgroundImage = new StyleBackground(miniMapTexture);
    }

    /// <summary>
    /// Actualiza el temporizador en la UI con el tiempo proporcionado.
    /// </summary>
    /// <param name="time">El tiempo que se mostrará en el temporizador.</param>
    public void UpdateTime(string time)
    {
        timer.text = time;
    }

    /// <summary>
    /// Actualiza el contador de llaves mostrando la llave correspondiente en la UI.
    /// </summary>
    /// <param name="number">El número de la llave que debe ser visible.</param>
    public void UpdateKeysCounter(int number) 
    {
        keys[number - 1].style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Actualiza el contador de corazones, ocultando el corazón correspondiente al número de vida.
    /// </summary>
    /// <param name="number">El número de vida que debe ser ocultado.</param>
    public void UpdateHeartsCounter(int number)
    {
        hearts[4 - number].style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Desactiva la visibilidad de todas las llaves en la UI.
    /// </summary>
    private void DisableAllKeys()
    {
        foreach (var key in keys)
        {
            key.style.display = DisplayStyle.None;
        }
    }

    /// <summary>
    /// Activa la visibilidad de todos los corazones en la UI.
    /// </summary>
    private void EnableAllHearts()
    {
        foreach (var heart in hearts)
        {
            heart.style.display = DisplayStyle.Flex;
        }
    }
}