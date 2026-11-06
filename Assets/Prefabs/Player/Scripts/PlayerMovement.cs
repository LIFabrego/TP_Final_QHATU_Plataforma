using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private IMovementPlayer movementStrategy;
    private Player player;
    private Vector3 initialPosition;

    void Start()
    {
        player = new Player(3f,2f,7f); //Velocidad, Aceleracion, salto
        SetMovementStrategy(new SmoothMovement());
        initialPosition = transform.position;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terreno"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }*/
    public void SetMovementStrategy(IMovementPlayer movementStrategy)
    {
        this.movementStrategy = movementStrategy;
    }
    public void MovePlayer(Vector2 input)
    {
        movementStrategy?.Move(gameObject.transform, player,input);
        UpdateAnimator(input);
    }
    private void UpdateAnimator(Vector2 input)
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
            animator.SetFloat("speed", input.sqrMagnitude); // útil para saber si está quieto
        }
    }
}
