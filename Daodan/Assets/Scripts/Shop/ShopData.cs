using System.Collections;
using System.Collections.Generic;//引入
using System.Xml;//引入xml命名空间.

/// <summary>
/// 商城功能模块数据操作.
/// </summary>
public class ShopData  {


	//用于存储XML数据的实体集合.
	public List<ShopItem> shopList = new List<ShopItem> ();

	/// <summary>
	///商品的购买状态集合. 
	/// </summary>
	public List<int> shopState=new List<int>();

	public int GoldCount = 0;
	public int HeightCount=0;

	/// <summary>
	/// 通过路径读取XML商城文件.
	/// </summary>
	/// <param name="path">xml的参数.</param>
	public void ReadXmlByPath(string path)
	{
		XmlDocument doc = new XmlDocument();
		doc.Load (path);
		XmlNode root= doc.SelectSingleNode ("Shop");
		XmlNodeList nodelist = root.ChildNodes;

		foreach (XmlNode node in nodelist) 
		{	
			string speed = node.ChildNodes [0].InnerText;
			string rotate = node.ChildNodes [1].InnerText;
			string model = node.ChildNodes [2].InnerText;
			string price = node.ChildNodes [3].InnerText;
			string id = node.ChildNodes [4].InnerText;
            string prefab = node.ChildNodes[5].InnerText;
			//<1>遍历完毕直接打印输出
			//string info = string.Format ("speed:{0},rotate:{1},model:{2},price:{3}", speed, rotate, model, price);
			//Debug.Log (info);

			//<2>遍历完毕后，存储到List实体集合中.
			ShopItem item = new ShopItem(speed,rotate,model,price,id,prefab);
			shopList.Add (item);
		}
	}


	/// <summary>
	/// 读取金币和最高分数以及购买状态.
	/// </summary>
	public void ReadScoreAndGold(string path)
	{

		XmlDocument doc = new XmlDocument();
		doc.Load (path);
		XmlNode root= doc.SelectSingleNode ("SaveData");
		XmlNodeList nodelist = root.ChildNodes;

		GoldCount= int.Parse(nodelist [0].InnerText);
		HeightCount=int.Parse(nodelist [1].InnerText);

		//获取商品的购买状态
		for (int i = 2; i < 6; i++) 
		{
			shopState.Add (int.Parse (nodelist [i].InnerText));
		}
	}

	/// <summary>
	/// 更新XML文档内内容.
	/// </summary>
	/// <param name="path">Path.</param>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	public void UpdataXMLData(string path,string key ,string value)
	{

		XmlDocument doc = new XmlDocument();
		doc.Load (path);
		XmlNode root= doc.SelectSingleNode ("SaveData");
		XmlNodeList nodelist = root.ChildNodes;

		foreach (XmlNode node in nodelist)
		{
			if (node.Name == key) 
			{
				node.InnerText = value;
				doc.Save (path);
			}
		}

	}

	/// <summary>
	/// 测试函数.
	/// </summary>
	//private void DebugInfo()
//	{
	//	for (int i = 0; i < shopList.Count; i++) 
	//	{
			//string info = string.Format ("speed:{0},rotate:{1},model:{2},price:{3}", speed, rotate, model, price);

		//	Debug.Log (shopList[i].ToString());
	//	}
//	}
}
