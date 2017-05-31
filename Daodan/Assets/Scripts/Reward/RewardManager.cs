using UnityEngine;
using System.Collections;

/// <summary>
/// 管理所有奖励物品.
/// </summary>
public class RewardManager : MonoBehaviour {

	private GameObject prefab_Reward;
	private Transform m_Transform;

	private int rewardcount = 0;//当前场景存在多少奖励物品.
	private int rewardmax = 18;//当前场景最多存在多少奖励物品.


	void Start () {
		prefab_Reward=Resources.Load<GameObject> ("reward");
		m_Transform = gameObject.transform;

		InvokeRepeating ("CreatReward", 1, 1f);
	}
	

	/// <summary>
	/// 生成奖励物品.
	/// </summary>
	private void CreatReward()
	{
		if (rewardcount < rewardmax) 
		{
			Vector3 pos = new Vector3(Random.Range(-980,980),0,Random.Range(-980,980));
			Instantiate (prefab_Reward, pos, Quaternion.identity, m_Transform);
			rewardcount++;
			Debug.Log ("Count:" + rewardcount);
		}
	}

	/// <summary>
	/// 停止生成奖励物品.
	/// </summary>
	public void StopCreat()
	{
		CancelInvoke ();
	}

	/// <summary>
	/// 一个奖励物品被销毁.
	/// </summary>
	public void RewardCountDown()
	{
		
		rewardcount--;
		Debug.Log ("死了一个");

	}
}
