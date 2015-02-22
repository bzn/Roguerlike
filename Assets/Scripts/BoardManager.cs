using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count
	{
		public int minimum; 			//Minimum value for our Count class.
		public int maximum; 			//Maximum value for our Count class.
		
		
		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
	
	
	public int columns = 6; 										//Number of columns in our game board.
	public int rows = 6;											//Number of rows in our game board.
	private Count wallCount = new Count (5, 9);						//Lower and upper limit for our random number of walls per level.
	private Count foodCount = new Count (1, 5);						//Lower and upper limit for our random number of food items per level.
	public GameObject exit;											//Prefab to spawn for exit.
	public GameObject[] floorTiles;									//Array of floor prefabs.
	public GameObject[] wallTiles;									//Array of wall prefabs.
	public GameObject[] foodTiles;									//Array of food prefabs.
	public GameObject[] enemyTiles;									//Array of enemy prefabs.
	public GameObject[] outerWallTiles;								//Array of outer tile prefabs.
	public GameObject[] itemCoinTiles;
	public GameObject[] itemHealTiles;
	public GameObject itemKeyTile;

	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List<Vector3>();	//A list of possible locations to place tiles.		
	private List<GameObject> wallList = new List<GameObject>();
	//private List<GameObject> enemyList = new List<GameObject>();
	public static BoardManager instance = null;

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			
			//if not, set instance to this
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this)
			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);	
		
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}

	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear();
		wallList.Clear();
		//enemyList.Clear();
		
		//Loop through x axis (columns).
		for(int x = 0; x < columns; x++)
		{
			//Within each column, loop through y axis (rows).
			for(int y = 0; y < rows; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3(x, y, 0f));

				GameObject item = null;
				GameManager.instance.itemList.Add(item);
			}
		}
	}
	
	
	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;
		
		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for(int x = 0; x < columns; x++)
		{
			//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for(int y = 0; y < rows; y++)
			{
				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

				/*
				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				if(x == -1 || x == columns || y == -1 || y == rows)
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
				*/

				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance =
					Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				
				//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
				instance.transform.SetParent (boardHolder);
			}
		}
	}
	
	
	//RandomPosition returns a random position from our list gridPositions.
	Vector3 RandomPosition ()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (0, gridPositions.Count);
		
		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions[randomIndex];
		
		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt (randomIndex);
		
		//Return the randomly selected Vector3 position.
		return randomPosition;
	}

	void LayoutObjectAtAll(GameObject[] tileArray)
	{
		int objectCount = columns * rows;

		for(int i = 0; i < rows; i++)
		{
			for(int j = 0; j < columns; j++)
			{
				Vector3 position = new Vector3(j, i);
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				GameObject instance = Instantiate(tileChoice, position, Quaternion.identity) as GameObject;
				instance.GetComponent<Wall>().pos = i * columns + j;
				wallList.Add(instance);
			}
		}
	}

	void LayoutObjectAtRandom(GameObject tile)
	{
		Vector3 randomPosition = RandomPosition();
		GameObject item = Instantiate(tile, randomPosition, Quaternion.identity) as GameObject;
		item.SetActive(false);

		int pos = (int)(randomPosition.y * columns + randomPosition.x);

		GameManager.instance.itemList[pos] = item;
	}

	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectsAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range (minimum, maximum+1);
		
		//Instantiate objects until the randomly chosen limit objectCount is reached
		for(int i = 0; i < objectCount; i++)
		{
			//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
			Vector3 randomPosition = RandomPosition();
			
			//Choose a random tile from tileArray and assign it to tileChoice
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			
			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
			GameObject item = Instantiate(tileChoice, randomPosition, Quaternion.identity) as GameObject;
			item.SetActive(false);
			
			int pos = (int)(randomPosition.y * columns + randomPosition.x);

			GameManager.instance.itemList[pos] = item;

			if(tileChoice.name == "Enemy1" || tileChoice.name == "Enemy2")
			{
				GameManager.instance.enemyList.Add(item);
				item.GetComponent<Enemy>().pos = pos;
			}
			else if(tileChoice.name == "ItemCoin" || tileChoice.name == "ItemGold")
			{
				GameManager.instance.itemCoinList.Add(item);
			}
			else if(tileChoice.name == "ItemHeal")
			{
				GameManager.instance.itemHealList.Add(item);
			}
		}
	}

	void LayoutKeyAtRandomEnemy(GameObject tile)
	{
		GameObject enemyChoice = GameManager.instance.enemyList[Random.Range(0,GameManager.instance.enemyList.Count)];
		GameObject item = Instantiate(tile, enemyChoice.transform.position, Quaternion.identity) as GameObject;
		item.SetActive(false);
		enemyChoice.GetComponent<Enemy>().nextItem = item;
	}
	
	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene(int level)
	{
		//Creates the outer walls and floor.
		BoardSetup();
		
		//Reset our list of gridpositions.
		InitialiseList();
		
		//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
		LayoutObjectAtAll(wallTiles);

		LayoutObjectAtRandom(exit);

		LayoutObjectsAtRandom(itemCoinTiles, 3, 6);

		LayoutObjectsAtRandom(itemHealTiles, 1, 3);

		//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
		//LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		
		//Determine number of enemies based on current level number, based on a logarithmic progression
		//int enemyCount = (int)Mathf.Log(level, 2f);
		int enemyCount = Random.Range(3,6);
		
		//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
		LayoutObjectsAtRandom(enemyTiles, enemyCount, enemyCount);

		LayoutKeyAtRandomEnemy(itemKeyTile);
	}
}

