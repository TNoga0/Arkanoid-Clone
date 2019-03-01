using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameScript : MonoBehaviour
{

    GameObject paddle;
    GameObject ball;
    GameObject[] blocks;
    Vector3[,] blockCoords; //array containing coordinates of the blocks in a grid
    public GameObject[,] blockGrid;
    public int[] gridDimensions = { 5, 17 };

    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.Find("PlayerPad");
        ball = GameObject.Find("Ball");
        blocks = GameObject.FindGameObjectsWithTag("Block").OrderBy(go => go.name).ToArray();
        blockCoords = new Vector3[gridDimensions[0], gridDimensions[1]]; //grid size (to adjust and polish)  !!!!
        blockGrid = new GameObject[gridDimensions[0], gridDimensions[1]];

        //filling out the block grid positions
        for (int w = 0; w < gridDimensions[0]; w++)
        {
            for (int k = 0; k < gridDimensions[1]; k++)
            {
                blockCoords[w, k] = new Vector3(-12.34f + (k*1.5f) + 0.04f*(float)k, 9.5f - ((float)w / 2f) - 0.04f*(float)w, 0);
            }
        }

        System.Random index = new System.Random();
        
        //instantiating the blocks
        for (int w = 0; w < gridDimensions[0]; w++)
        {
            for (int k = 0; k < gridDimensions[1]; k++)
            {
               blockGrid[w,k] = Instantiate(blocks[index.Next(0,3)], blockCoords[w, k], Quaternion.identity);
            }
        }
        //inserting black blocks which drop powerups
        Destroy(blockGrid[2, 6]);
        Destroy(blockGrid[2, 10]);
        blockGrid[2, 6] = Instantiate(blocks[4], blockCoords[2, 6], Quaternion.identity);
        blockGrid[2, 10] = Instantiate(blocks[4], blockCoords[2, 10], Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
