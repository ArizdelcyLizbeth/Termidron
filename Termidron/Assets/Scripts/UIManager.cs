using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador central de la interfaz de usuario (UI) del juego. 
/// Se encarga de actualizar elementos visuales como el temporizador y la pantalla final, 
/// así como de activar y desactivar elementos de UI según sea necesario.
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameObject[] uiElements;

    /// <summary>
    /// Actualiza el temporizador visible en pantalla mediante el componente Display1.
    /// </summary>
    /// <param name="time">Cadena de texto que representa el tiempo actual del juego.</param>
    public void UpdateTimer(string time)
    {
        Display1 display1 = uiElements[1].GetComponent<Display1>();
        display1.UpdateTime(time);
    }

    /// <summary>
    /// Muestra el mensaje final del juego, incluyendo título y subtítulo,
    /// utilizando el componente Display2.
    /// </summary>
    /// <param name="title">Texto principal a mostrar (título).</param>
    /// <param name="subtitle">Texto secundario a mostrar (subtítulo).</param>
    public void UpdateFinal(string title, string subtitle)
    {
        Display2 display2 = uiElements[2].GetComponent<Display2>();
        display2.UpdateTitleAndSubtitle(title, subtitle);
    }

    /// <summary>
    /// Activa un elemento específico de la UI según su índice en el arreglo.
    /// </summary>
    /// <param name="index">Índice del elemento en el arreglo uiElements.</param>
    public void ActivateUI(int index)
    {
        if (index >= 0 && index < uiElements.Length)
        {
            uiElements[index].SetActive(true);
        }
    }

    /// <summary>
    /// Desactiva un elemento específico de la UI según su índice en el arreglo.
    /// </summary>
    /// <param name="index">Índice del elemento en el arreglo uiElements.</param>
    public void DeactivateUI(int index)
    {
        if (index >= 0 && index < uiElements.Length)
        {
            uiElements[index].SetActive(false);
        }
    }

    /// <summary>
    /// Desactiva todos los elementos de la UI registrados en el arreglo.
    /// </summary>
    public void DeactivateAllUI()
    {
        foreach (var element in uiElements)
        {
            element.SetActive(false);
        }
    }

    /// <summary>
    /// Activa todos los elementos de la UI registrados en el arreglo.
    /// </summary>
    public void ActivateAllUI()
    {
        foreach (var element in uiElements)
        {
            element.SetActive(true);
        }
    }
}