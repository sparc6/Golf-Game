using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDifficulty : MonoBehaviour
{
    [SerializeField] Transform golfKart;
    [SerializeField] Material easyMaterial;
    [SerializeField] Material moderateMaterial;
    [SerializeField] Material hardMaterial;
    public static GameObject[] balls;
    public static Dictionary<GameObject, int> ballScores = new Dictionary<GameObject, int>();



    public void BallDifficultySetStart()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(golfKart.position, ball.transform.position);
            Renderer renderer = ball.GetComponent<Renderer>();
            int difficultyScore;
            if (distance < 70)
            {
                renderer.material = easyMaterial;
                difficultyScore = 1;
            }
            else if (distance < 120)
            {
                renderer.material = moderateMaterial;
                difficultyScore = 2;
            }
            else
            {
                renderer.material = hardMaterial;
                difficultyScore = 3;
            }
            ballScores[ball] = difficultyScore;

        }
    }


}
