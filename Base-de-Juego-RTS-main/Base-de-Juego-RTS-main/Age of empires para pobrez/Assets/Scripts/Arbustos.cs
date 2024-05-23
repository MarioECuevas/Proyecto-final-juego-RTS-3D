using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbustos : MonoBehaviour
{
    public float shakeAmount = 0.5f; // Cantidad de sacudida del arbusto
    public float shakeSpeed = 5.0f; // Velocidad de la sacudida

    private Vector3 originalPosition;
    private bool isShaking = false;

    void Start()
    {
        originalPosition = transform.position; // Guarda la posición original del arbusto
    }

    void Update()
    {
        if (isShaking)
        {
            // Calcula la nueva posición del arbusto con una sacudida suave
            Vector3 newPosition = originalPosition + new Vector3(Random.Range(-shakeAmount, shakeAmount), 0, Random.Range(-shakeAmount, shakeAmount));
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * shakeSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agente"))
        {
            // Comienza a sacudir el arbusto cuando el agente entra en su collider
            isShaking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Agente"))
        {
            // Detiene la sacudida del arbusto cuando el agente sale de su collider
            isShaking = false;
            // Resetea la posición del arbusto a su posición original
            transform.position = originalPosition;
        }
    }
}
