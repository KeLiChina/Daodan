using UnityEngine;
using System.Collections;
using System.Collections.Generic;//引入集合命名空间.
/// <summary>
/// 商城模块总控制器.
/// </summary>
public class ShopManager : MonoBehaviour {

	private StartUIManager m_StartUIManager;//应用StartUIManager.

	private int index=0;//要展现的商品UI的索引.

	private ShopData m_Shopdata;
	//xml商城路径
	private string xmlPath="Assets/Datas/ShopData.xml";
	//xml存档路径
	private string saveDataPath="Assets/Datas/SaveData.xml";

	//商城模板
	private GameObject ui_ShopItem;

	private GameObject m_LeftButton;
	private GameObject m_RightButton;

	private UILabel starNum;
	private UILabel scountNum;
	//商品UI的集合.
	private List<GameObject>shopUI=new List<GameObject>();

	void Start () {

	//	Debug.Log ("----StartHeightScore:" + PlayerPrefs.GetInt ("heightScore") + "-----------");

		m_LeftButton = GameObject.Find ("Shop_Left");
		m_RightButton = GameObject.Find ("Shop_Right");
		//实例化商城数据对象.
		m_Shopdata = new ShopData ();
		//读取Xml文件.
		m_Shopdata.ReadXmlByPath(xmlPath);
		m_Shopdata.ReadScoreAndGold (saveDataPath);
		//shoplist
		Debug.Log(m_Shopdata.GoldCount);
		Debug.Log (m_Shopdata.HeightCount);

		//Debug商品状态.
		for (int i = 0; i < m_Shopdata.shopState.Count; i++) 
		{
			Debug.Log (m_Shopdata.shopState [i]);

		}
		//加载ShopItem.
		ui_ShopItem=Resources.Load<GameObject>("UI/ShopItem");

		m_StartUIManager = GameObject.Find ("UI Root").GetComponent<StartUIManager> ();

		CreatAllShopUI ();

		UIEventListener.Get (m_LeftButton).onClick = IsLeftClick;
		UIEventListener.Get (m_RightButton).onClick = IsRightClick;
		//--------------------同步UI与XML中的数据-----------------------------------
		starNum = GameObject.Find ("Star/Star_Num").GetComponent<UILabel> ();
		scountNum = GameObject.Find ("Score/Score_Num").GetComponent<UILabel> ();
		//--------------读取PlayerPrefs中的新的最高分-------------------------------
		int tempHeightScore = PlayerPrefs.GetInt("heightScore",0);
		int gold = PlayerPrefs.GetInt("gold",0);
		Debug.Log ("----------GetIntGold:" + gold + "---------");
		if (tempHeightScore > m_Shopdata.HeightCount)
		{
			//-------更新UI---------
			UpdataUIHeightScore (tempHeightScore);
			Debug.Log("----------tempHeightScore--------"+tempHeightScore);
			Debug.Log ("----------HeightCount--------" + m_Shopdata.HeightCount);
			//更新XML，存储新的最高分
			m_Shopdata.UpdataXMLData(saveDataPath,"HeightScore",tempHeightScore.ToString());

		} else {
			//-------更新UI---------
			UpdataUIHeightScore (m_Shopdata.HeightCount);
		}
		//------------更新最高金币---------------------------
		UpdataUIHeightGold (gold);
		m_Shopdata.UpdataXMLData(saveDataPath,"GoldCount",starNum.text);
		//-----------更新完毕清除内存中保存的上局的金币-----------
		PlayerPrefs.SetInt ("gold", 0);
		Debug.Log ("----------!GoldCount--------" + PlayerPrefs.GetInt("gold",0));
        //----------初始角色选择-------------------------
		SetPlayerInfo (m_Shopdata.shopList [0]);

	}

	/// <summary>
	///更新UI面板最高分分数。
	/// </summary>
	private void UpdataUIHeightScore(int heightScore)
	{
		
		scountNum.text = heightScore.ToString ();
	}
	/// <summary>
	///更新UI面板最高分金币分数。
	/// </summary>
	private void UpdataUIHeightGold(int gold)
	{

		starNum.text =( m_Shopdata.GoldCount + gold) .ToString ();
	}


