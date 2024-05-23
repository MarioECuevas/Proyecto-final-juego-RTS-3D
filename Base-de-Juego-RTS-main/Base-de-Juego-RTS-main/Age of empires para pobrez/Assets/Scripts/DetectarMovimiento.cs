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
        // Verifica si la posición actual es diferente a la posición anterior
        if (transform.position != posicionAnterior)
        {
            estaCaminando = true;
            // Actualiza la posición anterior
            posicionAnterior = transform.position;
        }
        else
        {
            estaCaminando = false;
        }

        // Actualiza el parámetro "Camina" del animator
        animador.SetBool("Camina", estaCaminando);
    }
}
