using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj == null)
        {
            Debug.LogError("No se encontró al jugador con tag 'Player'");
        }
        else
        {
            player = playerObj.transform;
            Debug.Log("Jugador encontrado: " + player.name);
        }
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
            if (Time.time - lastAttackTime > attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        if (player == null) return;

        // 
        PlayerHealth health = player.GetComponentInChildren<PlayerHealth>();
        if (health != null)
        {
            Debug.Log("Atacando al jugador. Vida actual: " + health.GetCurrentHealth());
            health.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("¡No se encontró PlayerHealth en el jugador!");
        }
    }
}
