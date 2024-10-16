using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject ball;
    public GameObject golfKart;
    public bool walking;

    private void Update()
    {
        agent.SetDestination(ball.transform.position);

        Rigidbody rigidbody = agent.GetComponent<Rigidbody>();
        Animator animator = agent.GetComponent<Animator>();

        if (rigidbody.velocity.magnitude > 0.02f)
        {
            walking = true;
            
        }
        else walking = false;

        animator.SetBool("walking", walking);
    }
  
}
