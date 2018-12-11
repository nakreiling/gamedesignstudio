using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //needed to switch scences

public class TileMap : MonoBehaviour
{

    //public GameObject[] selectedUnit;
    public List<GameObject> selectedUnit = new List<GameObject>();
    public TileType[] tileTypes;

    int[,] tiles;
    Node[,] graph;
    public int unitSelector;

    int mapSizeX = 20;
    int mapSizeY = 20;

    public static bool gameSessionActive = true; //Saying true for now the idea is that this only get changed ONCE per the entire game session
    private void Awake()
    {
        if (gameSessionActive)
        {
            float healthE = GameObject.FindWithTag("Enemy").GetComponent<Stats>().getHealth();
            UnitManager.EnemyBattleResultHealth = healthE; //initializing the initator
            Debug.Log("Game Start Enemy Health is: " + UnitManager.EnemyBattleResultHealth);
            float healthP = GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth();
            UnitManager.PlayerBattleResultHealth = healthP;
            Debug.Log("Game Start Player Health is: " + UnitManager.PlayerBattleResultHealth);  //I hate unity
            gameSessionActive = false; //idea is that it will be false until the game is shut down and restarted
        }
    }
    void Start()
    {
        //just for testing purposes to pull out the data of the test Skelly and Knights health after Battle has occured

        //float healthE = GameObject.FindWithTag("Enemy").GetComponent<Stats>().getHealth();
        //Issue only works with singular Knight and Skelly
        //Need a way to record which two units were actually fighting before restoring health


        if (UnitManager.tileMapUsed == false)
        {
            UnitManager.unitXList = new List<int>();
            UnitManager.unitYList = new List<int>();
            for (int k = 0; k < selectedUnit.Count; k++)
            {
                selectedUnit[k].GetComponent<Unit>().tileX = (int)selectedUnit[k].transform.position.x;
                selectedUnit[k].GetComponent<Unit>().tileY = (int)selectedUnit[k].transform.position.y;
                selectedUnit[k].GetComponent<Unit>().map = this;
                Debug.Log("Hello");
                UnitManager.tileMapUsed = true;
                UnitManager.unitXList.Add(selectedUnit[k].GetComponent<Unit>().tileX);
                UnitManager.unitYList.Add(selectedUnit[k].GetComponent<Unit>().tileY);
            }
        }
        else
        {
            Debug.Log("Instantiating units from static array.");
            Debug.Log("Length of selectedUnit " + selectedUnit.Count);
            Debug.Log("Length of unitXList " + UnitManager.unitXList.Count);
            for (int k = 0; k < UnitManager.unitXList.Count; k++)
            {
                selectedUnit[k].GetComponent<Unit>().tileX = UnitManager.unitXList[k];
                selectedUnit[k].GetComponent<Unit>().tileY = UnitManager.unitYList[k];
                selectedUnit[k].transform.position = this.TileCoordToWorldCoord(UnitManager.unitXList[k], UnitManager.unitYList[k]);
                selectedUnit[k].GetComponent<Unit>().map = this;
            }
        }
        GenerateMapData();
        GeneratePathFindingGraph();
        GenerateMapVisuals();
        unitSelector = 0;
        //issue: this is only for test purposes need to be able to set health based on last known position of any active unit

        //Reposition (Player) and alter health accordingly
        //need to put in a check here to SetActive to 'false' if health is at 0 or less i.e. if the unit should be dead remove it from play
       // GameObject.FindGameObjectWithTag("Enemy").transform.position = UnitManager.EnemyMapPosition;
        GameObject.FindWithTag("Enemy").GetComponent<Stats>().setHealth(UnitManager.EnemyBattleResultHealth); //we need a setter for the Units health that can set it based on what is from the Static (DB) class
        Debug.Log("Back on the Map, known health of Enemy is: " + UnitManager.EnemyBattleResultHealth);//Derpy?


        //Reposition units based on where they were before battle started (Player)
        //float healthP = GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth();
       // GameObject.FindGameObjectWithTag("Player").transform.position = UnitManager.PlayerMapPosition;
        GameObject.FindWithTag("Player").GetComponent<Stats>().setHealth(UnitManager.PlayerBattleResultHealth); //okay so if I did this correctly we will pass Health data (and more) from the two scenes
        Debug.Log("Back on the Map, known health of pdog is: " + UnitManager.PlayerBattleResultHealth);//RRREEEEE
    }

    void GenerateMapData()
    {
        // allocate them tiles brooo
        tiles = new int[mapSizeX, mapSizeY];

        // those grass tiles fool
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }

        // make that bitchass swamp broooo
        for (int x = 3; x <= 8; x++)
        {
            for (int y = 1; y < 19; y++)
            {
                tiles[x, y] = 1;
            }
        }

