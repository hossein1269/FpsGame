using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent NavMeshAgent;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    PlayerWeaponHandler WeaponHandler;

    [SerializeField]
    Animation Animation;

    public float attackDistance = 3;

    bool isFallow = false;

    void Update()
    {
        Fallow();
        if (isFallow)
        {
            if(!Animation.IsPlaying("Attack2"))
                NavMeshAgent.SetDestination(Player.transform.position);

            float distance = Vector3.Distance(Player.transform.position, transform.position);

            if(distance <= attackDistance)
            {
                Animation.Play("Attack2");
            }
            else if(distance > attackDistance && !Animation.IsPlaying("Attack2"))
            {
                Animation.Play("Run");
            }

        }
    }

    void Fallow()
    {

            float distance = Vector3.Distance(Player.transform.position, transform.position);
            float dot = Vector3.Dot(transform.forward, Player.transform.position - transform.position);

            if (distance < 2)
            {
                isFallow = true;
            }

            if (distance < 10 && dot > 1)
            {
                isFallow = true;
            }
            if (Input.GetMouseButtonDown(0) && WeaponHandler.currentAmo > 0 && distance < 30)
            {
                isFallow = true;
            }
        
    }
}
