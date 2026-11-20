using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Button))]
public class BotonVolverConB : MonoBehaviour
{
    private Button botonAtras;

    private void Awake()
    {
        botonAtras = GetComponent<Button>();
    }

    private void Update()
    {
        // Si el botón no está activo o no es interactuable, no hacemos nada
        if (botonAtras == null || !botonAtras.gameObject.activeInHierarchy || !botonAtras.interactable)
            return;

        // 1. Comprobamos si se pulsó el botón B en algún mando de Xbox conectado
        bool botonBPulsado = Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame;

        // 2. Comprobamos si se pulsó Escape en el teclado (para tus pruebas en PC)
        bool escapePulsado = Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;

        if (botonBPulsado || escapePulsado)
        {
            // Ejecuta exactamente el mismo evento OnClick() que configuraste en el Inspector
            botonAtras.onClick.Invoke();
        }
    }
}
