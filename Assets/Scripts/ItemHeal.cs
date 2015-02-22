using UnityEngine;
using System.Collections;

public class ItemHeal : MonoBehaviour 
{
	void OnMouseDown()
	{
		Debug.Log("Item Heal");
		if(GameManager.instance.gameState == GameManager.GameState.PlayersTurn)
		{
			GameManager.instance.PlayerTurnEnd();
			gameObject.SetActive(false);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHP(10);
		}
		/*
		GetComponentInParent<SpriteRenderer>().enabled = false;
		GetComponentInParent<BoxCollider2D>().enabled = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHP(10);
		*/
	}
}
