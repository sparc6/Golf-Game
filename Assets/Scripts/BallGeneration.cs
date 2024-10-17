using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGeneration : MonoBehaviour
{
    public GameObject ballPrefab;
    public Terrain terrain;
    public int ballCount = 10;
    public float treeCheckRadius = 2f;
    public GameObject [] waterPonds;


    public void BallSpawner()
    {
        int spawnedBalls = 0;
        while (spawnedBalls < ballCount)
        {
            if (SpawnBall())
            {
                spawnedBalls++;
            }
        }
    }

    bool SpawnBall()
    {
        
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        
        float posX = Random.Range(-(terrainWidth) / 2, (terrainWidth) / 2);
        float posZ = Random.Range(-(terrainLength) / 2, (terrainLength) / 2);

        float posY = terrain.SampleHeight(new Vector3(posX, 0, posZ)) + terrain.transform.position.y;

        Vector3 spawnPosition = new Vector3(posX, posY, posZ);

        if(!IsNearTree(spawnPosition) && !IsInWater(spawnPosition))
        {
            Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsNearTree(Vector3 position)
    {
        foreach(TreeInstance tree in terrain.terrainData.treeInstances)
        {
            Vector3 treeWorldPos = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

            if (Vector3.Distance(position, treeWorldPos)<treeCheckRadius)
            {
                return true;
            }
        }
        return false;
    }

    bool IsInWater(Vector3 position)
    {
        for (int i = 0; i<waterPonds.Length;i++)
        {
            float minXvalue = (waterPonds[i].transform.localScale.x) / 2 - waterPonds[i].transform.position.x;
            float maxXvalue = (waterPonds[i].transform.localScale.x) / 2 + waterPonds[i].transform.position.x;
            float minYvalue = (waterPonds[i].transform.localScale.y) / 2 - waterPonds[i].transform.position.y;
            float maxYvalue = (waterPonds[i].transform.localScale.y) / 2 + waterPonds[i].transform.position.y;
            if(position.x > minXvalue && position.x<maxXvalue && position.y > minYvalue && position.y < maxYvalue)
            {
                return true;
            }
        }
        return false;
    }
    

}
