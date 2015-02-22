using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{
	public static PlayerInfo instance = null;

	public UILabel hpLabel;
	public UILabel atkLabel;
	public UILabel coinLabel;
	public UILabel expLabel;
	public UILabel dexLabel;
	public UILabel luckLabel;
	public UILabel lvLabel;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);	
		DontDestroyOnLoad(gameObject);
	}
	
	public void UpdateInfo()
	{
		hpLabel.text = "HP: "+GameManager.instance.nowHP.ToString()+"/"+GameManager.instance.maxHP.ToString();
		atkLabel.text = "ATK: "+GameManager.instance.basicAtk.ToString()+"(+"+GameManager.instance.weaponAtk.ToString()+")";
		coinLabel.text = "Coin: "+GameManager.instance.nowCoins.ToString()+"/"+GameManager.instance.maxCoins.ToString();
		expLabel.text = "Exp: "+GameManager.instance.nowExp.ToString()+"/"+GameManager.instance.maxExp.ToString();
		lvLabel.text = "Lv: "+GameManager.instance.level.ToString();
	}
}
