using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏界面UI控制.
/// </summary>
public class GameUIManager : MonoBehaviour {

	private GameObject m_GamePanel;
	private GameObject m_OverPanel;
	private GameObject m_Reset;
	private GameObject m_ButtonControl;
    

	private UILabel label_StarNum;
	private UILabel label_Time;
	private UILabel heightScore;
	private UILabel starNum;
	private UILabel timeNum;

	private int time=0;//计时

	/// <summary>
	/// 封装时间.
	/// </summary>
	/// <value>The time.</value>
	public int Time
	{
		get { return time;}
		set{ time = value;
			UpDataTimeNum (time);
		//	Debug.Log ("时间：" + Time);
		
		}
	}


	void Start () {

//		m_Ship = GameObject.FindGameObjectWithTag ("Player").GetComponent<Ship> ();
		m_Reset = GameObject.Find ("Reset");
		m_GamePanel = GameObject.Find ("GamePanel");
		m_OverPanel = GameObject.Find ("OverPanel");
		m_ButtonControl=GameObject.Find("ButtonControl");

		label_StarNum = GameObject.Find ("StarNum").GetComponent<UILabel>();
		label_Time = GameObject.Find ("Time1").GetComponent<UILabel>();
		heightScore = GameObject.Find ("Score/ScoreNum").GetComponent<UILabel>();
		starNum = GameObject.Find ("Star/OverStarNum").GetComponent<UILabel>();
		timeNum = GameObject.Find ("Time/TimeNum").GetComponent<UILabel>();


		label_Time.text = "0:1";

		m_OverPanel.SetActive (false);
		m_ButtonControl.SetActive (true);
		label_StarNum.text = "0 ";

		UIEventListener.Get (m_Reset).onClick = IsReset;

		StartCoroutine ("AddTime");
	}
	

	/// <summary>
	/// 更新UI面板Star分数.
	/// </summary>
	/// <param name="rewardNum">Reward number.</param>
	public void UpDataStarNum(int rewardNum)
	{
		label_StarNum.text = rewardNum+" ";

	}

	/// <summary>
	/// 更新UI面板时间
	/// </summary>
	/// <param name="rewardNum">Reward number.</param>
	public void UpDataTimeNum(int t)
	{
		if (t < 60)
			label_Time.text = "0:" + t;
		else 
		{
			label_Time.text = t / 60 + ":" + t % 10;

		}

	}



	/// <summary>
	/// 显示UI结束面板.
	/// </summary>
	public void ShowOverPanel()
	{
		m_OverPanel.SetActive (true);
		StopAddTime ();
		SetOverUIValue ();
	}

	/// <summary>
	///隐藏左右按钮.
	/// </summary>
	public void FadeButtonControl()
	{
		m_ButtonControl.SetActive (false);
	}

	/// <summary>
	/// 跳转到开始场景.
	/// </summary>
	/// <returns><c>true</c> if this instance is reset the specified go; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	private void IsReset(GameObject go)
	{
		SceneManager.LoadScene ("Start");
	}


	/// <summary>
	///协程计时器.
	/// </summary>
	/// <returns>The time.</returns>
	IEnumerator AddTime()
	{
		while (true) 
		{
			yield return new WaitForSeconds (1);
			Time++;
		//	Debug.Log ("Time:" + "--" + time);
		}
	}


	/// <summary>
	/// 停止计时.
	/// </summary>
	private void StopAddTime()
	{
		StopCoroutine ("AddTime");
	}

	/// <summary>
	/// 给结束面板UI赋值.
	/// </summary>
	private void SetOverUIValue()
	{
		int t = int.Parse (label_StarNum.text);

		starNum.text = t*10+"";
		timeNum.text = time.ToString();

		int tempHeightScore = t * 10 + time;

		heightScore.text = tempHeightScore.ToString ();

		PlayerPrefs.SetInt ("heightScore", tempHeightScore);
		PlayerPrefs.SetInt ("gold", t*10);

		int GetIntgold= PlayerPrefs.GetInt ("gold", 0);
		Debug.Log ("----------@GetIntGold:" + GetIntgold + "---------");
	}
}
