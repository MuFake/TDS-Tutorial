using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public bool chasePlayer = true;
    public float moveSpeed;

    public float detectionDistance = 5.0f;
    public float curDistance;

    bool isAttacking = false;
    public float attackDistance = 1.5f;
    public float attackTimer = 1.0f;
    public float damage = 20.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        curDistance = Vector3.Distance(player.position, transform.position);

        if (curDistance <= detectionDistance)
        {
            chasePlayer = true;
        }
        else
            chasePlayer = false;

        if(chasePlayer && player != null && agent.isOnNavMesh && !isAttacking)
        {
            agent.speed = moveSpeed;
            agent.destination = player.position;
        }


        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackDistance) && !isAttacking)
        {
            if(hit.transform.gameObject.name == "Player")
            {
                isAttacking = true;
                hit.transform.gameObject.BroadcastMessage("ApplyDamage", damage);
                Invoke("AttackTimeDelay", attackTimer);
            }
        }
    }

    void AttackTimeDelay()
    {
        isAttacking = false;
    }
}
