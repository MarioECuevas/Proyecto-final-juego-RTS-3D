using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyUnit : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 5f;
    public int attackDamage = 10;
    public float attackCooldown = 10f;
    public float wanderRadius = 10f;

    private NavMeshAgent agent;
    private GameObject target;
    private bool isAttacking;
    private Coroutine wanderCoroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isAttacking = false;
        wanderCoroutine = StartCoroutine(Wander());
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
                if (hit.CompareTag("Agente") || hit.CompareTag("Base"))
                {
                    float distance = Vector3.Distance(transform.position, hit.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = hit.gameObject;
                    }
                }
            }

            if (closestTarget != null && closestDistance <= attackRange)
            {
                if (wanderCoroutine != null)
                {
                    StopCoroutine(wanderCoroutine);
                    wanderCoroutine = null;
                }
                StartCoroutine(AttackRoutine(closestTarget));
            }
        }
    }

    IEnumerator Wander()
    {
        while (true)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            yield return new WaitForSeconds(Random.Range(5f, 10f)); // Cambiar intervalos según sea necesario
        }
    }

    IEnumerator AttackRoutine(GameObject target)
    {
        isAttacking = true;
        while (target != null)
        {
            Debug.Log("Enemy attacking " + target.name);
            // Intentar acercarse al objetivo
            MoveTowardsTarget(target.transform.position);
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
        // Reiniciar movimiento aleatorio
        wanderCoroutine = StartCoroutine(Wander());
    }

    void MoveTowardsTarget(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
