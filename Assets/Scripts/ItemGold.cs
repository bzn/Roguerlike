using UnityEngine;
using System.Collections;

public class ItemGold : MonoBehaviour 
{
	void OnMouseDown()
	{
		Debug.Log("Item Gold");
		if(GameManager.instance.gameState == GameManager.GameState.PlayersTurn)
		{
			GameManager.instance.PlayerTurnEnd();
			gameObject.SetActive(false);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddCoins(5);
		}
		/*
		this.GetComponentInParent<SpriteRenderer>().enabled = false;
		GetComponentInParent<BoxCollider2D>().enabled = false;

		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddCoins(5);
		*/
	}
}
