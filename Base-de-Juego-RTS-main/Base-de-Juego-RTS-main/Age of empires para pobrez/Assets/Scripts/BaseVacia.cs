using UnityEngine;

public class BaseVacia : MonoBehaviour
{
    public bool controlledByEnemy = false;
    public bool controlledByPlayer = false;
    public float captureTime = 5f; // Tiempo que tarda en ser capturada por el enemigo
    public float additionalCaptureTime = 2f; // Tiempo adicional para capturar una base ya controlada por el otro bando
    private float captureTimer = 0f;
    public float captureRange = 3f; // Rango en el que el enemigo puede capturar la base
    public Material enemyMaterial;
    public Material playerMaterial;
    private Renderer baseRenderer;
    private bool isBeingCaptured = false;

    public BaseManager playerBase;
    public EnemyBaseManager enemyBase;

    void Start()
    {
        baseRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Si la base no está controlada por ningún bando y hay enemigos cercanos, capturarla por el enemigo
        if (!controlledByEnemy && !controlledByPlayer)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, captureRange);
            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Enemigo"))
                {
                    if (!isBeingCaptured)
                    {
                        Capture(true);
                        isBeingCaptured = true;
                    }
                    break;
                }
            }
        }

        // Si la base es controlada por el enemigo
        if (controlledByEnemy)
        {
            // Incrementa el temporizador de captura
            captureTimer += Time.deltaTime;

            // Si el temporizador alcanza el tiempo de captura

        }
        // Si la base es controlada por el jugador
        else if (controlledByPlayer)
        {
            // Incrementa el temporizador de captura
            captureTimer += Time.deltaTime;

            // Si hay enemigos cercanos
            Collider[] enemies = Physics.OverlapSphere(transform.position, captureRange);
            if (enemies.Length > 0)
            {
                // Reinicia el temporizador de captura
                captureTimer = 0f;
            }
        }

        // Actualiza el material de la base
        UpdateMaterial();
    }

    // Función para actualizar el material de la base según quién la controla
    void UpdateMaterial()
    {
        if (controlledByEnemy)
        {
            baseRenderer.material = enemyMaterial;
        }
        else if (controlledByPlayer)
        {
            baseRenderer.material = playerMaterial;
        }
    }

    public void Capture(bool isEnemy)
    {
        if (isEnemy && !controlledByPlayer && !controlledByEnemy)
        {
            // Cambia el control de la base al enemigo
            controlledByEnemy = true;
            isBeingCaptured = false; // Reinicia el estado de captura
            Debug.Log("Base captured by enemy: " + gameObject.name);
            if (enemyBase != null)
                enemyBase.CaptureBase(); // Incrementa los recursos y tropas del enemigo
        }
        else if (!isEnemy && !controlledByPlayer && !controlledByEnemy)
        {
            // Cambia el control de la base al jugador
            controlledByPlayer = true;
            isBeingCaptured = false; // Reinicia el estado de captura
            Debug.Log("Base captured by player: " + gameObject.name);
            if (playerBase != null)
                playerBase.CaptureBase(); // Incrementa los recursos y tropas del jugador
        }
    }
}
