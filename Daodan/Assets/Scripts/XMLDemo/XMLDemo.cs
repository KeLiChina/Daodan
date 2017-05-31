using UnityEngine;
using System.Collections;
using System.Xml;//引入xml操作相关命名空间.

/// <summary>
/// XML操作演示.
/// </summary>
public class XMLDemo : MonoBehaviour {

	//定义一个字段存储xml路径；
	private string xmlPath="Assets/Datas/web.xml";

	void Start () {
		ReadXMLByPath (xmlPath);
	}

	/// <summary>
	/// 使用路径读取xml中的数据进行显示.
	/// </summary>
	/// <param name="path">Path.</param>
	private void ReadXMLByPath(string path)
	{
		//<1>实力化一个XML文档操作对象.
		XmlDocument doc = new XmlDocument();

		//<2>使用XML对象加载XML.
		doc.Load(path);

		//<3>获取根节点.
		XmlNode root = doc.SelectSingleNode("Web");

		//<4>获取根节点下所有的子节点.
		XmlNodeList nodelist= root.ChildNodes;

		//<5>遍历输出.
		foreach(XmlNode node in nodelist)
		{	//string转换成int,取得属性.
			int id = int.Parse (node.Attributes ["id"].Value);
			//取得文本.
			string name = node.ChildNodes [0].InnerText;
			string url = node.ChildNodes [1].InnerText;

			Debug.Log (id + "--" + name + "--" + url);
		}
	}

}
