using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public bool orientation = false;
	//private DeviceOrientation lastOrientation;
	private DeviceOrientation currentOrientation;
	public Camera cam;
	public GameObject player;


	public bool moved = false;
	// startposition von der die drehung und kameraverschiebung ausgeht
	public Vector3 startPosition;


	// Use this for initialization
	void Start () {
		
		//lastOrientation = Input.deviceOrientation;
		currentOrientation = Input.deviceOrientation;

		//beim start der scene wird anfangsort festgestellt.

		//To-DO: GO auf ActiveCharacter setten
		player = ActiveCharacter.activeCharacter;
		startPosition = ActiveCharacter.activeCharacter.transform.position;
		//startPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
		//Debug.Log (startPosition.x + " "+ startPosition.y + " "+ startPosition.z + " " );

	}


	// Update is called once per frame
	void Update () {

		DeviceOrientation devOr = Input.deviceOrientation;


			

		//Wenn das erfüllt ist darf gedreht werden
		if (devOr != DeviceOrientation.Unknown && orientation == true && devOr != currentOrientation)
		{

			float x = 0;
			float y = 0;

			if (devOr == DeviceOrientation.Portrait)
			{				
				if(currentOrientation == DeviceOrientation.LandscapeRight)
				{
					cam.transform.Rotate(new Vector3(0,0,90));
					//cam.transform.position = (new Vector3(0,0,-10));

					// wenn player zu weit nach links
					if (player.transform.position.x < -12.0f) {

						x = -12;
						y = 0 + (player.transform.position.y - startPosition.y);

						//cam.transform.position = new Vector3 (0 + -4, 
						//	0 + (player.transform.position.y - startPosition.y),
						//	cam.transform.position.z);
					
					} 
					// wenn active player zu weit nach rechts
					else if (player.transform.position.x > 12.0f) {

						x = 12;
						y = 0;

						//cam.transform.position = new Vector3 (0 + 4, 
						//	0 + (player.transform.position.y - startPosition.y),
						//	cam.transform.position.z);

					}
					//player is between borders?! 
					else
					{

						x = 0 + (player.transform.position.x - startPosition.x); 
						y = 0 + (player.transform.position.y - startPosition.y);

					}

					if (player.transform.position.y < -12.0f) {

						//x = 0;
						y = -12;

					} 
					// wenn active player zu weit nach oben
					else if (player.transform.position.y > 12.0f) {

						//x = 0;
						y = 12;
					}
					//player is between borders?! 
					else
					{
						//x = 0;
						y = -7.0f + (player.transform.position.y - startPosition.y);


					}

					cam.transform.position = new Vector3 (x, y, cam.transform.position.z);
				}

				currentOrientation = DeviceOrientation.Portrait;
				
				orientation = false;
				
			}
			else if (devOr == DeviceOrientation.LandscapeRight ) 
			{
				if(currentOrientation == DeviceOrientation.Portrait)
				{
					cam.transform.Rotate(new Vector3(0,0,-90));
					//cam.transform.position = (new Vector3(0,-4,-10));

					//cam.transform.position = new Vector3(	0, 
					//	-4.0f +(player.transform.position.y - startPosition.y),
					//	cam.transform.position.z	);

					// wenn active char zu weit nach unten
					if (player.transform.position.y < -7.0f) {

						x = 0;
						y = -7;

					//	cam.transform.position = new Vector3 (0, 
					//		-4,
					//		cam.transform.position.z);

					} 
					// wenn active player zu weit nach oben
					else if (player.transform.position.y > 7.0f) {

						x = 0;
						y = 7;

						//cam.transform.position = new Vector3 (0 , 
						//	4,
						//	cam.transform.position.z);

					}
					//player is between borders?! 
					else
					{
						x = 0;
						y = -7.0f + (player.transform.position.y - startPosition.y);


					}



					// X position noch abfragen, weil landscape auch möglich dass camera nach rechts und links moved!
					if (player.transform.position.x < -7.0f) {
						x = -7;
					} else if (player.transform.position.x > 7.0f) {
						x = 7;
					} else {
						x = 0 + (player.transform.position.x - startPosition.x);
					}

					/*
					if (player.transform.position.x < -11.0f) {
						x = -11;
						//y = 0 + (player.transform.position.y - startPosition.y);
					} 
					// wenn active player zu weit nach rechts
					else if (player.transform.position.x > 11.0f) {
						x = 11;
						//y = 0;
					}
					//player is between borders?! 
					else
					{
						x = 0 + (player.transform.position.x - startPosition.x); 
						//y = 0 + (player.transform.position.y - startPosition.y);
					}

					if (player.transform.position.y < -7.0f) {

						//x = 0;
						y = -7;

					} 
					// wenn active player zu weit nach oben
					else if (player.transform.position.y > 7.0f) {

						//x = 0;
						y = 7;
					}
					//player is between borders?! 
					else
					{
						//x = 0;
						y = -7.0f + (player.transform.position.y - startPosition.y);


					}
					*/
				
					cam.transform.position = new Vector3 (x, y, cam.transform.position.z);
				}

				currentOrientation = DeviceOrientation.LandscapeRight;
				
				orientation = false;
				
			}



		} // endif
		else if( devOr != DeviceOrientation.Unknown && devOr != currentOrientation && devOr != DeviceOrientation.PortraitUpsideDown && devOr != DeviceOrientation.LandscapeLeft)
		{
			orientation = true;
		}


		if(currentOrientation == DeviceOrientation.LandscapeRight)
		{

		
			float x = 0;
			float y = 0;

			// wenn active char zu weit nach unten
			if (player.transform.position.y < -7.0f) {

				x = 0;
				y = -7;

			} 
			// wenn active player zu weit nach oben
			else if (player.transform.position.y > 7.0f) {

				x = 0;
				y = 7;



			}
			//player is between borders?! 
			else
			{
				x = 0;
				//y auch hier -7
				y = -7.0f + (player.transform.position.y - startPosition.y);


			}
		

			if (player.transform.position.y < -7.0f) {

				//x = 0;
				y = -7;

			} 
			// wenn active player zu weit nach oben
			else if (player.transform.position.y > 7.0f) {

				//x = 0;
				y = 7;
			}
			//player is between borders?! 
			else
			{
				//y war hier immer -7
				y = -7.0f + (player.transform.position.y - startPosition.y);


			}


			// X position noch abfragen, weil landscape auch möglich dass camera nach rechts und links moved!
			if (player.transform.position.x < -7.0f) {
				x = -7;
			} else if (player.transform.position.x > 7.0f) {
				x = 7;
			} else {
				x = 0 + (player.transform.position.x - startPosition.x);
			}

			cam.transform.position = new Vector3 (x, y, cam.transform.position.z);

		}
		else if(currentOrientation == DeviceOrientation.Portrait)
		{


			float x = 0;
			float y = 0;


			if (player.transform.position.y < -7.0f) {
				x = 0;
				y = 0;
			} 
			// wenn active player zu weit nach oben
			else if (player.transform.position.y > 7.0f) {
				x = 0;
				y = 0;
			}
			//player is between borders?! 
			else
			{
				x = 0;
				//y = -7.0f + (player.transform.position.y - startPosition.y);
				y = 0;
			}



			if (player.transform.position.x < -11.0f) {
				x = -11;
				//y = 0 + (player.transform.position.y - startPosition.y);
			} 
			// wenn active player zu weit nach rechts
			else if (player.transform.position.x > 11.0f) {
				x = 11;
				//y = 0;
			}
			//player is between borders?! 
			else
			{
				x = 0 + (player.transform.position.x - startPosition.x); 
				//y = 0 + (player.transform.position.y - startPosition.y);
			}



			cam.transform.position = new Vector3 (x, y, cam.transform.position.z);

		}



		//Screen.orientation = ScreenOrientation.Landscape;
		//		Debug.Log (DeviceOrientation);

		/*DeviceOrientation orientation = Input.deviceOrientation;
		if (orientation == DeviceOrientation.LandscapeLeft)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}
		else if (orientation == DeviceOrientation.LandscapeRight)
		{
			Screen.orientation = ScreenOrientation.LandscapeRight;
		}
*/

		//!!!! Gibt die aktuelle Orientierung des Screens zurück
		//Debug.Log (Input.deviceOrientation);

		// AUsrichtung des Tablets
		//Debug.Log ("X:" + Input.acceleration.x + " " + "Y:" + Input.acceleration.y + " " + "Z:" + Input.acceleration.z);

		//TOUCHPOSITION 
		//if(Input.touchCount > 0)
		//	Debug.Log (Input.GetTouch(0).position.x + "   " +  Input.GetTouch(0).position.y);
	}



}
