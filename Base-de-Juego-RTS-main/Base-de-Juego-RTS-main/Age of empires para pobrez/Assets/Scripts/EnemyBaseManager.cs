using UnityEngine;
using TMPro;

public class EnemyBaseManager : MonoBehaviour
{
    public int maxResources = 30;
    public int maxTroops = 5; // M�ximo n�mero de tropas que puede tener el enemigo
    public float resourceGenerationInterval = 3f; // Intervalo entre generaciones de recursos en segundos
    public float troopGenerationInterval = 8f; // Intervalo entre generaciones de tropas en segundos
    public GameObject enemyTroopPrefab;
    public Transform enemySpawnPoint;

    private int currentResources = 0;
    private int currentTroops = 0;
    private float resourceTimer = 0f;
    private float troopTimer = 0f;

    void Start()
    {
        // Inicializar recursos al comienzo del juego
        currentResources = 0;
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

        // Imprimir la cantidad de recursos en la consola
        Debug.Log("Recursos del enemigo: " + currentResources);
    }

    void GenerateResources(int amount)
    {
        if (currentResources < maxResources)
        {
            currentResources += amount;
            if (currentResources > maxResources)
                currentResources = maxResources;
        }

        // Generar tropas si hay suficientes recursos, no se ha alcanzado el l�mite de tropas y ha pasado el intervalo de tiempo
        GenerateEnemyTroop();
    }

    // M�todo para que la base enemiga genere una tropa autom�ticamente
    void GenerateEnemyTroop()
    {
        if (currentResources >= 10 && currentTroops < maxTroops && troopTimer >= troopGenerationInterval) // Verificar recursos, l�mite de tropas y temporizador
        {
            Instantiate(enemyTroopPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
            currentResources -= 10; // Gastar recursos para generar la tropa
            currentTroops++; // Incrementar el contador de tropas del enemigo
            troopTimer = 0f; // Reiniciar el temporizador de generaci�n de tropas
        }
    }
}
