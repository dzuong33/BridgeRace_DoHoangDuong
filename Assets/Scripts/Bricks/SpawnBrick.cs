using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnBrick : MonoBehaviour
{

    [SerializeField] private List<GameObject> brickPrefab;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private GameObject brickContainer;
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private float spacing;

    [SerializeField] private LayerMask layerMask;
  
    private void Start()
    {
        SpawnBrickPrefab();   
    }

    private void SpawnBrickPrefab()
    {
        for(int row = 0; row < this.row; row++)
        {
            for(int column = 0; column < this.column; column++)
            {
                spawnPosition.x = brickContainer.transform.position.x + column * spacing;
                spawnPosition.y = brickContainer.transform.position.y;
                spawnPosition.z = brickContainer.transform.position.z + row * spacing;
                GameObject spawnBricks = brickPrefab[Random.Range(0, brickPrefab.Count)];
                GameObject spawnedBricks = Instantiate(spawnBricks, spawnPosition, Quaternion.identity);
                spawnedBricks.AddComponent<BrickState>();
                spawnedBricks.transform.parent = brickContainer.transform;
            }
        }
    }
    

}
