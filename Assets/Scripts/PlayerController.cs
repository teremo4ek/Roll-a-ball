using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float Speed;
	public GUIText Score;
	public GUIText GameOver;
	
	// Number of pickups collected.
	private int count;
	private int pickupTotal;
	
	void Start()
	{
		count = 0;
		
		// Find out how many pickups there are.
		var pickups = GameObject.FindGameObjectsWithTag("Pickup");
		if (pickups != null)
		{
			pickupTotal = pickups.Length;
		}
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = 0.0f;
		float moveVertical = 0.0f;
#if UNITY_EDITOR
		moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
#else
		moveHorizontal = Input.acceleration.x;
		moveVertical = Input.acceleration.y;
#endif
		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
		if (movement.magnitude > 1.0f)
		{
			movement.Normalize();
		}

		rigidbody.AddForce(movement * Speed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickup")
		{
			++count;
			UpdateText();
			other.gameObject.SetActive(false);
		}
	}
	
	void UpdateText()
	{
		Score.text = "Score: " + count.ToString();
		if (count == pickupTotal)
		{
			GameOver.text = "You won!";
		}
	}
}
