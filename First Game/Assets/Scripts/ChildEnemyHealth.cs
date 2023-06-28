using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildEnemyHealth : MonoBehaviour
{
    public float health = 50;

    [SerializeField]
    Animation Animation;

    ChildEnemyAttack EnemyChild;

    NavMeshAgent NavMeshAgent;

    bool dead = false;


    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        EnemyChild = GetComponent<ChildEnemyAttack>();

    }

    void Update()
    {
        if(health <= 0 && !dead)
        {
            dead = true;
            NavMeshAgent.enabled = false;
            EnemyChild.enabled = false;
            Animation.Play("Death");
            Destroy(gameObject,3f);
        }
    }

    public void TakeDamage(float damage)
    {
        if(damage > health)
        {
            health = 0;
        }
        else
        {
            health -= damage;
        }
    }
}
