using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// 开始界面UI管理.
/// </summary>
public class StartUIManager : MonoBehaviour {

	private GameObject m_StartPanel;
	private GameObject m_SettingsPanel;

	private GameObject button_Setting;
	private GameObject button_Close;
	private GameObject button_TapStart;

	void Start () {
		m_StartPanel = GameObject.Find ("StartPanel");
		m_SettingsPanel = GameObject.Find ("SettingsPanel");

		button_Setting = GameObject.Find ("Setting");
		button_Close = GameObject.Find ("Close");
		button_TapStart = GameObject.Find ("TapStart");

		m_SettingsPanel.SetActive (false);

		UIEventListener.Get (button_Setting).onClick = Setting;
		UIEventListener.Get (button_Close).onClick = Close;
		UIEventListener.Get (button_TapStart).onClick = TapStart;

	}

	/// <summary>
	/// 按钮被点击时.
	/// </summary>
	/// <param name="go">Go.</param>
	private void Setting(GameObject go)
	{
		if (!m_SettingsPanel.activeSelf) 
		{
			m_SettingsPanel.SetActive (true);
			Debug.Log ("isClick");
		}
	}

	/// <summary>
	/// 关闭Setting界面.
	/// </summary>
	/// <param name="go">Go.</param>
	private void Close(GameObject go)
	{
		
		m_SettingsPanel.SetActive (false);
	//	Debug.Log ("isClosing");

	}

	/// <summary>
	/// 点击开始游戏,跳转到Game界面
	/// </summary>
	/// <param name="go">Go.</param>
	private void TapStart(GameObject go)
	{
		SceneManager.LoadScene("Game");
	}

	/// <summary>
	/// 设置开始按钮的状态.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void Set_button_TapStart(int state)
	{
		if(state==1)
			button_TapStart.SetActive (true);
		else
			button_TapStart.SetActive (false);
	}
}
