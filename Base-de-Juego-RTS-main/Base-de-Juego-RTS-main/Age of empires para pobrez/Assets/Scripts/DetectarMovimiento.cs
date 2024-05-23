using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarMovimiento : MonoBehaviour
{
    private Animator animador;
    private bool estaCaminando = false;
    private Vector3 posicionAnterior;

    void Start()
    {
        animador = GetComponent<Animator>();
        posicionAnterior = transform.position;
    }

    void Update()
    {
        // Verifica si la posici�n actual es diferente a la posici�n anterior
        if (transform.position != posicionAnterior)
        {
            estaCaminando = true;
            // Actualiza la posici�n anterior
            posicionAnterior = transform.position;
        }
        else
        {
            estaCaminando = false;
        }

        // Actualiza el par�metro "Camina" del animator
        animador.SetBool("Camina", estaCaminando);
    }
}
