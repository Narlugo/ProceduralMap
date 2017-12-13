using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float power = 5f;

    public int spawnDistance = 10;

    public int rayX = 20;
    public int rayY = 20;

    public int numberOfBuilding = 10;

    private int olddepth = 20;
    private int oldwidth = 256;
    private int oldheight = 256;
    private float oldscale = 20f;
    private float oldpower = 5f;
    private int oldnumberOfBuilding = 10;

    private bool valueChanged = false;

    private bool compute = false;

    public GameObject go;

    private List<GameObject> list =  new List<GameObject>();

    private void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        bool valueHasChanged = ChangeValue();
        if(valueChanged != valueHasChanged)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
            Debug.Log("moui");

            computeRaycast();
            //InstantiateBuilding(terrain);
        }

    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;
        return Mathf.Pow(Mathf.PerlinNoise(xCoord, yCoord),power);
    }


    public bool ChangeValue()
    {
        if (depth != olddepth)
        {
            olddepth = depth;
            return true;
        }
        if (width != oldwidth)
        {
            oldwidth = width;
            return true;
        }
        if (height != oldheight)
        {
            oldheight = height;
            return true;
        }
        if (scale != oldscale)
        {
            oldscale = scale;
            return true;
        }
        if (power != oldpower)
        {
            oldpower = power;
            return true;
        }
        if (numberOfBuilding != oldnumberOfBuilding)
        {
            oldnumberOfBuilding = numberOfBuilding;
            return true;
        }
        return false;
    }

    public void computeRaycast()
    {
        for(int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
        }
        list.Clear();
        Ray ray = new Ray(Vector3.zero, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Collider terrainCol = GetComponent<TerrainCollider>();
        for (int x = 0;x< rayX; x++)
        {
            for(int y = 0;y < rayY; y++)
            {
                ray.origin = transform.position + new Vector3((float)x / (float)rayX * width , 25f, (float)y / (float)rayY * height);
                Debug.DrawLine(ray.origin, ray.origin - new Vector3(0,30,0));
                if(terrainCol.Raycast(ray, out hit, spawnDistance))
                {
                    if (Vector3.Dot(Vector3.up, hit.normal) > 0.9f && hit.point.y > 0.1)
                    {
                        int randomSpawn = Random.Range(1, numberOfBuilding);
                        if(randomSpawn == 1)
                        {
                            Debug.Log("MAISON");
                            GameObject o = Instantiate(go, hit.point, Quaternion.identity);
                            o.transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(3, 8), Random.Range(1, 5));
                            o.transform.rotation = Quaternion.Euler(0, Random.Range(-45, 45), 0);
                            list.Add(o);
                        }
                    }
                }
                
            }
        }

    }
    
}
