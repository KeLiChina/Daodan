
/// <summary>
/// 商城物品Item实体类.
/// </summary>
public class ShopItem  {
	private string speed;
	private string rotate;
	private string model;
	private string price;
	private string id;
    private string prefab;

	public ShopItem(string speed,string rotate,string model,string price,string id,string prefab)
	{
		this.speed = speed;
		this.rotate = rotate;
		this.model = model;
		this.price = price;
		this.id = id;
        this.prefab = prefab;
	}

	public string Id
	{
		get{ return id;}
		set{ id = value;}
	}

	public string Speed
	{
		get{ return speed;}
		set{ speed = value;}
	}
	public string Rotate
	{
		get{ return rotate;}
		set{ rotate = value;}
	}
	public string Model
	{
		get{ return model;}
		set{ model = value;}
	}
	public string Price
	{
		get{ return price;}
		set{ price = value;}
	}
    public string Prefab
    {
        get { return prefab; }
        set { prefab = value; }
    }

	public override string ToString ()
	{
		return string.Format ("speed:{0},rotate:{1},model:{2},price:{3}", speed, rotate, model, price);
	}
}
