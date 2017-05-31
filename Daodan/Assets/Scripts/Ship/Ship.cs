using UnityEngine;
using System.Collections;

/// <summary>
/// 控制飞船移动.
/// </summary>
public class Ship : MonoBehaviour {

	public GameObject ptc_Smoke;

    private GameObject Bullet;
	private Transform m_Transform;
    private Transform bulletCreatPoint1;
    private Transform bulletCreatPoint2;

	private MissileManager m_MissileManager;

	private GameUIManager m_GameUIManager;

	private int rewardNum = 0;//本局获得多少奖励物品.

	private bool isleft=false;
	private bool isright=false;
    private bool isfire = false;

	private bool isDeath=false;

    private float shotTime = 0.13f;
    private float myShotTime = 0;

    private AudioSource myAS;
   // public AudioClip ShotClip;
	//--------------定义飞船的速度和角速度----------------------
	private float speed;
	private float rotate;
	/// <summary>
	/// 封装飞船的速度.
	/// </summary>
	public float Speed
	{
		get{ return speed; }
		set{ speed = value; }
	}
	/// <summary>
	/// 封装飞船的角速度.
	/// </summary>
	public float Rotate
	{
		get{ return rotate; }
		set{ rotate = value; }
	}
	//---------------------------------------------------------
	public bool Isleft
	{
		get { return isleft;}
		set { isleft = value;}
	}
	public bool IsRight
	{
		get { return isright;}
		set { isright = value;}
	}
    public bool IsFire
    {
        get { return isfire; }
        set { isfire = value; }
    }

	void Start () {

		m_Transform = gameObject.transform;
        bulletCreatPoint1 = m_Transform.FindChild("BulletCreatPoint1");
        bulletCreatPoint2 = m_Transform.FindChild("BulletCreatPoint2");
        Bullet = Resources.Load<GameObject>("Bullet");
		m_MissileManager = GameObject.Find ("MissileManager").GetComponent<MissileManager> ();
		m_GameUIManager = GameObject.Find ("UI Root").GetComponent<GameUIManager> ();

		ptc_Smoke = Resources.Load<GameObject> ("Smoke03");
	}
	

	void Update () {
        if (!isDeath)
        {
            Move();
          //  if(Input.GetMouseButton(0))
            Shoot();
        }
        
	}
	/// <summary>
	/// 飞机移动及旋转.
	/// </summary>
	private void Move()
	{
		m_Transform.Translate (Vector3.forward*speed);
  
		if (isleft)
			m_Transform.Rotate (Vector3.up*-1*rotate);
		if (isright)
			m_Transform.Rotate (Vector3.up*1*rotate);

	}
	/// <summary>
	/// 检测飞机的碰撞.
	/// </summary>
	/// <param name="coll">Coll.</param>
	private void OnTriggerEnter(Collider coll)
	{
		//和边界碰撞.
		if (coll.tag == "Broder") 
		{
			isDeath = true;
			m_GameUIManager.ShowOverPanel ();
			m_GameUIManager.FadeButtonControl ();
		}
		//和导弹碰撞.
		if (coll.tag == "Missile") 
		{
			isDeath = true;
			Instantiate (ptc_Smoke, gameObject.transform.position, Quaternion.identity);
			m_GameUIManager.ShowOverPanel ();
			m_GameUIManager.FadeButtonControl ();
			m_MissileManager.StopCreat ();
			Destroy (coll.gameObject);
			gameObject.SetActive (false);

		}
		if (coll.tag == "Reward") 
		{
			rewardNum++;
			m_GameUIManager.UpDataStarNum (rewardNum);
			GameObject.Destroy (coll.gameObject);
			Debug.Log ("NUM:"+rewardNum);

		}
			
	}

    /// <summary>
    /// 生成子弹并射击
    /// </summary>
    public void Shoot()
    {
        if (isfire)
        {
            Debug.Log("asdasdasdasdasd");

            if (myShotTime < shotTime)
            {
                myShotTime += Time.deltaTime;
            }
            if (myShotTime >= shotTime)
            {
                Instantiate(Bullet, bulletCreatPoint1.position, bulletCreatPoint1.rotation);
                Instantiate(Bullet, bulletCreatPoint2.position, bulletCreatPoint2.rotation);

                //       myAS.PlayOneShot(ShotClip);
                myShotTime -= shotTime;
            }

        }
    }
}
