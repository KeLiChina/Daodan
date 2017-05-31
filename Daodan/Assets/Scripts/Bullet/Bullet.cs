using UnityEngine;
using System.Collections;
/// <summary>
/// 子弹脚本
/// </summary>
public class Bullet : MonoBehaviour {

    private float speed = 800;
    private float lifeTime = 0.4f;
    public LayerMask Enemy;
    private Transform m_Transform;
    private Transform track_Transform;


	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        track_Transform = m_Transform.FindChild("Track").GetComponent<Transform>();
        Destroy(gameObject, lifeTime);
	}
	
	
	void Update () {
        DestroyEnemy();
        BulletChange();
	}

    /// <summary>
    /// 射线检测，若成功，销毁Enemy
    /// </summary>
    private void DestroyEnemy()
    {
        //-------声明射线-------
        RaycastHit hit;
        //如果射线检测生效
        if (Physics.Raycast(m_Transform.position, m_Transform.forward, out hit, speed + Time.deltaTime, Enemy))
        {
            Debug.Log(hit.transform.name + "ppppp");
            //销毁导弹
            hit.transform.GetComponent<Missile>().MissileDestroy();
        }
    }

    /// <summary>
    /// 子弹向前飞去及形变
    /// </summary>
    private void BulletChange()
    {
        //-------子弹向前飞行----------------------
        m_Transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //-----------子弹变形，视觉效果---------------
        if (track_Transform.localScale.z < 50)
            track_Transform.localScale += Vector3.forward * speed * Time.deltaTime;
    }
}
