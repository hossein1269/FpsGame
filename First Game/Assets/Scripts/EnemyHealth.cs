using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    Animation Animation;

    [SerializeField]
    GameObject Child;

    EnemyAttack EnemyAttack;
    NavMeshAgent NavMeshAgent;

    public float health = 100f;
    bool have_child = false;

    private void Start()
    {
        EnemyAttack = GetComponent<EnemyAttack>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 50 && !have_child)
        {

            Instantiate(Child, transform.position, transform.rotation);
            Instantiate(Child, transform.position, transform.rotation);
            Instantiate(Child, transform.position, transform.rotation);
            have_child = true;
        }

        if(health <= 0)
        {
            NavMeshAgent.enabled = false;
            EnemyAttack.enabled = false;
            Animation.Play("Death");
            Invoke("EnemyDestroy", 1f);
        }
    }

    void EnemyDestroy()
    {
        Animation.Play("EnemyDestroy");
        Destroy(gameObject,.5f);
    }
}
