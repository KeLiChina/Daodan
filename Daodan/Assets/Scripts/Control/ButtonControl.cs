using UnityEngine;
using System.Collections;


/// <summary>
/// 按钮方式控制.
/// </summary>
public class ButtonControl : MonoBehaviour {

	private Ship m_Ship;

	private GameObject m_Left;
	private GameObject m_Right;
    private GameObject m_Fire;

	void Start () {
		m_Ship = GameObject.FindWithTag ("Player").GetComponent<Ship> ();
        m_Fire = GameObject.Find("Fire");
		m_Left = GameObject.Find ("Left");
		m_Right = GameObject.Find ("Right");

		UIEventListener.Get (m_Left).onPress = LeftPress;
		UIEventListener.Get (m_Right).onPress = RightPress;
        UIEventListener.Get(m_Fire).onPress = FirePress;
	}

	/// <summary>
	/// 按下Left按钮时，飞机向左旋转.
	/// </summary>
	/// <returns><c>true</c> if this instance is left the specified go isleft; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	/// <param name="isleft">If set to <c>true</c> isleft.</param>
	private void LeftPress(GameObject go,bool leftpress)
	{
		if (leftpress)
			m_Ship.Isleft = true;
		else
			m_Ship.Isleft = false;
	}
	/// <summary>
	/// 按下Right按钮时，飞机向右旋转.
	/// </summary>
	/// <returns><c>true</c> if this instance is left the specified go isleft; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	/// <param name="isleft">If set to <c>true</c> isleft.</param>
	private void RightPress(GameObject go,bool rightpress)
	{
		if (rightpress)
			m_Ship.IsRight = true;
		else
			m_Ship.IsRight = false;
	}

    /// <summary>
    /// 按下Fire按钮，飞机开火
    /// </summary>
    /// <param name="go"></param>
    /// <param name="firepress"></param>
    private void FirePress(GameObject go,bool firepress)
    {
        if (firepress)
        {

            m_Ship.IsFire = true;
        }
        else
        {
            m_Ship.IsFire = false;

        }
    }
}
