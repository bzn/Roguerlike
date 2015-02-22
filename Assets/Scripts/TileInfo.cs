using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour 
{
	public UILabel hpLabel;
	public UILabel atkLabel;
	public UILabel cdLabel;

	public void UpdateInfo(int hp, int atk, int cd)
	{
		hpLabel.text = "HP:"+hp.ToString();
		atkLabel.text = "AK:"+atk.ToString();
		cdLabel.text = "CD:"+cd.ToString();
	}
}