	/// <summary>
	///更新UI面板的金币（Star）和分数.
	/// </summary>
	private void UpdataUI()
	{
		starNum.text = m_Shopdata.GoldCount.ToString ();
		scountNum.text = m_Shopdata.HeightCount.ToString ();
	}

	/// <summary>
	/// 创建商城模块中所有商品.
	/// </summary>
	private void CreatAllShopUI()
	{
		for (int i = 0; i < m_Shopdata.shopList.Count; i++) {

			//实例化单个商品.
			GameObject item = NGUITools.AddChild (gameObject, ui_ShopItem);

			GameObject model = Resources.Load<GameObject> (m_Shopdata.shopList[i].Model) ;
			//给单个商品赋值.
			item.GetComponent<ShopItemUI> ().SetUIValue (m_Shopdata.shopList [i].Speed, m_Shopdata.shopList [i].Rotate, m_Shopdata.shopList [i].Price,model,m_Shopdata.shopState[i],m_Shopdata.shopList[i].Id);
			//将生成的UI添加到集合中
			shopUI.Add (item);
		}
		ShopUIHadeAndShow (index);
	}

	/// <summary>
	/// 商城点击左.
	/// </summary>
	/// <returns><c>true</c> if this instance is left click the specified go; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	private void IsLeftClick(GameObject go)
	{
		//Debug.Log("Lift");
		if (index != 0) 
		{
			index--;

			int state = m_Shopdata.shopState [index];

			m_StartUIManager.Set_button_TapStart (state);//设置开始按钮隐藏

			ShopUIHadeAndShow (index);

			SetPlayerInfo (m_Shopdata.shopList [index]);
		}
	}
	/// <summary>
	/// 商城点击右.
	/// </summary>

	private void IsRightClick(GameObject go)
	{
		//Debug.Log("Right");
		if (index != 3) 
		{
			index++;

			int state = m_Shopdata.shopState [index];

			m_StartUIManager.Set_button_TapStart (state);//设置开始按钮隐藏

			ShopUIHadeAndShow (index);

			SetPlayerInfo (m_Shopdata.shopList [index]);
		}
	}

	/// <summary>
	/// 商城UI的显示和隐藏.
	/// </summary>
	private void ShopUIHadeAndShow(int index)
	{
		for(int i=0;i<shopUI.Count;i++)
		{
			shopUI [i].SetActive (false);
		}
		shopUI [index].SetActive (true);
	}


	/// <summary>
	/// 计算商品价格.
	/// </summary>
	private void CalcItemPrice(ShopItemUI item)
	{
		if(m_Shopdata.GoldCount>=item.itemPrice)
		{
			//Debug.Log ("购买成功");
			//隐藏购买按钮
			item.BuyEnd();
            m_StartUIManager.Set_button_TapStart(1);
			m_Shopdata.GoldCount -= item.itemPrice;//减去消耗的金币
			UpdataUI();

         

			m_Shopdata.UpdataXMLData (saveDataPath, "GoldCount", m_Shopdata.GoldCount.ToString());//更新XML中金币的数量
			m_Shopdata.UpdataXMLData (saveDataPath, "ID"+item.itemId, "1");//更新XML中商品购买状态（是否已经购买）.

           

          
		}
		else
		{
			//Debug.Log ("购买失败");
		}
	}


	/// <summary>
	/// 存储当前角色信息到PlayerPrefas.
	/// </summary>

	private void SetPlayerInfo(ShopItem item)
	{   
        float speed = float.Parse(item.Speed);
        float rotate = float.Parse(item.Rotate);
		PlayerPrefs.SetString ("PlayerName", item.Prefab);
    
        PlayerPrefs.SetFloat("PlayerSpeed", speed);
        PlayerPrefs.SetFloat("PlayerRotate", rotate);
	//	PlayerPrefs.SetInt ("PlayerSpeed", int.Parse (item.Speed));
	//	PlayerPrefs.SetInt ("PlayerRotate", int.Parse (item.Rotate));
      //  Debug.Log("===================");
	}

}
