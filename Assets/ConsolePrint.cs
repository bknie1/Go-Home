﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*  
 *  Notes:  
 *      - Vector2 is a built in vector with two values.
 *    
 */
//--------------------------------------------------------------------
public class ConsolePrint : MonoBehaviour {
	protected Vector2 location = new Vector2(0.0f, 0.0f);
	protected Vector2 home = new Vector2(0.0f, 0.0f);
	protected Vector2 path_to_home;
	public Input key;
	protected int moves;
	//----------------------------------------------------------------
	// Use this for initialization. Runs once at start.
	void Start() {
		moves = 0;
		Randomize();
		Calculate_Distance();

		print("\tGO HOME");
		print("You are lost in the woods.\n" +
			"Can you find your way home before the encroaching " + 
			"darkness consumes you?");
		//Print_Location(); // DEBUG
		//Print_Home(); // DEBUG
	}
	//----------------------------------------------------------------
	void Randomize() {
		location = new Vector2(Random.Range(0, 10),  // Loc X
							   Random.Range(0, 10)); // Loc Y

		home = new Vector2(Random.Range(0, 10),      // Home X
						   Random.Range(0, 10));     // Home Y
		Calculate_Distance();
		if (path_to_home.magnitude < 4) Randomize();
	}
	//----------------------------------------------------------------
	void Calculate_Distance() {
		path_to_home = home - location;
	}
	//----------------------------------------------------------------
	void Check_Progress() {
		// Win Condition
		if (location.magnitude == home.magnitude) {
			print("Congratulations, you've made it home in " + moves
				+ " moves.");
		}
		// E/W Tree
		else if (location.y == home.y) {
			if (location.x > home.x) {
				print("You need to go west.");
			}
			else {
				print("You need to go east.");
			}
		}
		// N/S Tree
		else if (location.x == home.x) {
			if (location.y > home.y) {
				print("You need to go south.");
			}
			else {
				print("You need to go north.");
			}
		}
		else {
			// NE
			if (location.x > home.x && location.y > home.y) {
				print("You need to go southwest.");
			}
			// SE
			else if (location.x > home.x && location.y < home.y) {
				print("You need to go northwest.");
			}
			// SW
			else if (location.x < home.x && location.y < home.y) {
				print("You need to go northeast.");
			}
			// NW - Could be else, but it's nice to see the logic.
			else if (location.x < home.x && location.y > home.y) {
				print("You need to go southeast.");
			}
		}
	}
	//----------------------------------------------------------------
	void Print_Location() {
		print("Location: " + location);
	}
	//----------------------------------------------------------------
	void Print_Distance() {
		print("Distance (M): " + path_to_home.magnitude);
	}
	//----------------------------------------------------------------
	void Print_Home() {
		print("Home: " + home);
	}
	//----------------------------------------------------------------
	//----------------------------------------------------------------
	// Update is called once per frame. Continuous.
	void Update() {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			print("Moving west.");
			location.x -= 1;
			Update_Player();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			print("Moving east.");
			location.x += 1;
			Update_Player();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			print("Moving north.");
			location.y += 1;
			Update_Player();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			print("Moving south.");
			location.y -= 1;
			Update_Player();
		}
	}
	//--------------------------------------------------------------------
	// Informs the player of his/her progress.
	void Update_Player() {
		Calculate_Distance();
		// Print_Distance(); // Easy mode.
		++moves;
		Check_Progress();
	}
}
//--------------------------------------------------------------------