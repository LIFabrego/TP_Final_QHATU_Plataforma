using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerControle : MonoBehaviour
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
        playerMovement= GetComponent<PlayerMovement>();
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;
        commands.Add(new MoveCommand(playerMovement, inputVector));

        if (Input.GetKey(KeyCode.Space))
        {
            commands.Add(new AccelerateCommandPlayer(playerMovement, inputVector));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            commands.Add(new AccelerateCommandPlayer(playerMovement, inputVector));
        }
        foreach (var command in commands)
        {
            command.Execute();
        }
        _animator.SetFloat("Horizontal", horizontalInput);
        _animator.SetFloat("Vertical", verticalInput);
        if (horizontalInput != 0 || verticalInput != 0) // Si hay movimiento
        {
            _animator.SetBool("IsRunning", true); // Activa la animación de correr
        }
        else
        {
            _animator.SetBool("IsRunning", false); // Desactiva la animación de correr
        }
    }
}
