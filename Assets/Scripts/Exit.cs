using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour 
{
	public float restartLevelDelay = 1f;
	public GameObject locker;

	public void Unlock()
	{
		locker.GetComponent<SpriteRenderer>().enabled = false;
	}

	void OnMouseDown()
	{
		if(locker.GetComponent<SpriteRenderer>().enabled == false)
		{
			Invoke("Restart", restartLevelDelay);
		}
		else if(GameManager.instance.isGotKey == true)
		{
			Unlock();
		}
		else
		{
			// cant pass
			// ....
		}
	}

	//Restart reloads the scene when called.
	private void Restart()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game.
		Application.LoadLevel(Application.loadedLevel);
	}
}
