using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieBrain : MonoBehaviour
{
    public NavMeshAgent TravelAgent;
    public Transform Target;
    public Animator Animater;
    public Stats myStats;



    void MoveToTarget()
    {
        TravelAgent.speed = 3.5f;
        TravelAgent.angularSpeed = 100f;
        TravelAgent.stoppingDistance = 3.5f;

        TravelAgent.SetDestination(Target.position);
        Animater.SetFloat("running_level", 1f, 0.3f, Time.deltaTime);

        if(Vector3.Distance(Target.position, transform.position)<=TravelAgent.stoppingDistance)
        {
           Animater.SetFloat("running_level", 0f);
        }
    }



    void Update()
    {
        MoveToTarget();
        transform.LookAt(Target);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "projectile")
        {
            // subtract hp from zombie
            print("zombie: ow! i got shot");
        }
    }



}

