using UnityEngine;
using UnityEngine.InputSystem;

public class CerrarAyudaConB : MonoBehaviour
{
    [SerializeField] private DialogoInteractivo dialogoInteractivo;
    [SerializeField] private GameObject cuadroDeAyuda;

    private void Update()
    {
        // Solo actúa si el cuadro de ayuda está visible en pantalla
        if (cuadroDeAyuda == null || !cuadroDeAyuda.activeInHierarchy)
            return;

        bool botonBPulsado = Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame;
        bool escapePulsado = Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;

        if (botonBPulsado || escapePulsado)
        {
            if (dialogoInteractivo != null)
            {
                dialogoInteractivo.VolverDeAyuda();
            }
        }
    }
}