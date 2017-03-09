using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {
	int[,] map;
	int width;
	int height;
	int xPos;
	int zPos;
	Vector3 spawnPos;
	int castCount = 0;
	bool goalHit = false;

	int drawing;
	int drawingPos;

	public List<int> pathToDraw;
	List<int> posToDraw;

	public GameObject point;

	Rigidbody errorGuy;

	bool cast(int dir, int xP, int zP, List<Object> curPath){
		bool one = true;
		bool two = true;
		//pathToDraw.Add (dir);
		castCount++;
			if (castCount < 100) {
				//
				//Debug.Log ("casting");
				//Debug.Log (dir);
				int check = 1;
				int x = xP;
				int z = zP;

			if (dir == 0) {
				z++;
			} else if (dir == 1) {
				x--;
			} else if (dir == 2) {
				z--;
			} else if (dir == 3) {
				x++;
			} else {
				check = -1;
				Debug.Log ("Error wrong direction");
			}
				int c = 0;
				while (check > 0 && c < 100) {
					//Debug.Log (x);
					//Debug.Log (z);
					if (x < width && x > 0 && z < height && z > 0) {
						if (map [x, z] == 0) {
							if (dir == 0) {
								z++;
							} else if (dir == 1) {
								x--;
							} else if (dir == 2) {
								z--;
							} else if (dir == 3) {
								x++;
							}
						} else if (map [x, z] == 200) {
							check = -2;
							if (dir == 0) {
								z--;
							} else if (dir == 1) {
								x++;
							} else if (dir == 2) {
								z++;
							} else if (dir == 3) {
								x--;
							}

						} else if (map [x, z] == 100) {
							check = -3;
						goalHit = true;
						Debug.Log ("gooooal");

							//break;
							//Destroy (gameObject);
							//done = true;
						} else {
							check = -1;
						Debug.Log ("what is this " + map [x, z]); 
							//deletePath (curPath);
							//break;
						}
					} else {
						//Debug.Log ("hit edge");
						//Debug.Log (z);
						//Debug.Log (x);
						//check = -1;
						//deletePath (curPath);
						check = -1;
					}

					c++;
				}

				//Debug.Log (check);
				//if (check < -1) {
					//drawCast (dir, xP, zP, curPath);
			if (check == -2) {
					//Debug.Log ("poop");
					//allDirections (dir, x, z, curPath);
				if (dir == 0 || dir == 2) {
					one = cast (1, x, z, curPath);
					two = cast (3, x, z, curPath);
				} else if (dir == 1 || dir == 3) {
					one = cast (0, x, z, curPath);
					two = cast (2, x, z, curPath);
				}
			} else if (check > -2){
				one = false;
				two = false;

			}

				//}


			}

		if (one == false && two == false) {
			return false;
		} else {
			//Debug.Log ("added " + dir);
			pathToDraw.Add (dir);
			posToDraw.Add (xP);
			posToDraw.Add (zP);
			return true;

		}

	}

	void drawCast(int dir, int xP, int zP, List<Object> curPath){
			//Debug.Log("drawing");
			//Debug.Log (xP);
		////Debug.Log (zP);
		//Debug.Log ("drawing " + dir);
			int check = 1;
			int x = xP;
			int z = zP;
		if (dir == 0) {
			z++;
		} else if (dir == 1) {
			x--;
		} else if (dir == 2) {
			z--;
		} else if (dir == 3) {
			x++;
		} else {
			check = -1;
			Debug.Log ("Error drawing wrong direction");
		}

		int count = 0;
		while (check > 0 && count < 1000) {
			//Debug.Log (z);
				if (x < width && x > 0 && z < height && z > 0) {
				//Debug.Log (map [x, z]);
					if (map [x, z] == 0) {
						spawnPos = new Vector3 (x, 0, z);
						Instantiate (point, spawnPos, gameObject.transform.rotation);
						if (dir == 0) {
						//Debug.Log ("poo");
							z++;
						} else if (dir == 1) {
							x--;
						} else if (dir == 2) {
							z--;
						} else if (dir == 3) {
							x++;
						}
					} else if (map [x, z] == 200) {
						check = -2;
						//Debug.Log (x);
						//Debug.Log (z);
						if (dir == 0) {
							z--;
						} else if (dir == 1) {
							x++;
						} else if (dir == 2) {
							z++;
						} else if (dir == 3) {
							x--;
						}
					} else if (map [x, z] == 100) {
						//done = true;
					if (dir == 0) {
						z--;
					} else if (dir == 1) {
						x++;
					} else if (dir == 2) {
						z++;
					} else if (dir == 3) {
						x--;
					}
						check = -1;
						//break;
					} else {
						//deletePath (curPath);
						check = -1;
						//break;
					}
				} else {
					//deletePath (curPath);
					check = -1;
				}

			count++;
			}
		//Debug.Log (check);
		if (drawing > 0) {
			//Debug.Log ("count is " + (pathToDraw.Count - 1));
			//int next = pathToDraw [pathToDraw.Count - 1];
			//pathToDraw.Remove (pathToDraw [pathToDraw.Count - 1]);

			//int next = pathToDraw [0];
			//pathToDraw.Remove (pathToDraw [0]);
			//Debug.Log("drawing " + next);
			drawing--;
			drawingPos -= 2;
			drawCast (pathToDraw[drawing], posToDraw[drawingPos -1], posToDraw[drawingPos], curPath);
		}
	}


	public void GetMap(int[,] colMap, int mWidth, int mHeight){
		map = colMap;

		width = mWidth;
		height = mHeight;
		xPos = (int)transform.position.x;
		zPos = (int)transform.position.z;
		pathToDraw = new List<int> ();
		posToDraw = new List<int> ();

		//Debug.Log (xPos);
		//Debug.Log (zPos);


		List<Object> pathNorth = new List<Object> ();
		cast (0, xPos, zPos, pathNorth);
		List<Object> pathWest = new List<Object> ();
		cast (1, xPos, zPos, pathWest);
		List<Object> pathSouth = new List<Object> ();
		cast (2, xPos, zPos, pathSouth);
		List<Object> pathEast = new List<Object> ();
		cast (3, xPos, zPos, pathEast);

		//pathToDraw [pathToDraw.Count] = 0;

		if (pathToDraw.Count > 0 && goalHit) {
			//Debug.Log ("path is long " + pathToDraw.Count);
			//errorGuy.AddForce (new Vector3 (0, 0, 0));
			//int next = pathToDraw [pathToDraw.Count - 1];
			//pathToDraw.Remove (pathToDraw [pathToDraw.Count - 1]);
			drawing = pathToDraw.Count - 1;
			drawingPos = posToDraw.Count - 1;
			drawCast (pathToDraw [drawing], posToDraw[drawingPos -1], posToDraw[drawingPos], pathEast);
		} else {
			Debug.Log ("ERROR NO PATH");
		}
			
	}

	void deletePath(List<Object> path){
		for (int i = 0; i < path.Count; i++) {
			Destroy (path[i]);
		}
	}
		
}
