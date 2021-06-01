using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//variables
	public float moveSpeed = 300;
	public GameObject character;

	private Rigidbody characterBody;
	private float ScreenWidth;

	private bool isMobile = false;


	// Use this for initialization
	void Start()
	{
		ScreenWidth = Screen.width;
		characterBody = character.GetComponent<Rigidbody>();
		
		if (Application.platform == RuntimePlatform.WebGLPlayer ||
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.OSXPlayer)
        {
			isMobile = false;
        } else if (Application.platform == RuntimePlatform.Android ||
			Application.platform == RuntimePlatform.IPhonePlayer)
        {
			isMobile = true;
        }
		// Default isMobile is set to false
	}

	// Update is called once per frame
	void Update()
	{
		/* Using mobile platform*/
		if (isMobile)
        {
			// Loop over every touch found
			int i = 0;
			while (i < Input.touchCount)
			{
				if (Input.GetTouch(i).position.x > ScreenWidth / 2)
				{
					//move right
					RunCharacter(1.0f);
				}
				if (Input.GetTouch(i).position.x < ScreenWidth / 2)
				{
					//move left
					RunCharacter(-1.0f);
				}
				++i;
			}
		}
		/* Using Unity Player or WebGL platform*/
		else
		{
			float inputDirection = Input.GetAxis("Horizontal");
			if (inputDirection >= -1 && inputDirection < 0)
            {
				RunCharacter(-1.0f);
			} else if (inputDirection > 0)
            {
				RunCharacter(1.0f);
            }
		}
	}
	void FixedUpdate()
	{
#if UNITY_EDITOR
		RunCharacter(Input.GetAxis("Horizontal"));
#endif
	}

	private void RunCharacter(float horizontalInput)
	{
		//move player
		characterBody.AddForce(new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0));

	}
}
