using UnityEngine;
using System.Collections;


/// <summary>
/// 控制导弹旋转.
/// </summary>
public class Missile : MonoBehaviour {

	public GameObject ptc_Smoke;

	private Transform m_Transform;
	private Transform ship_Transform;

	private Vector3 normalForward=Vector3.forward;
	void Start () {
		m_Transform = gameObject.GetComponent<Transform> ();
		ship_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	

	void Update () {
		m_Transform.Translate (Vector3.forward*2.1f);
		//得到飞机和导弹的方向。
		Vector3 dir = ship_Transform.position - m_Transform.position;
		//插值计算导弹要旋转的角度。
		normalForward = Vector3.Lerp (normalForward, dir, Time.deltaTime);
		//四元数的lookrotation改变当前导弹朝向。
		m_Transform.rotation=Quaternion.LookRotation(normalForward);
	}

	private void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Missile") 
		{
            MissileDestroy();
		}
	}

    public void MissileDestroy()
    {
        Instantiate(ptc_Smoke, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
