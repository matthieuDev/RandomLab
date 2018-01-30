using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createRabdomLabPlus : MonoBehaviour {
	public GameObject wall;
	private float wallLength ;
	static private LabCell deadCell = new LabCell (-1, -1);
	public GameObject labObject;
	private int[,,] lab ;

	// Use this for initialization
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond;
		wallLength = wall.transform.localScale.x - wall.transform.localScale.z ;
		lab = createLabRandom(20,20); 


		createLabPhysique (lab,0);
	}


	/*int[] createStartFinish () {
		int xSize = lab.GetLength (0);
		int ySize = lab.GetLength (0);


		return {0,0};
	}*/

	int[,,] createLabRandom (int sizeX , int sizeY) {


		int[,,] lab = new int[sizeX,sizeY,4];
		LabCell[,] labCells = new LabCell[sizeX,sizeY];



		for (int i = 0; i < sizeX; i++) {
			for (int j = 0; j < sizeY; j++) {
				labCells [i, j] = new LabCell (i, j);
			}
		}

		WallValue[,] wallsId = new WallValue[sizeX+1,sizeY+1] ;

		for (int i = 0; i < sizeX+1; i++) {
			for (int j = 0; j < sizeY+1; j++) {
				wallsId [i, j] = null;
			}
		}

		WallValue firstValue = new WallValue (); 

		for (int i = 0; i < sizeX; i++) {
			for (int j = 0; j < sizeY; j++) {
				LabCell curCell = labCells [i, j] ;
				if (i != 0) {
					curCell.neigbours.Add (3, labCells [i - 1, j]);
				} else {
					wallsId [i, j] = firstValue;
					lab [i, j, 3] = 1;
				}
				if (i != sizeX-1) {
					curCell.neigbours.Add (1,labCells [i+1, j]);
				}else {
					wallsId [i+1, j] = firstValue;
					lab [i, j, 1] = 1;
				}
				if (j != 0) {
					curCell.neigbours.Add (2,labCells [i, j-1]);
				}else {
					wallsId [i, j] = firstValue;
					lab [i, j, 2] = 1;
				}
				if (j != sizeY-1) {
					curCell.neigbours.Add (0,labCells [i, j+1]);
				}else {
					wallsId [i, j+1] = firstValue;
					lab [i, j, 0] = 1;
				}
			}
		}

		int count = 0;
		Queue<LabCell> listRandomWalker = new  Queue<LabCell>();
		listRandomWalker.Enqueue(labCells [Random.Range(0,sizeX),Random.Range(0,sizeY)]);
		listRandomWalker.Enqueue(labCells [Random.Range(0,sizeX),Random.Range(0,sizeY)]);
		listRandomWalker.Enqueue(labCells [Random.Range(0,sizeX),Random.Range(0,sizeY)]);



		while (listRandomWalker.Count != 0) {
			LabCell randomWalker = listRandomWalker.Dequeue();
			//createLabPhysique (lab,count);
			count++;
			if (!randomWalker.equals (deadCell) && randomWalker.neigbours.Count != 0) {
				List<int> neigboursKeys;
				while (((int)Random.Range (0, 2)) != 1) {
					neigboursKeys = new List<int> (randomWalker.neigbours.Keys);
					int rand = (int)Random.Range (0, neigboursKeys.Count - 1);
					int moveKey = neigboursKeys [rand];
					randomWalker = randomWalker.neigbours [moveKey];
				}

				neigboursKeys = new List<int> (randomWalker.neigbours.Keys);

				int toDeletePos = Random.Range (0, neigboursKeys.Count);
				int toDeleteKey = neigboursKeys [toDeletePos];


				LabCell neighbour = randomWalker.neigbours [toDeleteKey];
				int oppositeKey = (toDeleteKey + 2) % 4;

				listRandomWalker.Enqueue( neighbour.delete (oppositeKey));
				listRandomWalker.Enqueue(randomWalker.delete (toDeleteKey));


				int rwx = randomWalker.x;
				int rwy = randomWalker.y;
				int idy = 0, idx = 0;



				WallValue [] wallValue = new WallValue [2] ;
				int [,] wallPos = {{rwx,rwy},{rwx+1,rwy+1}};

				if (toDeleteKey == 1 || toDeleteKey == 3) {

					idx = rwx + (-(toDeleteKey - 2) +1)/2;
					wallValue [0] = wallsId [idx, rwy];
					wallValue [1] = wallsId [idx, rwy+1];

					wallPos [0,0] = idx;
					wallPos [1,0] = idx;

				} else {
					idy = rwy + (-(toDeleteKey - 1)+1)/2;
					wallValue [0] = wallsId [rwx, idy];
					wallValue [1] = wallsId [rwx+1, idy];

					wallPos [0,1] = idy;
					wallPos [1,1] = idy;
				}
					
				string s = "";


				if (wallValue [0] == null && wallValue [1] == null) {
					
					lab [rwx, rwy, toDeleteKey] = 1;
					lab [neighbour.x, neighbour.y, oppositeKey] = 1;

					WallValue newValue = new WallValue ();
					wallsId [wallPos [0, 0], wallPos [0, 1]] = newValue;
					wallsId [wallPos [1, 0], wallPos [1, 1]] = newValue;


				} else if (wallValue [0] == null) {
					lab [rwx, rwy, toDeleteKey] = 1;
					lab [neighbour.x, neighbour.y, oppositeKey] = 1;

					wallsId [wallPos [0, 0], wallPos [0, 1]] =
						wallsId [wallPos [1, 0], wallPos [1, 1]];
				} else if (wallValue [1] == null) {
					lab [rwx, rwy, toDeleteKey] = 1;
					lab [neighbour.x, neighbour.y, oppositeKey] = 1;

					wallsId [wallPos [1, 0], wallPos [1, 1]] = 
						wallsId [wallPos [0, 0], wallPos [0, 1]];
				} else if (!wallValue [0].Equals (wallValue [1])) {
					lab [rwx, rwy, toDeleteKey] = 1;
					lab [neighbour.x, neighbour.y, oppositeKey] = 1;

					wallsId [wallPos [1, 0], wallPos [1, 1]].setValue( 
						wallsId [wallPos [0, 0], wallPos [0, 1]]);
				} else {

				}








			}
		}

		return lab;
	}

	private class WallValue {
		public int valueInt ;
		public WallValue valueWV;
		public bool isInt = true ;
		public static int id = 0;
		public WallValue () {
			valueInt = id ++;
		}

		public WallValue (int v) {
			valueInt =v;
		}

		public bool Equals (WallValue other) {
			return getValue () == other.getValue ();
		}

		public int getValue () {
			if (isInt) {
				return valueInt;
			} else {
				return valueWV.getValue();
			}
		}

		public WallValue getWVValue () {
			if (isInt) {
				return this;
			} else {
				return valueWV;
			}
		}

		public void setValue (WallValue newVal) {
			if (isInt)
				valueWV = newVal;
			else
				valueWV.setValue (newVal);
			isInt = false;
		}

	}

	private class LabCell {
		public int x , y ;
		public Dictionary<int,LabCell> neigbours  = new Dictionary<int,LabCell>();
		public LabCell (int x , int y ) {
			this.x = x;
			this.y=y;
		}
		public bool equals (LabCell other){
			return x ==other.x && y ==other.y;
		}

		public LabCell delete( int key){

			neigbours.Remove (key);
			int neighbourSize = neigbours.Count;


			if ( neighbourSize== 1) {
				int lastKey = new List<int> (neigbours.Keys)[0];
				LabCell lastneigh = neigbours[lastKey];
	
				neigbours.Clear();
				return lastneigh.delete ((lastKey+2)%4);
			} else if (neighbourSize == 0){
				print("end "+ x + "  " + y + "  " + key);
				return deadCell;
			}else{
				return this;
			}

		}
		public void printt () {
			print (x + " , " + y);
		}


	}

	private class Tuple {
		public int _1 ;
		public LabCell _2;
		public Tuple (int i , LabCell lc ) {
			_1=i;
			this._2=lc;
		}

	}


	void createLabPhysique (int[,,] labyrinth, int y ) {
		int xsize =labyrinth.GetLength(0);
		int ysize = labyrinth.GetLength(1);
		for (int i = 0; i < xsize; i++) {
			for (int j = 0;j < ysize; j++) {
				for (int z = 0; z < 4; z++) {
					if ( ( (i+j) %2 == 0 || i == 0 || j == 0 || i == xsize-1 || j == ysize-1 ) && labyrinth[i,j,z] == 1) {
						GameObject curWall =Instantiate(wall,new Vector3(i*wallLength + y * 5, 0, j*wallLength), Quaternion.identity);
						curWall.transform.Rotate (new Vector3(0, 90 * z, 0));
						curWall.transform.Translate (new Vector3(0,0, wallLength/2));

						curWall.transform.parent = labObject.transform;
					}

				}
			}
		}
	}
}
