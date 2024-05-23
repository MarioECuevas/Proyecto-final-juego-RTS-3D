using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerTroop : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 5f;
    public int attackDamage = 10;
    public float attackCooldown = 10f;

    private NavMeshAgent agent;
    private GameObject target;
    private bool isAttacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isAttacking = false;
    }

    void Update()
    {
        if (!isAttacking)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
            GameObject closestTarget = null;
            float closestDistance = detectionRange;

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemigo") || hit.CompareTag("BaseEnemigos"))
                {
                    float distance = Vector3.Distance(transform.position, hit.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = hit.gameObject;
                    }
                }
            }

            if (closestTarget != null)
            {
                target = closestTarget;
                if (closestDistance <= attackRange)
                {
                    StartCoroutine(AttackRoutine(target));
                }
                else
                {
                    MoveTowardsTarget(target.transform.position);
                }
            }
            else
            {
                target = null;
            }
        }

        // Capturar bases vacías si están dentro del rango de captura
        Collider[] basesInRange = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider baseCollider in basesInRange)
        {
            if (baseCollider.CompareTag("BaseV"))
            {
                BaseVacia emptyBase = baseCollider.GetComponent<BaseVacia>();
                if (!emptyBase.controlledByEnemy && !emptyBase.controlledByPlayer)
                {
                    // Iniciar la captura de la base vacía
                    emptyBase.Capture(false); // El jugador está capturando la base
                }
            }
        }
    }

    IEnumerator AttackRoutine(GameObject target)
    {
        isAttacking = true;
        while (target != null)
        {
            Debug.Log("Player attacking " + target.name);
            Slider healthSlider = target.GetComponentInChildren<Slider>();
            if (healthSlider != null)
            {
                healthSlider.value -= attackDamage;
                if (healthSlider.value <= 0)
                {
                    healthSlider.value = 0;
                    Destroy(target); // Destruye el objeto si su vida llega a 0
                    target = null;
                }
            }
            yield return new WaitForSeconds(attackCooldown);
        }
        isAttacking = false;
    }

    void MoveTowardsTarget(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    void MoveAwayFromTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = transform.position - targetPosition;
        Vector3 newPosition = transform.position + directionToTarget.normalized * moveSpeed * Time.deltaTime;
        agent.SetDestination(newPosition);
    }
}
