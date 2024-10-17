using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class CharacterController : MonoBehaviour
{
    public NavMeshAgent agent;
    private GameObject currentTarget;
    public Transform baseDestination;
    public Slider healthBar;

    public Text scoreText;

    public int score = 0;
    public Text finalScore;
    public Text quitPanelScore;

    public bool walking;
    public bool collected = false;

    public float lowHealthThreshold = 50f;
    public float ballReachThreshold = 3f;
    public float distanceToBase;
    private void Start()
    {
        UpdateScoreUI(); // Initialize score display
    }



    public void BallCollectionStart()
    {
        Rigidbody rigidbody = agent.GetComponent<Rigidbody>();
        Animator animator = agent.GetComponent<Animator>();
        distanceToBase = Vector3.Distance(agent.transform.position, baseDestination.position);
        walking = rigidbody.velocity.magnitude > 1f;
        animator.SetBool("walking", walking);

        if (!collected)
        {
            ChooseBall(); // Find the next ball if not already collected
        }
        else if (distanceToBase < ballReachThreshold)
        {
            collected = false; // Reset when NPC returns to base
        }

        HealthBar(); // Decrease health if walking
    }

    public void ChooseBall()
    {
        GameObject targetBall = null;
        float bestScore = Mathf.NegativeInfinity;

        foreach (GameObject ball in BallDifficulty.ballScores.Keys)
        {
            if (ball == null) continue; // Skip if the ball is destroyed

            float distance = Vector3.Distance(agent.transform.position, ball.transform.position);
            float difficulty = BallDifficulty.ballScores[ball];
            float healthPenalty = (healthBar.value < lowHealthThreshold) ? distance * difficulty : 0;
            float value = (difficulty / distance) - healthPenalty;

            if (value > bestScore)
            {
                bestScore = value;
                targetBall = ball;
            }
        }

        // Set destination to the targetBall if available
        if (targetBall != null)
        {
            agent.SetDestination(targetBall.transform.position);
            currentTarget = targetBall;
        }
        else
        {
            agent.SetDestination(baseDestination.position); // Go to base if no balls available
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if NPC has reached the target ball
        if (other.gameObject == currentTarget && !collected)
        {
            collected = true;
            int points = CalculatePoints(other.gameObject); // Calculate points based on difficulty
            score += points; // Add points to the total score
            UpdateScoreUI(); // Update score display

            Destroy(other.gameObject);
            agent.SetDestination(baseDestination.position); // Set destination to base after collecting
        }
    }
    private int CalculatePoints(GameObject ball)
    {
        int difficulty = BallDifficulty.ballScores[ball]; // Retrieve the difficulty from dictionary
        int points;

        switch (difficulty)
        {
            case 1: points = 10; break;   // Low difficulty
            case 2: points = 20; break;   // Medium difficulty
            case 3: points = 30; break;   // High difficulty
            default: points = 0; break;   // Fallback for undefined difficulties
        }
        return points;
    }
    public void HealthBar()
    {
        if (walking)
        {
            healthBar.value -= Time.deltaTime; // Decrease health over time when walking
        }
    }
    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString("F0"); // Update the score text in the UI
        finalScore.text = scoreText.text;
        quitPanelScore.text = scoreText.text;
    }
}
