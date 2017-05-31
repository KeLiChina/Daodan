using UnityEngine;
using System.Collections;


/// <summary>
/// 商城Item(商品)控制器.
/// </summary>
public class ShopItemUI : MonoBehaviour {
	private GameObject ship_Parent;

	private Transform m_Transform;

	private UILabel ui_Speed;
	private UILabel ui_Rotate;
	private UILabel ui_Price;

	public int itemPrice;//商品价格
	public int itemId;//商品id编号

	private GameObject m_BuyButton;

	private GameObject itemState;//商品状态（是否显示购买按钮）.


	/// <summary>
	/// 这里的Awake()是原来的Start(),若不把它的级别提前，则在ShopManager脚本中的Start()中实例化Item时会报错.
	/// </summary>
	void Awake () {
		m_Transform = gameObject.transform;
		ship_Parent = m_Transform.FindChild ("Model_Ship").gameObject;

		itemState = m_Transform.FindChild ("BuyButton").gameObject;


		ui_Speed = m_Transform.FindChild ("Speed/SpeedNum").GetComponent<UILabel> ();
		ui_Rotate = m_Transform.FindChild ("Rotate/RotateNum").GetComponent<UILabel> ();
		ui_Price = m_Transform.FindChild ("BuyButton/BuyButton_Num").GetComponent<UILabel> ();

		m_BuyButton = m_Transform.FindChild ("BuyButton/BuyButton_bg").gameObject;
	}


	/// <summary>
	/// 给单个商品的UI赋值.
	/// </summary>
	/// <param name="speed">Speed.</param>
	/// <param name="rotate">Rotate.</param>
	/// <param name="price">Price.</param>
	public void SetUIValue(string speed,string rotate,string price,GameObject model,int state,string id)
	{
		//为单个商品元素添加属性.
		ui_Speed.text = "+"+speed;
		ui_Rotate.text = "+"+rotate;
		ui_Price.text= price;

		itemPrice = int.Parse (price);
		itemId = int.Parse (id);

		//判断商品状态（是否购买）
		if (state == 1) 
		{
			itemState.SetActive (false);

		}

		//添加商品模型，指定layer为NGUI.
		GameObject ship=NGUITools.AddChild (ship_Parent, model);

		Transform ship_Transform = ship.transform;

		ship.layer = 8;
		//指定商品Mesh的Layer为NGUI.
		ship_Transform.FindChild ("Mesh").gameObject.layer = 8;
		//设置飞机模型缩放位置信息.
		if(model.name!="Ship_4")
			ship_Transform.localScale=new Vector3(12,12,12);//缩放
		else 
			ship_Transform.localScale=new Vector3(5,5,5);//缩放
        if (model.name == "Ship_1")
            ship_Transform.localScale = new Vector3(6, 6, 6);//缩放
		ship_Transform.localPosition=new Vector3(0,-100,190);
        if (model.name == "Ship_3")
            ship_Transform.localPosition = new Vector3(0, -85, 190);
		ship_Transform.localRotation = Quaternion.Euler( new Vector3 (-90, 0, 0));

		UIEventListener.Get (m_BuyButton).onClick = IsBuyButtonClick;
	}

	/// <summary>
	/// 点击购买按钮.
	/// </summary>
	/// <returns><c>true</c> if this instance is buy button click the specified go; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	private void IsBuyButtonClick(GameObject go)
	{
		Debug.Log ("IsBuyButtonClick");
		SendMessageUpwards ("CalcItemPrice", this);
	}


	/// <summary>
	/// 购买成功.
	/// </summary>
	public void BuyEnd()
	{
		itemState.SetActive (false);

	}
   
}
