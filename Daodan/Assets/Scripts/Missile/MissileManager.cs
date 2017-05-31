using UnityEngine;
using System.Collections;


/// <summary>
/// 导弹生成器管理
/// </summary>
public class MissileManager : MonoBehaviour {

	private Transform m_Transform;
	private Transform[] CreatPoints;
	private GameObject preafab_Missile_3;

	void Start () {
		preafab_Missile_3 = Resources.Load <GameObject>("Missile_3");
		m_Transform = gameObject.transform;
		CreatPoints = GameObject.Find ("CreatPoints").GetComponent<Transform> ().GetComponentsInChildren<Transform> ();

		//调用生成导弹方法.
		InvokeRepeating("CreatMissile",1,2.5f);
	}
	

	void Update () {
//		for (int i = 0; i < CreatPoints.Length; i++)
//			Debug.Log (CreatPoints [i].name);
	}

	/// <summary>
	/// 生成导弹的方法.
	/// </summary>
	private void CreatMissile()
	{
		int index = Random.Range (0, CreatPoints.Length);
		Instantiate (preafab_Missile_3, CreatPoints [index].position, Quaternion.identity, m_Transform);
       //----------导弹层级设为Enemy----------
        preafab_Missile_3.layer = 9;
	}

	/// <summary>
	/// 停止生产导弹.
	/// </summary>
	public void StopCreat()
	{
		CancelInvoke ();
	}
}
