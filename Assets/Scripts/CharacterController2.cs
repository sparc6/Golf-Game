using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterController2 : MonoBehaviour
{
    public NavMeshAgent agent;


    public Transform baseDestination;

    public bool walking;
    public bool collected = false;

    public Slider healthBar;

    public float lowHealthThreshold = 30f;
    public float ballReachThreshold = 3f;
    public float distanceToBase;

    [SerializeField] GameObject currentTarget;

    private void Start()
    {

    }
    private void Update()
    {

        Rigidbody rigidbody = agent.GetComponent<Rigidbody>();
        Animator animator = agent.GetComponent<Animator>();
        distanceToBase = Vector3.Distance(agent.transform.position, baseDestination.position);

        if (rigidbody.velocity.magnitude > 1f)
        {
            walking = true;
            
        }
        else walking = false;

        animator.SetBool("walking", walking);

        ChooseBall();

        /// To check distance between Agent and its Destination, and whether if it has a path to the destination.
        // Debug.Log($"Remaining Distance : {agent.remainingDistance}   and   Agent has path : {agent.hasPath}"); 
    }
    
    public void ChooseBall()
    {
        
        GameObject targetBall = null;
        foreach (GameObject ball in BallDifficulty.ballScores.Keys)
        {
            float bestScore = Mathf.NegativeInfinity;
            float distance = Vector3.Distance(agent.transform.position, ball.transform.position);
            float difficulty = BallDifficulty.ballScores[ball];
            float healthPenalty = (healthBar.value < lowHealthThreshold) ? distance * difficulty : 0;
            
            float value = (difficulty / distance) - healthPenalty;

            if(value > bestScore)
            {
                bestScore = value;
                targetBall = ball;
            }

            if(!targetBall == null && !collected)
            {
                agent.SetDestination(targetBall.transform.position);
            }

            if(distance < ballReachThreshold)
            {
                collected = true;
                Destroy(ball);
                return;
            }

            if(collected)
            {
                agent.SetDestination(baseDestination.position);
            }

            if(distanceToBase < ballReachThreshold)
            {
                collected = false;
            }

            currentTarget = targetBall;
            
        }
    }
    //float GetDifficultyMultiplier(GameObject ball)
    //{
    //    BallScore ballScore = ball.GetComponent<BallScore>();
    //    if (ballScore == null)
    //    {
    //        Debug.LogError("BallScore component missing on ball: " + ball.name);
    //        return 0;
    //    }
    //    else
    //    {
    //        if (ballScore.difficultyLevel < 2)
    //        {
    //            return 1f; // Low difficulty
    //        }
    //        else if (ballScore.difficultyLevel < 3)
    //        {
    //            return 2f; // Medium difficulty
    //        }
    //        else
    //        {
    //            return 3f; // High difficulty
    //        }
    //    }
   
    //}

    public void HealthBar()
    {
        if(walking)
        {
            healthBar.value -= Time.deltaTime;
        }
    }



}
