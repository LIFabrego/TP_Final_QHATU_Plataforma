using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private List<ICommandPlayer> commands;
    private Animator _animator;

    [Header("AnimacionHumano")]
    [SerializeField] private GameObject animacionHumano;

    [Header("AnimacionGato")]
    [SerializeField] private GameObject animacionGato;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        commands = new List<ICommandPlayer>();
        var mundo = SeleccionMundoManager.Instance?.mundoSeleccionado ?? SeleccionMundoManager.Mundo.Humano;

        if (mundo == SeleccionMundoManager.Mundo.Gato)
        {
            animacionGato.SetActive(true);
            animacionHumano.SetActive(false);
        }
        else
        {
            animacionGato.SetActive(false);
            animacionHumano.SetActive(true);
        }
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        commands.Clear();

        // Lee tanto WASD/Flechas como Joystick Izquierdo y D-Pad (configurados en Input Manager)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        if (inputVector.magnitude > 1f)
        {
            inputVector.Normalize();
        }

        commands.Add(new MoveCommand(playerMovement, inputVector));

        // Lee tanto Espacio en PC como el botón asignado en Xbox (ej. botón A / joystick button 0)
        if (Input.GetButton("Jump"))
        {
            commands.Add(new AccelerateCommandPlayer(playerMovement, inputVector));
        }

        foreach (var command in commands)
        {
            command.Execute();
        }

        _animator.SetFloat("Horizontal", horizontalInput);
        _animator.SetFloat("Vertical", verticalInput);
        _animator.SetBool("IsRunning", horizontalInput != 0 || verticalInput != 0);
    }
}