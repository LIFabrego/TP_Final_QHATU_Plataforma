using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarObjeto2 : MonoBehaviour
{

    private GameObject objetoRecogido;
    [SerializeField] private float fuerzaLanzamiento = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (objetoRecogido == null && other.CompareTag("Trofeo"))
        {
            objetoRecogido = other.gameObject;
            MonoBehaviour movimientoScript = objetoRecogido.GetComponent<MonoBehaviour>();
            if (movimientoScript != null)
            {
                movimientoScript.enabled = false; 
            }
            objetoRecogido.transform.rotation = transform.rotation;

            objetoRecogido.transform.SetParent(transform);
            objetoRecogido.transform.localPosition = new Vector3(1, 1, 0); 

            Rigidbody rb = objetoRecogido.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void Update()
    {
        if (objetoRecogido != null && Input.GetKeyDown(KeyCode.E))
        {
            Rigidbody rb = objetoRecogido.GetComponent<Rigidbody>();
            if (rb != null)
            {
               
                objetoRecogido.transform.SetParent(null);
                objetoRecogido.transform.position += transform.forward * 2f;

                MonoBehaviour movimientoScript = objetoRecogido.GetComponent<MonoBehaviour>();
                if (movimientoScript != null)
                {
                    movimientoScript.enabled = true;
                }
                rb.isKinematic = false;  // Reactivar la física
                Vector3 direccionLanzamiento = transform.forward.normalized;
                rb.AddForce(direccionLanzamiento * fuerzaLanzamiento, ForceMode.Impulse);  // Aplicar la fuerza de lanzamiento
            }
            objetoRecogido = null;
        }
    }
}

