using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDifficulty : MonoBehaviour
{
    [SerializeField] Transform golfKart;
    [SerializeField] Material easyMaterial;
    [SerializeField] Material moderateMaterial;
    [SerializeField] Material hardMaterial;
    void Start()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(golfKart.position, ball.transform.position);
            Renderer renderer = ball.GetComponent<Renderer>();
            if ( distance < 70 )
            {
                renderer.material = easyMaterial;
            }
            else if (distance < 120)
            {
                renderer.material = moderateMaterial;
            }
            else
            {
                renderer.material = hardMaterial;
            }
        }
    }


}
