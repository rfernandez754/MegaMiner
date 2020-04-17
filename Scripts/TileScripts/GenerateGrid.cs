using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public GameObject cube;

    public Material dirtMaterial;
    public Material stoneMaterial;
    public Material grassMaterial;
    public Material darkStoneMaterial; // Dark Grey Dark Stone
    public Material coalMaterial;
    public Material ironMaterial;
    public Material goldMaterial;
    public Material diamondMaterial; // Dark Grey Dark Stone
    public Material silverMaterial;
    public Material emeraldMaterial;


    public Mesh treasureMesh;
    public Material treaureMaterial;
    public Material treaureMaterial2;

    public int gridX;
    public int gridY;

    private GameObject currentTile;

    // Start is called before the first frame update
    void Start()
    {

        //Generate backround brown tiles
        for (int x = -1 * gridX; x < gridX*2; x++)
        {
            for (int y = -1 * gridY; y < 0; y++)
            {
                GenerateTile(x, y, 1, -2);
            }
        }
        for (int x = -1 * gridX; x < gridX * 2; x++)
        {
            GenerateTile(x, -0.002F, 1, -2);
        }

        int colorIndex = -1;

        //Generate mineable tiles
        for (int x = -gridX; x < gridX; x++)
        {
            for(int y = 0; y < gridY; y++)
            {
                colorIndex++;
                GenerateTile(x, -y, 0,colorIndex);
            }
            colorIndex++;
        }

        Vector3 pos = new Vector3(0, 1, currentTile.transform.position.z); // old x: currentTile.transform.position.x/2
        GameObject.Find("Player").transform.position = pos;
    }

    void GenerateTile(float x, float y, float z, int colorIndex)
    {
        GameObject tile = Instantiate(cube, new Vector3(x * 1.0F, y * 1.0F, z * 1.0F), Quaternion.identity);

        if(colorIndex == -2)
        {
            tile.GetComponent<MeshRenderer>().material = dirtMaterial;
        }
        else if(y > -1)
        {
            GenerateLevel0(tile);
        }
        else if (y > -5)
        {
            GenerateLevel1(tile);
        }
        else if (y > -25)
        {
            GenerateLevel2(tile);
        }
        else if (y > -100)
        {
            GenerateLevel3(tile);
        }
        else
        {
            tile.GetComponent<MeshRenderer>().material = dirtMaterial;
        }

        currentTile = tile;
    }

    void GenerateLevel0(GameObject tile)
    {
        MakeGrass(tile);
    }

    void GenerateLevel1(GameObject tile)
    {
        float num = Random.Range(0.0f, 100.0f);
        if (num <= 98)
        {
            MakeStone(tile);
        }
        else if (num <= 100)
        {
            MakeCoal(tile);
        }
    }

    void GenerateLevel2(GameObject tile)
    {
        float num = Random.Range(0.0f, 100.0f);
        if (num <= 90)
        {
            MakeStone(tile);
        }
        else if (num <= 99)
        {
            MakeCoal(tile);
        }
        else
        {
            MakeWoodenTreasure(tile);
        }
    }

    void GenerateLevel3(GameObject tile)
    {
        float num = Random.Range(0.0f, 100.0f);
        if (num <= 90)
        {
            MakeHardStone(tile);
        }
        else if (num <= 95)
        {
            MakeCoal(tile);
        }
        else if (num <= 99)
        {
            MakeIron(tile);
        }
        else if (num <= 99.8)
        {
            MakeWoodenTreasure(tile);
        }
        else
        {
            MakeIronTreasure(tile);
        }
    }

    private void MakeGrass(GameObject tile)
    {
        tile.GetComponent<MeshRenderer>().material = grassMaterial;
        tile.GetComponent<TileStats>().id = 0;
        tile.GetComponent<TileStats>().toughness = 1;
        tile.GetComponent<TileStats>().worth = 1;
        tile.GetComponent<TileStats>().tileName = "Grass";
        tile.GetComponent<TileStats>().xp = 1.0F;
    }

    private void MakeStone(GameObject tile)
    {
        tile.GetComponent<MeshRenderer>().material = stoneMaterial;
        tile.GetComponent<TileStats>().id = 1;
        tile.GetComponent<TileStats>().toughness = 3;
        tile.GetComponent<TileStats>().worth = 5;
        tile.GetComponent<TileStats>().tileName = "Stone";
        tile.GetComponent<TileStats>().xp = 2.0F;
    }

    private void MakeCoal(GameObject tile)
    {
        tile.GetComponent<MeshRenderer>().material = coalMaterial;
        tile.GetComponent<TileStats>().id = 2;
        tile.GetComponent<TileStats>().toughness = 10;
        tile.GetComponent<TileStats>().worth = 25;
        tile.GetComponent<TileStats>().tileName = "Coal";
        tile.GetComponent<TileStats>().xp = 5.0F;
    }

    private void MakeWoodenTreasure(GameObject tile)
    {
        tile.GetComponent<MeshFilter>().mesh = treasureMesh;
        tile.GetComponent<MeshRenderer>().material = treaureMaterial;
        tile.transform.Rotate(-90, 0, -90, Space.Self);
        tile.GetComponent<TileStats>().id = 3;
        tile.GetComponent<TileStats>().toughness = 3;
        tile.GetComponent<TileStats>().worth = 100;
        tile.GetComponent<TileStats>().tileName = "Wooden Treasure";
        tile.GetComponent<TileStats>().xp = 25.0F;
    }

    private void MakeHardStone(GameObject tile)
    {
        tile.GetComponent<MeshRenderer>().material = darkStoneMaterial;
        tile.GetComponent<TileStats>().id = 4;
        tile.GetComponent<TileStats>().toughness = 20;
        tile.GetComponent<TileStats>().worth = 10;
        tile.GetComponent<TileStats>().tileName = "Hard Stone";
        tile.GetComponent<TileStats>().xp = 15.0F;
    }

    private void MakeIron(GameObject tile)
    {
        tile.GetComponent<MeshRenderer>().material = ironMaterial;
        tile.GetComponent<TileStats>().id = 5;
        tile.GetComponent<TileStats>().toughness = 40;
        tile.GetComponent<TileStats>().worth = 250;
        tile.GetComponent<TileStats>().tileName = "Iron";
        tile.GetComponent<TileStats>().xp = 20.0F;
    }

    private void MakeIronTreasure(GameObject tile)
    {
        tile.GetComponent<MeshFilter>().mesh = treasureMesh;
        tile.GetComponent<MeshRenderer>().material = treaureMaterial2;
        tile.transform.Rotate(-90, 0, -90, Space.Self);
        tile.GetComponent<TileStats>().id = 6;
        tile.GetComponent<TileStats>().toughness = 30;
        tile.GetComponent<TileStats>().worth = 1000;
        tile.GetComponent<TileStats>().tileName = "Iron Treasure";
        tile.GetComponent<TileStats>().xp = 300.0F;
    }
}
