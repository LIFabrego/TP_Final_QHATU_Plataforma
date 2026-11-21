using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AtajoBotonControl : MonoBehaviour
{
    public enum BotonXbox
    {
        BotonY, // buttonNorth (arriba en el diamante ABXY)
        BotonX  // buttonWest (izquierda en el diamante ABXY)
    }

    [SerializeField] private Button botonObjetivo;
    [SerializeField] private BotonXbox botonAsignado = BotonXbox.BotonY;
    [SerializeField] private bool permitirTeclaHParaPruebas = true;

    private void Update()
    {
        if (botonObjetivo == null || !botonObjetivo.gameObject.activeInHierarchy || !botonObjetivo.interactable)
            return;

        bool pulsadoEnMando = false;

        if (Gamepad.current != null)
        {
            pulsadoEnMando = botonAsignado == BotonXbox.BotonY
                ? Gamepad.current.buttonNorth.wasPressedThisFrame
                : Gamepad.current.buttonWest.wasPressedThisFrame;
        }

        // Tecla H en PC para probar sin mando físico
        bool pulsadoEnTeclado = permitirTeclaHParaPruebas &&
                                Keyboard.current != null &&
                                Keyboard.current.hKey.wasPressedThisFrame;

        if (pulsadoEnMando || pulsadoEnTeclado)
        {
            botonObjetivo.onClick.Invoke();
        }
    }
}
