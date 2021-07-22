using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpUIMove : MonoBehaviour
{
	// [Range(-2000f, 2000f)]
	public float TargetPositionLeft = -450f;
	// [Range(-2000f, 2000f)]
	public float TargetPositionTop = 1020f;
	// [Range(0.001f, 1000f)]
	public float Speed = 1.0f;
	public float closeEnough;

	private Vector2 targetPosition;
	private Vector2 currentPosition;
	private RectTransform objTrans;
	private float distanceToTarget;

	public string collectibleType;

	void Start ()
	{
		//Get a reference to the UI object
		objTrans = GetComponent<RectTransform> ();

		//Get its current position
		currentPosition = objTrans.anchoredPosition;

		//Get a reference to where we want it to go
		GameObject collectibleItemIconInBag = GameObject.FindGameObjectWithTag(collectibleType);
		
		if (collectibleItemIconInBag != null)
        {
			GameObject canvas = GameObject.FindGameObjectWithTag("UI");
			Vector2 offset = new Vector2(0, - canvas.transform.position.y);

			targetPosition = (Vector2) collectibleItemIconInBag.GetComponent<RectTransform>().position + offset;
		}
		else
        {
			targetPosition = new Vector2(TargetPositionLeft, TargetPositionTop);
		}

		//Run the UpdateTotal, which updates the UI Text for the number of this resource
		// UpdateTotal ();
	}

	// Update is called once per frame
	void Update ()
	{
		//Get a position that is a little bit closer to our goal position
		objTrans.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, Speed * Time.deltaTime);

		//Set our object to that new position
		currentPosition = objTrans.anchoredPosition;

		//How far are we from our goal?
		distanceToTarget = Vector2.Distance (currentPosition, targetPosition);
	}

	void LateUpdate()
	{
		//If we are close enough and we want the icon to disappear...
		if (distanceToTarget < closeEnough)
		{
			//Bonus: Make the default icon animate as the new resource is brought in
			//Swell the resource icon

			//Destroy the object, we are done with it!
			Destroy (gameObject);
		}
	}

}
