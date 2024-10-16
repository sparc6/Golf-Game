//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public interface ISpawnCondition
//{
//    bool IsValidSpawn(Vector3 position);
//}
//public class TreeProximityCondition : ISpawnCondition
//{
//    private Terrain terrain;
//    private float radius;

//    public TreeProximityCondition(Terrain terrain, float radius)
//    {
//        this.terrain = terrain;
//        this.radius = radius;
//    }

//    public bool IsValidSpawn(Vector3 position)
//    {
//        foreach (TreeInstance tree in terrain.terrainData.treeInstances)
//        {
//            Vector3 treeWorldPos = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;
//            if (Vector3.Distance(position, treeWorldPos) < radius)
//            {
//                return false; // Too close to a tree
//            }
//        }
//        return true; // Safe to spawn
//    }
//}

//public class WaterProximityCondition : ISpawnCondition
//{
//    public GameObject[] waterPonds;

//    public WaterProximityCondition(float waterLevel)
//    {
//    }

//    public bool IsValidSpawn(Vector3 position)
//    {
//        for (int i = 0; i< waterPonds.Length; i++ )
//        {

//        }
//    }
//}


