using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System.Collections.Generic;

public class Controller: MonoBehaviour
{
    public RectTransform selectionBox;
    public LayerMask agentLayerMask;
    public List<NavMeshAgent> selectedAgents = new List<NavMeshAgent>();

    private Vector2 startPos;
    private bool isSelecting;

    void Start()
    {
        // Hacer la imagen del cuadro de selección invisible al iniciar el juego
        selectionBox.gameObject.SetActive(false);
    }

    void Update()
    {
        // Detectar clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            // Guardar posición inicial del clic derecho
            startPos = Input.mousePosition;
            isSelecting = true;

            // Activar la imagen del cuadro de selección al comenzar la selección
            selectionBox.gameObject.SetActive(true);
        }
        // Si se mantiene presionado el clic derecho
        else if (Input.GetMouseButton(1))
        {// Actualizar tamaño y posición del recuadro de selección
            if (isSelecting)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                Vector2 boxStart = startPos;
                Vector2 boxEnd = currentMousePosition;

                // Calcular el centro del recuadro de selección
                Vector2 boxCenter = (boxStart + boxEnd) / 2f;

                // Actualizar posición del recuadro de selección al centro
                selectionBox.position = boxCenter;

                // Calcular el tamaño absoluto del recuadro de selección
                Vector2 boxSize = new Vector2(Mathf.Abs(boxEnd.x - boxStart.x), Mathf.Abs(boxEnd.y - boxStart.y));

                // Actualizar tamaño del recuadro de selección
                selectionBox.sizeDelta = boxSize;
            }
        }
        // Si se suelta el clic derecho
        else if (Input.GetMouseButtonUp(1))
        {
            // Ocultar el recuadro de selección
            selectionBox.gameObject.SetActive(false);
            isSelecting = false;

            // Realizar la selección de agentes dentro del recuadro
            SelectAgentsInBox(startPos, Input.mousePosition);
        }

        // Si se hace clic izquierdo
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Mover todos los agentes seleccionados hacia el punto donde se hizo clic
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, agentLayerMask))
            {
                foreach (NavMeshAgent agent in selectedAgents)
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    void SelectAgentsInBox(Vector2 startPos, Vector2 endPos)
    {
        selectedAgents.Clear(); // Limpiar la lista de agentes seleccionados

        // Convertir las coordenadas del ratón a rayos en el mundo
        Ray startRay = Camera.main.ScreenPointToRay(new Vector3(startPos.x, startPos.y, 0));
        Ray endRay = Camera.main.ScreenPointToRay(new Vector3(endPos.x, endPos.y, 0));

        // Calcular los puntos en el mundo correspondientes al inicio y fin del recuadro de selección
        Plane plane = new Plane(Vector3.up, 0);
        float startDistance, endDistance;
        if (plane.Raycast(startRay, out startDistance) && plane.Raycast(endRay, out endDistance))
        {
            Vector3 worldStartPos = startRay.GetPoint(startDistance);
            Vector3 worldEndPos = endRay.GetPoint(endDistance);

            // Calcular el rectángulo en el mundo correspondiente al recuadro de selección
            Rect selectionRect = new Rect(
                Mathf.Min(worldStartPos.x, worldEndPos.x),
                Mathf.Min(worldStartPos.z, worldEndPos.z),
                Mathf.Abs(worldEndPos.x - worldStartPos.x),
                Mathf.Abs(worldEndPos.z - worldStartPos.z));

            // Obtener todos los objetos con el componente NavMeshAgent
            NavMeshAgent[] allAgents = FindObjectsOfType<NavMeshAgent>();

            // Verificar si los agentes están dentro del recuadro de selección y tienen el tag "Agente"
            foreach (NavMeshAgent agent in allAgents)
            {
                if (agent.CompareTag("Agente") && selectionRect.Contains(new Vector2(agent.transform.position.x, agent.transform.position.z)))
                {
                    // Si el agente está dentro del recuadro y tiene el tag "Agente", agregarlo a la lista de agentes seleccionados
                    selectedAgents.Add(agent);
                }
            }
        }
    }
}
