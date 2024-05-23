using UnityEngine;
using TMPro;

public class BaseManager : MonoBehaviour
{
    public int maxResources = 30;
    public int maxTroops = 5; // M�ximo n�mero de tropas que puede tener el jugador
    public float resourceGenerationInterval = 3f; // Intervalo entre generaciones de recursos en segundos
    public float troopGenerationInterval = 8f; // Intervalo entre generaciones de tropas en segundos
    public GameObject playerTroopPrefab;
    public Transform playerSpawnPoint;

    private int currentResources = 0;
    private int currentTroops = 0;
    private float resourceTimer = 0f;
    private float troopTimer = 0f;

    public TextMeshProUGUI resourceText; // Asigna el objeto TextMeshProUGUI desde el editor

    void Start()
    {
        // Inicializar recursos al comienzo del juego
        currentResources = 0;
        UpdateResourceUI();
    }

    public void CaptureBase()
    {
        maxResources += 10;
        maxTroops += 2;
    }

    void Update()
    {
        // Generar recursos con el tiempo
        resourceTimer += Time.deltaTime;
        if (resourceTimer >= resourceGenerationInterval)
        {
            GenerateResources(1); // Generar 1 recurso cada intervalo de tiempo
            resourceTimer = 0f;
        }

        // Contar tiempo para generaci�n de tropas
        if (troopTimer < troopGenerationInterval)
        {
            troopTimer += Time.deltaTime;
        }
    }

    void GenerateResources(int amount)
    {
        if (currentResources < maxResources)
        {
            currentResources += amount;
            if (currentResources > maxResources)
                currentResources = maxResources;

            UpdateResourceUI();
        }
    }

    void UpdateResourceUI()
    {
        // Actualizar el texto que muestra los recursos en pantalla
        if (resourceText != null)
            resourceText.text = currentResources.ToString();
    }

    // M�todo para que el jugador genere una tropa
    public void GeneratePlayerTroop()
    {
        if (currentResources >= 10 && currentTroops < maxTroops && troopTimer >= troopGenerationInterval) // Verificar recursos, l�mite de tropas y temporizador
        {
            Instantiate(playerTroopPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            SpendResources(10); // Gastar recursos para generar la tropa
            currentTroops++; // Incrementar el contador de tropas del jugador
            troopTimer = 0f; // Reiniciar el temporizador de generaci�n de tropas
        }
        else
        {
            Debug.Log("No puedes generar m�s tropas por ahora.");
        }
    }

    // M�todo para gastar recursos, ll�malo desde otras partes del juego seg�n sea necesario
    public void SpendResources(int amount)
    {
        currentResources -= amount;
        if (currentResources < 0)
            currentResources = 0;

        UpdateResourceUI();
    }

    // M�todo para decrementar el contador de tropas al destruir una tropa
    public void DestroyTroop()
    {
        currentTroops--;
    }
}
