using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For Text
/*  
 *  Notes:  
 *      - Vector2 is a built in vector with two values.
 *    
 */
//--------------------------------------------------------------------
public class ConsolePrint : MonoBehaviour {
    protected Text text; // Our text component.
	protected Vector2 location = new Vector2(0.0f, 0.0f);
	protected Vector2 home = new Vector2(0.0f, 0.0f);
	protected Vector2 path_to_home;
	public Input key;
	protected int moves;

    //----------------------------------------------------------------
    IEnumerator Start_Game() {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
        text.text = "You are lost in the woods.\n" +
            "Can you find your way home before the encroaching " +
            "darkness consumes you? Use the arrows!";
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
    //----------------------------------------------------------------
    // Use this for initialization. Runs once at start.
    void Start() {
        text = GetComponent<Text>(); // Sets up the text reference.
		moves = 0;
		Randomize();
		Calculate_Distance();

		print("\tGO HOME");
		print("You are lost in the woods.\n" +
			"Can you find your way home before the encroaching " + 
			"darkness consumes you? Use the arrows!");

        StartCoroutine(Start_Game());
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
            text.text = "Congratulations, you've made it home in " + moves
                + " moves.";
            StartCoroutine(End_Game());
		}
		// E/W Tree
		else if (location.y == home.y) {
			if (location.x > home.x) {
				print("You need to go west.");
				text.text = "Go west.";
			}
			else {
				print("You need to go east.");
				text.text = "Go east.";
			}
		}
		// N/S Tree
		else if (location.x == home.x) {
			if (location.y > home.y) {
				print("You need to go south.");
				text.text = "Go south.";
			}
			else {
				print("You need to go north.");
				text.text = "Go north.";
			}
		}
		else {
			// NE
			if (location.x > home.x && location.y > home.y) {
				print("You need to go southwest.");
				text.text = "Go southwest.";
			}
			// SE
			else if (location.x > home.x && location.y < home.y) {
				print("You need to go northwest.");
				text.text = "Go northwest.";
			}
			// SW
			else if (location.x < home.x && location.y < home.y) {
				print("You need to go northeast.");
				text.text = "Go northeast.";
			}
			// NW - Could be else, but it's nice to see the logic.
			else if (location.x < home.x && location.y > home.y) {
				print("You need to go southeast.");
				text.text = "Go southeast.";
			}
		}
	}
	//----------------------------------------------------------------
	void Print_Location() {
		print("Location: " + location);
        text.text = "Location: " + location;
	}
	//----------------------------------------------------------------
	void Print_Distance() {
		print("Distance (M): " + path_to_home.magnitude);
    }
	//----------------------------------------------------------------
	void Print_Home() {
		print("Home: " + home);
        text.text = "Home : " + home;
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
		Print_Distance();
		++moves;
		Check_Progress();
	}
    //--------------------------------------------------------------------
    IEnumerator End_Game() {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
        Application.Quit();
    }
}
//--------------------------------------------------------------------