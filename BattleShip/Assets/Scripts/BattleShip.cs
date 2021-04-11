using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleShip : MonoBehaviour
{
    // Start is called before the first frame update
    public int width = 10;
    public int height = 9;
    private int[,] grid;

    public Text display;
    
    //set up a bool to test if player hits a ship
    private int YouHitTheShip = 1;
    
    //if you didn't hit ship, generate a grey spot and put into list
    private List<GameObject> spawnedSpots = new List<GameObject>();

    public GameObject greyPrefab, redPrefab;

    public int row = 0;
    public int col= 0 ;

    void Start()
    {
        // Instantiate and initialize the grid.
        
        grid = new int[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                grid[x, y] = 2;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // If you press space, it reloads the scene.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (YouHitTheShip > 2)
        {
            youWin();
        }

    }
    public bool noShip(int x, int y)
    {
        return grid[x, y] == 0;
    }
        
    // if there is ship, return 1;

    public bool hitShip(int x, int y)
    {
        return grid[x, y] == 1;
    }
        
    //if you haven't check the spot, return 2;
    public bool isEmpty(int x, int y)
    {
        return grid[x, y] == 2;
    }

    public void UpdateDisplay()
    {
        foreach (var piece in spawnedSpots)
        {
            Destroy(piece);
        }
        spawnedSpots.Clear();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (noShip(x, y))
                {
                    var noShipSprite = Instantiate(greyPrefab);
                    noShipSprite.transform.position = new Vector3(x, y);
                    spawnedSpots.Add(noShipSprite);
                }

                if (hitShip(x, y))
                {
                    var hitShipSprite = Instantiate(redPrefab);
                    hitShipSprite.transform.position = new Vector3(x, y);
                    spawnedSpots.Add(hitShipSprite);
                }
            }
        }
        
    }
    
    public void youWin()
    {
        display.text = "Good job! You win;)";
    }
    
    //location position
    public int locationPositionX(int row)
    {
        return row;
    }

    public int locationPositionY(int col)
    {
        return col;
    }


    // if the spot is empty, check the spot, and then drop something


    public void CheckPosition()
    {
        if (!isEmpty) return;
        /*
          // use grid[row, column]==value to check status
          
         for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (grid[x, y] == 0)
                {
                    return 0;
                }

                if (grid[x, y] == 1)
                {
                    return 1;
                    YouHitTheShip++;
                }
            }*/


        if (isEmpty(row, col))
        {


            if (grid[row, col] == 0)
            {
                return 0;
            }

            if (grid[row, col] == 1)
            {
                return 1;
            }
        }

        UpdateDisplay();
    }
        

    }
}
