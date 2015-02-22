using UnityEngine;
using System.Collections;

public class AllTileInfo : MonoBehaviour 
{
	public static AllTileInfo instance = null;

	public GameObject[] allTiles;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);	
		DontDestroyOnLoad(gameObject);
	}

	public void SetEnable(int pos, bool isEnable)
	{
		if(allTiles[pos])
		{
			allTiles[pos].SetActive(isEnable);
		}
	}
}
