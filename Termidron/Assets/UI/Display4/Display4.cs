using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Script que maneja la pantalla de creditos, permitiendo al usuario regresar a la pantalla principal.
/// </summary>
public class Display4 : MonoBehaviour
{
    public UIManager uiManager;
    private UIDocument display;
    private Button back;

    /// <summary>
    /// Método llamado cuando el objeto se habilita en la escena.
    /// Inicializa los elementos visuales de la UI y registra el callback del botón "Back".
    /// </summary>
    void OnEnable()
    {
        display = GetComponent<UIDocument>();
        VisualElement root = display.rootVisualElement;
        back = root.Q<Button>("Back");
        back.RegisterCallback<ClickEvent>(ShowMainDisplay);
    }

    /// <summary>
    /// Método que se ejecuta cuando se hace clic en el botón "Back".
    /// Desactiva la pantalla actual (pantalla de instrucciones o créditos) y activa la pantalla principal.
    /// </summary>
    /// <param name="evt">El evento de clic que activa el callback.</param>
    private void ShowMainDisplay(ClickEvent evt)
    {
        uiManager.DeactivateUI(4);
        uiManager.ActivateUI(0);
    }
}
