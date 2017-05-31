using UnityEngine;
using System.Collections;

/// <summary>
/// 摄像机跟随角色移动.
/// </summary>
public class CameraFollow : MonoBehaviour {

	private Transform m_Transform;
	private Transform ship_Transform;

	void Start () {
		m_Transform = gameObject.transform;
		ship_Transform = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	

	void Update () {
		m_Transform.position=ship_Transform.position+new Vector3(0,50,0);
	}
}
