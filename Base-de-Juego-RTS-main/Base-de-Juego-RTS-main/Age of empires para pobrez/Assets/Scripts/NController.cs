using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NController : MonoBehaviour
{
    public LayerMask groundLayer; // Capa para detectar el suelo
    public NavMeshAgent agent;
    public bool isSelected = false;

    void Update()
    {
        // Manejar la selección del personaje
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("hizo clic");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.transform.CompareTag("Agente")) // Cambiado a "Agente"
                {
                    Debug.Log("hizo clic agente");
                    isSelected = true;
                    agent = hit.transform.GetComponent<NavMeshAgent>();
                }


                // Manejar el movimiento del personaje seleccionado
                if (isSelected && Input.GetMouseButtonDown(1) && hit.transform.CompareTag("Ground")) // Deseleccionar con clic derecho
                {
                    Debug.Log("ya no mueve agentes");
                    isSelected = false;
                    agent = null;
                }
            }
        }


        // Manejar el movimiento del personaje seleccionado
        if (isSelected && Input.GetMouseButton(0)) // Mover solo cuando está seleccionado y se hace clic derecho
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }


}
