using UnityEngine;
using System.Collections;

public class ItemKey : MonoBehaviour {

	void OnMouseDown()
	{
		Debug.Log("Item Key");
		if(GameManager.instance.gameState == GameManager.GameState.PlayersTurn)
		{
			GameManager.instance.PlayerTurnEnd();
			gameObject.SetActive(false);
			GameManager.instance.isGotKey = true;
		}
		/*
		GetComponentInParent<SpriteRenderer>().enabled = false;
		GetComponentInParent<BoxCollider2D>().enabled = false;
		GameManager.instance.isGotKey = true;
		*/
	}
}
