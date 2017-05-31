using UnityEngine;
using System.Collections;


/// <summary>
/// 飞机生成管理器.
/// </summary>
public class ShipManager : MonoBehaviour {

	private GameObject model;
	private GameObject player;

	void Start () {
//		Debug.Log(.GetString("PlayerName"));
		//--------------读取角色信息-----------------------
		string ship =PlayerPrefs.GetString ("PlayerName");
		float speed = PlayerPrefs.GetFloat ("PlayerSpeed");
		float rotate = PlayerPrefs.GetFloat ("PlayerRotate");
		//--------------动态加载模型，实例化，角色-----------
		model = Resources.Load <GameObject>(ship);

		player = GameObject.Instantiate (model, Vector3.zero, Quaternion.identity) as GameObject ;
		//---------------给角色添加组件，设置属性--------------
		Ship m_Ship= player.AddComponent<Ship> ();
        player.tag = "Player";
		m_Ship.Speed = speed;
		m_Ship.Rotate = rotate;
		Debug.Log ("-------m_speed:" + speed + "------------");
		Debug.Log ("-------m_rotate:" + rotate + "------------");
		//---------------调整第四个飞船的大小--------------------------------------------------
		//if (player.name == "Ship_4(Clone)")
	//		player.GetComponent<Transform> ().localScale = new Vector3 (1.2f, 1.2f, 1.2f);
	//	else
	//		player.GetComponent<Transform> ().localScale = new Vector3 (2.7f, 2.7f, 2.7f);
	}
	

}
