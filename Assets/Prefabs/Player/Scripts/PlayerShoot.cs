using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject[] bala;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
    }
    
    private void Disparar()
    {
        int indexBala = Random.Range(0, bala.Length);
        GameObject.Instantiate(bala[indexBala],transform.position,transform.rotation);
    }
}
