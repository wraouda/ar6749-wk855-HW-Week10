using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleShip : MonoBehaviour
{
    // set width and height of the grid
    public int width = 10;
    public int height = -9;
    private int[,] grid;
    
    // adding offsets to move the grid around
    public int xOffset;
    public int yOffset;
    // display text
    public Text display;
    
    //set up a bool to test if player hits a ship
    private int YouHitTheShip = 1;
    private bool hasShip = false; // Check to see if there is a ship

    //if you didn't hit ship, generate a grey spot and put into list
    private List<GameObject> spawnedSpots = new List<GameObject>();
    
    public GameObject greyPrefab, redPrefab; // hit and miss markers

    public int col; // adding columns
    public int row; // adding rows

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
        
        // place the ship 

        grid[1, 4] = 1;
        grid[3, 3] = 1;
        grid[7, 8] = 1;


    }

    // Update is called once per frame
    void Update()
    {
        // If you press space, it reloads the scene.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
// if you hit the ship 3 times, you win
        if (YouHitTheShip > 2)
        {
            youWin();
        }

        
    }
    
    // grid setting no ship seen to 0
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

    public void UpdateDisplay() // this is never called
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
    
   // display a "you win" text when you hit 3 different ships
    public void youWin()
    {
        display.text = "Good job! You win;)";
    }


    // if the spot is empty, check the spot, and then drop something

    /*
    public void DestoryCol(int column)
    {
        foreach (var piece in spawnedSpots)
        {
            Destroy(piece);
        }
        spawnedSpots.Clear();
        // Check to see if there is a ship in this col, if not destory the col
        for (var y = 0; y < height; y++)
        {
            var hitShipSprite = Instantiate(redPrefab);
            hitShipSprite.transform.position = new Vector3(column, y);
            spawnedSpots.Add(hitShipSprite);
        }
    }

    public void DestoryRow(int row)
    {
        foreach (var piece in spawnedSpots)
        {
            Destroy(piece);
        }
        spawnedSpots.Clear();
        for (var x = 0; x < width; x++)
        {
            var hitShipSprite = Instantiate(redPrefab);
            hitShipSprite.transform.position = new Vector3(x, row);
            spawnedSpots.Add(hitShipSprite);

        }
    }
    
    public void DestoryColNoShip(int column)
    {
        foreach (var piece in spawnedSpots)
        {
            Destroy(piece);
        }
        spawnedSpots.Clear();
        for (var y = 0; y < height; y++)
        {
            var noShipSprite = Instantiate(greyPrefab);
            noShipSprite.transform.position = new Vector3(column, y);
            spawnedSpots.Add(noShipSprite);
        }  
    }

    public void DestoryRowNoShip(int row)
    {
        foreach (var piece in spawnedSpots)
        {
            Destroy(piece);
        }
        spawnedSpots.Clear();
        for (var x = 0; x < width; x++)
        {
            var noShipSprite = Instantiate(greyPrefab);
            noShipSprite.transform.position = new Vector3(x, row);
            spawnedSpots.Add(noShipSprite);
        }
    }*/
// check positions for the y clomumns
    public void CheckPositionY(int column)
    {
        // check if point row,col is empty, if so return
        if (!isEmpty(row, col)) return;

        // use grid[row, column]==value to check status

        for (var y = 0; y < height; y++)
        {
            if (grid[column, y] == 2) // if there is no ship, set every column to be grey
            {
                var noShipSprite = Instantiate(greyPrefab);
                noShipSprite.transform.position = new Vector3(column, y);
                spawnedSpots.Add(noShipSprite);
            }

            if (grid[column, y] == 1) // if there is a ship, set the ship's position to be red
            { 
                YouHitTheShip++;
                var hitShipSprite = Instantiate(redPrefab);
                hitShipSprite.transform.position = new Vector3(column, y);
                spawnedSpots.Add(hitShipSprite);
            }
        }
        //UpdateDisplay();
    }
    // check positions for the x rows
    public void CheckPositionX(int row)
    {
        if (!isEmpty(row,col)) return;

        // use grid[row, column]==value to check status

        for (var x = 0; x < width; x++) // checks the row for the specific button
        {
            if (grid[x, row] == 2) // if it's empty, fill with grey
            {
                var noShipSprite = Instantiate(greyPrefab);
                noShipSprite.transform.position = new Vector3(x,row);
                spawnedSpots.Add(noShipSprite);
            }

            if (grid[x, row] == 1) // if it isn't fill the space with the ship button
                {
                    var hitShipSprite = Instantiate(redPrefab);
                    hitShipSprite.transform.position = new Vector3(x, row);
                    spawnedSpots.Add(hitShipSprite);
                }
        }
    }
    
    
    }

