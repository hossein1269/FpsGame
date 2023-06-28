using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildEnemyAttack : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent NavMeshAgent;

    [SerializeField]
    Animation Animation;

    [Range(1,5)]
    public int attackDistance = 2;

    private GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        NavMeshAgent.SetDestination(Player.transform.position);

        if (!Animation.IsPlaying("Attack2"))
            NavMeshAgent.SetDestination(Player.transform.position);

        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance <= attackDistance)
        {
            Animation.Play("Attack2");
        }
        else if (distance > attackDistance && !Animation.IsPlaying("Attack2"))
        {
            Animation.Play("Run");
        }

    }
}
