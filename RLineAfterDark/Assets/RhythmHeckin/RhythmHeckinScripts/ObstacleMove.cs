using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObstacleMove : MonoBehaviour {

	public float speed;
	
	//This had a 90 degree turn so that it -hopefully- accentuates the beat a bit more

	public Vector3 initLocation;
	//public Vector3 dropLocation;
	//public Vector3 endLocation;

	//public float beatsToDropLocation = 1f;
	//public float beatsToEndLocation = 1f;
	
	public Vector3 destination;

	private Vector3 direction;

	public Transform[] stops;

	public int currentStop;

	public float pointThreshold;

	public float numberOfBeats;

	bool songStarted;

	public float timeRemaining;
	public float distanceRemaining;
	public float nextGridTime;

	Vector3 priorVelocity;
	Vector3 currentVelocity;

	public Transform trainSprite;
	
	
	
	void Start () {

		
		
	}
	
	// Update is called once per frame
	void Update () {

        if (songStarted)
        {
			//timeRemaining = nextGridTime - (RhythmHeckinWwiseSync.GetMusicTimeInMS()/1000);

			//Vector3 checkVector = destination - transform.position;

			//distanceRemaining = Mathf.Abs(checkVector.magnitude);

			//speed = distanceRemaining / timeRemaining;

			currentVelocity = direction * speed;
			transform.Translate(currentVelocity * Time.deltaTime);
			//priorVelocity = Vector3.Lerp(priorVelocity, currentVelocity, 0.5f);

			//change directions when we are within 0.05 units of the target
			//only do this when the obstacle is traveling down

		}
		
		
	}

	void ChangeDirections(Vector3 newDestination, float beatsToNewDestination) {
		direction = newDestination - transform.position;
		speed = direction.magnitude / (beatsToNewDestination * (RhythmHeckinWwiseSync.secondsPerBeat));
		
		//normalize gives the vector a magnitude of 1
		direction.Normalize();

	}

	public void StartMoving()
    {
		currentStop = 0;

		initLocation = transform.position;
		destination = stops[(currentStop + 1) % stops.Length].position;
		direction = destination - initLocation;
		float distance = direction.magnitude;

		

		nextGridTime = (RhythmHeckinWwiseSync.secondsPerBeat * numberOfBeats) + (RhythmHeckinWwiseSync.GetMusicTimeInMS()/1000);

		direction.Normalize();
		float travelTime = (RhythmHeckinWwiseSync.secondsPerBeat) * numberOfBeats;
		priorVelocity = direction / travelTime;

		speed = distance / travelTime;

		songStarted = true;
	}

	public void UpdateStop()
    {
		Debug.Log("Updating Stop");
		currentStop += 1;
		if (currentStop >= stops.Length)
		{
			currentStop = 0;
		}
		trainSprite.eulerAngles = SetRotation(currentStop);
		//transform.position = stops[currentStop].position;
		destination = stops[(currentStop + 1) % stops.Length].position;
		ChangeDirections(destination, numberOfBeats);
		nextGridTime = (RhythmHeckinWwiseSync.secondsPerBeat * numberOfBeats) + (RhythmHeckinWwiseSync.GetMusicTimeInMS() / 1000);
	}

	Vector3 SetRotation(int stop)
    {
		Vector3 newRotation;
		float zRotation = 0;

		switch (stop)
        {
			case 0:
				zRotation = 0f;
				break;
			case 1:
				zRotation = 0f;
				break;
			case 2:
				zRotation = 0f;
				break;
			case 3:
				zRotation = 135f;
				break;
			case 4:
				zRotation = 90f;
				break;
			case 5:
				zRotation = 90f;
				break;
			case 6:
				zRotation = 90f;
				break;
			case 7:
				zRotation = 45;
				break;
			case 8:
				zRotation = 0f;
				break;
			case 9:
				zRotation = 0f;
				break;
			case 10:
				zRotation = 0f;
				break;
			case 11:
				zRotation = 135;
				break;
			case 12:
				zRotation = 90f;
				break;
			case 13:
				zRotation = 90f;
				break;
			case 14:
				zRotation = 90f;
				break;
			case 15:
				zRotation = 45;
				break;
		}

		newRotation = new Vector3(0, 0, zRotation);
		return newRotation;
	}
}
