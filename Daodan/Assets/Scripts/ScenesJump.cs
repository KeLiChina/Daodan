using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景跳转.
/// </summary>
public class ScenesJump : MonoBehaviour {



	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			SceneManager.LoadScene("02_1") ;
	
	}
}
