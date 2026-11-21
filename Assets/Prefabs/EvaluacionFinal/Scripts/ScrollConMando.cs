using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollConMando : MonoBehaviour
{
    [SerializeField] private float velocidadScroll = 1.5f;
    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (scrollRect == null || !scrollRect.gameObject.activeInHierarchy)
            return;

        float ejeVertical = 0f;

        // Stick izquierdo o D-Pad del mando de Xbox
        if (Gamepad.current != null)
        {
            float stickY = Gamepad.current.leftStick.y.ReadValue();
            float dpadY = Gamepad.current.dpad.y.ReadValue();

            if (Mathf.Abs(dpadY) > 0.1f)
                ejeVertical = dpadY;
            else if (Mathf.Abs(stickY) > 0.15f)
                ejeVertical = stickY;
        }

        // Flechas Arriba / Abajo en teclado para probar en PC
        if (Mathf.Abs(ejeVertical) < 0.01f && Keyboard.current != null)
        {
            if (Keyboard.current.upArrowKey.isPressed) ejeVertical = 1f;
            else if (Keyboard.current.downArrowKey.isPressed) ejeVertical = -1f;
        }

        if (Mathf.Abs(ejeVertical) > 0.01f)
        {
            float nuevaPosicion = scrollRect.verticalNormalizedPosition + (ejeVertical * velocidadScroll * Time.unscaledDeltaTime);
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(nuevaPosicion);
        }
    }
}