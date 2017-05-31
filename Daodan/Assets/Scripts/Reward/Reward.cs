using UnityEngine;
using System.Collections;

/// <summary>
///控制奖励物品旋转.
/// </summary>
public class Reward : MonoBehaviour {

	private Transform m_Transform;

	private RewardManager m_RewardManager;

	void Start () {
		m_Transform = gameObject.transform;
		m_RewardManager = GameObject.Find ("RewardManager").GetComponent<RewardManager> ();
	}
	

	void Update () {
		m_Transform.Rotate (Vector3.left);
	}


	void OnDestroy()
	{
		Debug.Log ("I AM DEAD!");
		SendMessageUpwards ("RewardCountDown");//给父物体发送消息执行“RewardCountDown”方法.

	}
}