        // make another swamp
        /*for (int x = 10; x <= 16; x++) {
            for (int y = 15; y < 19; y++) {
                tiles[x, y] = 1;
            }
        }*/

        // add a river
        for (int x = 0; x < 20; x++)
        {
            for (int y = 9; y < 11; y++)
            {
                if (x != 7 && x != 17)
                    tiles[x, y] = 3;
            }
        }

        // diagonal mountain range below river
        tiles[11, 7] = 2;
        tiles[12, 7] = 2;
        tiles[12, 6] = 2;
        tiles[13, 6] = 2;
        tiles[13, 5] = 2;
        tiles[14, 5] = 2;
        tiles[14, 4] = 2;
        tiles[15, 4] = 2;
        tiles[15, 3] = 2;
        tiles[16, 3] = 2;
        tiles[16, 2] = 2;
        tiles[17, 2] = 2;
        tiles[17, 1] = 2;
        tiles[18, 1] = 2;

        // diagonal mountian range above river
        tiles[11, 12] = 2;
        tiles[12, 12] = 2;
        tiles[12, 13] = 2;
        tiles[13, 13] = 2;
        tiles[13, 14] = 2;
        tiles[14, 14] = 2;
        tiles[14, 15] = 2;
        tiles[15, 15] = 2;
        tiles[15, 16] = 2;
        tiles[16, 16] = 2;
        tiles[16, 17] = 2;
        tiles[17, 17] = 2;
        tiles[17, 18] = 2;
        tiles[18, 18] = 2;
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        if (sourceX != targetX && sourceY != targetY)
        {
            // fudge cost for tiebreaking
            cost += 0.001f;
        }

