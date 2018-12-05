using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public GameObject selectedUnit;
    public TileType[] tileTypes;

    int[,] tiles;
    Node[,] graph;

    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start () {
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;
        GenerateMapData();
        GeneratePathFindingGraph();
        GenerateMapVisuals();
    }

    void GenerateMapData() {
        // allocate them tiles brooo
        tiles = new int[mapSizeX, mapSizeY];

        // those grass tiles fool
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                tiles[x, y] = 0;
            }
        }

        // make that bitchass swamp broooo
        for (int x = 3; x <= 5; x++) {
            for (int y = 0; y < 4; y++) {
                tiles[x, y] = 1;
            }
        }

        // mountain range that looks like the U
        tiles[4, 4] = 2;
        tiles[5, 4] = 2;
        tiles[6, 4] = 2;
        tiles[7, 4] = 2;
        tiles[8, 4] = 2;

        tiles[4, 5] = 2;
        tiles[4, 6] = 2;
        tiles[8, 5] = 2;
        tiles[8, 6] = 2;
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY) {
        TileType tt = tileTypes[tiles[targetX, targetY]];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        if (sourceX!=targetX && sourceY!=targetY) {
            // fudge cost for tiebreaking
            cost += 0.001f;
        }

        return cost;
    }

    void GeneratePathFindingGraph() {
        // intialize array
        graph = new Node[mapSizeX, mapSizeY];

        // initialize node for each spot in array
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }


        // calculate neighbors for nodes
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {

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
                if (x > 0) {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                // Try Right
                if (x < mapSizeX - 1) {
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

    void GenerateMapVisuals() {
        for(int x=0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                TileType tity = tileTypes[ tiles[x, y] ];
                GameObject go = (GameObject)Instantiate(tity.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                TileClickHandler tch = go.GetComponent<TileClickHandler>();
                tch.tileX = x;
                tch.tileY = y;
                tch.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y) {
        return new Vector3(x, y, 0);
    }

    public bool UnitCanEnterTile(int x, int y) {
        // test unit's walk/fly/hover type against various
        // terrain flags to see if they can enter tile
        return tileTypes[tiles[x, y]].isWalkable;
    }

    public void GeneratePathTo(int x, int y) {
        //selectedUnit.GetComponent<Unit>().tileX = x;
        //selectedUnit.GetComponent<Unit>().tileY = y;
        //selectedUnit.transform.position = TileCoordToWorldCoord(x, y);

        // clear out our unit's old path
        selectedUnit.GetComponent<Unit>().currentPath = null;

        if (UnitCanEnterTile(x, y) == false) {
            // quit out since we clicked on a mountian or water..
            Debug.Log("You are an idiot! That unit can't enter that tile!");
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();
        Node source = graph[
                            selectedUnit.GetComponent<Unit>().tileX,
                            selectedUnit.GetComponent<Unit>().tileY];

        Node target = graph[x, y];

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph) {
            if (v!=source) {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0) {
            Node u = null; // unvisted node with smallest distance

            foreach(Node possibleU in unvisited) {
                if (u == null || dist[possibleU] < dist[u]) {
                    u = possibleU;
                }
            }

            if (u == target) {
                break; // run away!
            }

            unvisited.Remove(u);

            foreach(Node v in u.neighbours) {
                // float alt = dist[u] + u.DistanceTo(v)
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v]) {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // if we get there we found shortest route to target
        // ORR there is no route to the target

        if (prev[target] == null) {
            // no route between target and source
            Debug.Log("No route between current and target nodes..");
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while(curr != null) {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // currentPath describes route from target to source
        // sooo reverse that bitch!
        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