        return cost;
    }

    void GeneratePathFindingGraph()
    {
        // intialize array
        graph = new Node[mapSizeX, mapSizeY];

        // initialize node for each spot in array
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }


        // calculate neighbors for nodes
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {

                // 4-way connection version
                /*if (x > 0)
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                if (x < mapSizeX-1)
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY-1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);*/

                // 8-way connection version (allows diagonals)
                // Try left
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                // Try Right
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }

                // Try straight up and down
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);

                // This also works with 6-way hexes and n-way variable areas (like EU4)
            }
        }
    }

    void GenerateMapVisuals()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tity = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tity.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                TileClickHandler tch = go.GetComponent<TileClickHandler>();
                tch.tileX = x;
                tch.tileY = y;
                tch.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public bool UnitCanEnterTile(int x, int y)
    {
        // test unit's walk/fly/hover type against various
        // terrain flags to see if they can enter tile
        return tileTypes[tiles[x, y]].isWalkable;// && TileNotOccupied(x, y);
    }

    public void Attack()
    {
        int victim = EnemyInFront(unitSelector);
        if (victim != -1)
        {
            
            Debug.Log("Attacking enemy!");
            //Destroy(selectedUnit[victim]);
            //selectedUnit.RemoveAt(victim);
            //We will instead grab the information of the attacker unit and the attacked unit and pass it to the Battle Scene

            //issue we need to use the tags "Player" and "Enemey" instead because of situation where loaded health will be reversed
            //selectedUnit[victim].tag("Player");
            //So the issue is that is is looking at the "unit" (i.e. the parent) where are Toon and Skelly are the children of the units

            /*
            if (selectedUnit[victim].CompareTag("Player")) //only need the tag of the unit basically hoping this works 
            {
                Debug.Log("Victim Found, Killing muh Targeto");
            }
            else if (selectedUnit[unitSelector].CompareTag("Player"))
            {
                Debug.Log("This is not the Victim, you are looking for");
            }
            */
            //Now testing if statements to see if 1) They correctly identify the Enemy and player 2) Health is being loaded up correctly based on assumption of correct ID
            for (int k = 0; k < selectedUnit.Count; k++)
            {
                UnitManager.unitXList[k] = selectedUnit[k].GetComponent<Unit>().tileX;
                UnitManager.unitYList[k] = selectedUnit[k].GetComponent<Unit>().tileY;
                Debug.Log(UnitManager.unitXList[k] + ", " + UnitManager.unitYList[k]);
            }
            if (selectedUnit[victim].CompareTag("Enemy"))
            {
                float targetHealth = selectedUnit[victim].GetComponent<Unit>().GetComponentInChildren<Stats>().getHealth(); //unit being attacked
                UnitManager.EnemyBattleResultHealth = targetHealth;
                Vector3 enemyLastKnowPosition = selectedUnit[victim].transform.position;
                Quaternion enemyLookingLike = selectedUnit[victim].transform.rotation;
                UnitManager.EnemyMapPosition = enemyLastKnowPosition;//crap how do I get the transfrom from this exactly?
                UnitManager.EnemyRotation = enemyLookingLike; //
                Debug.Log("Enemy Coordinates from lvl 1: "+UnitManager.EnemyMapPosition);
            }
            else //assumption is that it is the player
            {
                float attackerHealth = selectedUnit[unitSelector].GetComponent<Unit>().GetComponentInChildren<Stats>().getHealth(); //unit starting the attack
                UnitManager.PlayerBattleResultHealth = attackerHealth;
                Vector3 playerLastKnowPosition = selectedUnit[unitSelector].transform.position;
                Quaternion playerLookingLike = selectedUnit[unitSelector].transform.rotation;
                Debug.Log("You gunnin' for dat player? I feel you dawg");
                UnitManager.PlayerMapPosition = playerLastKnowPosition;//crap how do I get the transfrom from this exactly?
                UnitManager.PlayerRotation = playerLookingLike; //
                Debug.Log("Lvl 1 Alt Player at:" + UnitManager.PlayerMapPosition);

                //Vector3 attackerPosition = selectedUnit[unitSelector] //how to get the Transform data?
                
            }

            if (selectedUnit[unitSelector].CompareTag("Player"))
            {
                float attackerHealth = selectedUnit[unitSelector].GetComponent<Unit>().GetComponentInChildren<Stats>().getHealth(); //unit starting the attack
                                                                                                     //Vector3 attackerPosition = selectedUnit[unitSelector] //how to get the Transform data?
                UnitManager.PlayerBattleResultHealth = attackerHealth;
                Vector3 playerLastKnowPosition = selectedUnit[unitSelector].transform.position;
                Quaternion playerLookingLike = selectedUnit[unitSelector].transform.rotation;
                Debug.Log("Yougunnin' for dat player? I feel you dawg");
                UnitManager.PlayerMapPosition = playerLastKnowPosition;//crap how do I get the transfrom from this exactly?
                UnitManager.PlayerRotation = playerLookingLike; //
                Debug.Log(" lvl 2 Player at: " + UnitManager.PlayerMapPosition);

            }
            else
            {
                float targetHealth = selectedUnit[victim].GetComponent<Unit>().GetComponentInChildren<Stats>().getHealth(); //unit being attacked
                UnitManager.EnemyBattleResultHealth = targetHealth;
                Vector3 enemyLastKnowPosition = selectedUnit[victim].transform.position;
                Quaternion enemyLookingLike = selectedUnit[victim].transform.rotation;
              
                UnitManager.EnemyMapPosition = enemyLastKnowPosition;//crap how do I get the transfrom from this exactly?
                UnitManager.EnemyRotation = enemyLookingLike; //
                Debug.Log(" lvl 2 Enemy spatial Coordinates on Lock"+UnitManager.EnemyMapPosition);




            }
            
            SceneManager.LoadScene("BattleScene");// if it works, it will simply switch no record of stats will be maintained

        }
        else
        {
            Debug.Log("Cannot attack enemy");
        }
    }

    // returns true if the unit is standing in directly next to a unit
    // this method does not check for diagonal units
    public int EnemyInFront(int n)
    {
        int currentX = selectedUnit[n].GetComponent<Unit>().tileX;
        int currentY = selectedUnit[n].GetComponent<Unit>().tileY;
        bool status = selectedUnit[n].GetComponent<Unit>().isEnemy;
        int unit;
        if (TileNotOccupied(currentX + 1, currentY) == false)
        {
            unit = getUnitSelector(currentX + 1, currentY);
            if (selectedUnit[unit].GetComponent<Unit>().isEnemy != status)
            {
                Debug.Log("Found an enemy!");
                return unit;
            }
            else
            {
                Debug.Log("No friendly fire asshole...");
            }
        }
        if (TileNotOccupied(currentX - 1, currentY) == false)
        {
            unit = getUnitSelector(currentX - 1, currentY);
            if (selectedUnit[unit].GetComponent<Unit>().isEnemy != status)
            {
                Debug.Log("Found an enemy!");
                return unit;
            }
            else
            {
                Debug.Log("No friendly fire asshole...");
            }
        }
        if (TileNotOccupied(currentX, currentY + 1) == false)
        {
            unit = getUnitSelector(currentX, currentY + 1);
            if (selectedUnit[unit].GetComponent<Unit>().isEnemy != status)
            {
                Debug.Log("Found an enemy!");
                return unit;
            }
            else
            {
                Debug.Log("No friendly fire asshole...");
            }
        }
        if (TileNotOccupied(currentX, currentY - 1) == false)
        {
            unit = getUnitSelector(currentX, currentY - 1);
            if (selectedUnit[unit].GetComponent<Unit>().isEnemy != status)
            {
                Debug.Log("Found an enemy!");
                return unit;
            }
            else
            {
                Debug.Log("No friendly fire asshole...");
            }
        }
        Debug.Log("No enemies found.");
        return -1;
    }

    // return index of unit at x, y
    // return -1 if there is no unit
    public int getUnitSelector(int x, int y)
    {
        for (int k = 0; k < selectedUnit.Count; k++)
        {
            int unitX = selectedUnit[k].GetComponent<Unit>().tileX;
            int unitY = selectedUnit[k].GetComponent<Unit>().tileY;
            if (x == unitX && y == unitY)
            {
                Debug.Log("Found unit at location.");
                return k;
            }
        }
        Debug.Log("Couldn't locate unit.");
        return -1;
    }

    // Checks if the tile is occupied by another unit.
    // @return - false if the tile is occupied, true if it is not
    public bool TileNotOccupied(int x, int y)
    {
        for (int k = 0; k < selectedUnit.Count; k++)
        {
            int unitX = selectedUnit[k].GetComponent<Unit>().tileX;
            int unitY = selectedUnit[k].GetComponent<Unit>().tileY;
            if (x == unitX && y == unitY)
            {
                Debug.Log("There's someone there!");
                return false;
            }
        }
        //Debug.Log("By all means...");
        return true;
    }

    public void GeneratePathTo(int x, int y, int n)
    {
        //selectedUnit.GetComponent<Unit>().tileX = x;
        //selectedUnit.GetComponent<Unit>().tileY = y;
        //selectedUnit.transform.position = TileCoordToWorldCoord(x, y);

        // clear out our unit's old path
        selectedUnit[n].GetComponent<Unit>().currentPath = null;

        if (UnitCanEnterTile(x, y) == false || TileNotOccupied(x, y) == false)
        {
            // quit out since we clicked on a mountian or water or the tile is occupied by another unit
            Debug.Log("You are an idiot! That unit can't enter that tile!");
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();
        Node source = graph[
                            selectedUnit[n].GetComponent<Unit>().tileX,
                            selectedUnit[n].GetComponent<Unit>().tileY];

        Node target = graph[x, y];
        selectedUnit[n].GetComponent<Unit>().targetNode = target;

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            Node u = null; // unvisted node with smallest distance

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break; // run away!
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                // float alt = dist[u] + u.DistanceTo(v)
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // if we get there we found shortest route to target
        // ORR there is no route to the target

        if (prev[target] == null)
        {
            // no route between target and source
            Debug.Log("No route between current and target nodes..");
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // currentPath describes route from target to source
        // sooo reverse that bitch!
        currentPath.Reverse();

        selectedUnit[n].GetComponent<Unit>().currentPath = currentPath;
    }

    // this method currently allows you to control all units
    // TODO : needs to be altered to only allow switching between friendly units
    public void ChangeUnit()
    {
        unitSelector++;
        if (unitSelector >= selectedUnit.Count)
        {
            unitSelector = 0;
            Debug.Log("Setting unitSelector to 0");
        }
    }

    // end the turn and then move the enemies towards the nearest unit
    public void EndTurn()
    {
        for (int k = 0; k < selectedUnit.Count; k++)
        {
            if (selectedUnit[k].GetComponent<Unit>().isEnemy == true)
            {
                Debug.Log("Moving towards units");
                int chosenUnit = LocateClosestUnit(k);
                GeneratePathTo(selectedUnit[chosenUnit].GetComponent<Unit>().tileX,
                    selectedUnit[chosenUnit].GetComponent<Unit>().tileY + 1,
                    k);
                if (selectedUnit[k].GetComponent<Unit>().targetNode == selectedUnit[k - 1].GetComponent<Unit>().targetNode)
                {
                    GeneratePathTo(selectedUnit[chosenUnit].GetComponent<Unit>().tileX + 1,
                        selectedUnit[chosenUnit].GetComponent<Unit>().tileY + 1,
                        k);
                }
            }
        }
        //GeneratePathTo(0, 4, 3);
    }

    public int LocateClosestUnit(int n)
    {
        int unitX = selectedUnit[n].GetComponent<Unit>().tileX;
        int unitY = selectedUnit[n].GetComponent<Unit>().tileY;
        bool friend = selectedUnit[n].GetComponent<Unit>().isEnemy;
        int prevDist = 100;
        int closestUnitSelector = -1;

        for (int k = 0; k < selectedUnit.Count; k++)
        {
            if (k != n && friend != selectedUnit[k].GetComponent<Unit>().isEnemy)
            {
                int kX = selectedUnit[k].GetComponent<Unit>().tileX;
                int kY = selectedUnit[k].GetComponent<Unit>().tileY;
                int dist = Mathf.Abs(unitX - kX) + Mathf.Abs(unitY - kY);
                if (dist < prevDist)
                {
                    prevDist = dist;
                    closestUnitSelector = k;
                }
            }
        }
        if (closestUnitSelector == -1)
        {
            Debug.Log("Could not find a unit to pursue.");
        }
        return closestUnitSelector;
    }
}