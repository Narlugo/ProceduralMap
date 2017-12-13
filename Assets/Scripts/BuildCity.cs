using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCity : MonoBehaviour {

    public GameObject building;
    public GameObject grass;
    public GameObject xstreet;
    public GameObject zstreet;
    public GameObject cross;
    public int width = 20;
    public int height = 20;
    int[,] mapgrid;
    private int buildingFootPrint = 3;

    // Use this for initialization
    void Start () {
        GenerateCity();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
	}

    public void GenerateCity()
    {
        mapgrid = new int[width, height];

        for(int h = 0; h < height; h++)
        {
            for(int w = 0; w < width; w++)
            {
                mapgrid[w, h] = (int)(Mathf.PerlinNoise(w / 10f, h / 10f) * 10);
            }
        }

        int x = 0;
        for(int n = 0; n < 25; n++)
        {
            for(int h = 0; h < height; h++)
            {
                mapgrid[x, h] = -1;
            }
            x += Random.Range(3, 3);
            if (x >= width) break;
        }

        int z = 0;
        for(int n = 0; n < 10; n++)
        {
            for(int w =0; w< width; w++)
            {
                if(mapgrid[w,z] == -1)
                {
                    mapgrid[w, z] = -3;
                }
                else
                {
                    mapgrid[w, z] = -2;
                }
            }
            z += Random.Range(5, 20);
            if (z >= height) break;
        }

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                int result = mapgrid[w,h];

                if (result < -2)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 0.02f, h * buildingFootPrint);
                    GameObject go = Instantiate(cross, pos, cross.transform.rotation);
                }
                else if (result < -1)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 0.02f, h * buildingFootPrint);
                    GameObject go = Instantiate(xstreet, pos, xstreet.transform.rotation);
                }
                else if (result < 0)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 0.02f, h * buildingFootPrint);
                    GameObject go = Instantiate(zstreet, pos, zstreet.transform.rotation);
                }
                else if (result < 1)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 1, h * buildingFootPrint);
                    GameObject go = Instantiate(building, pos, Quaternion.identity);
                    go.transform.localScale = new Vector3(go.transform.localScale.x, 2, go.transform.localScale.z);
                }
                else if (result < 3)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 2, h * buildingFootPrint);
                    GameObject go = Instantiate(building, pos, Quaternion.identity);
                    go.transform.localScale = new Vector3(go.transform.localScale.x, 4, go.transform.localScale.z);
                }
                else if (result < 5)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 3, h * buildingFootPrint);
                    GameObject go = Instantiate(building, pos, Quaternion.identity);
                    go.transform.localScale = new Vector3(go.transform.localScale.x, 6, go.transform.localScale.z);
                }
                else if (result < 6)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 4, h * buildingFootPrint);
                    GameObject go = Instantiate(building, pos, Quaternion.identity);
                    go.transform.localScale = new Vector3(go.transform.localScale.x, 8, go.transform.localScale.z);
                }
                else if (result < 7)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 5, h * buildingFootPrint);
                    GameObject go = Instantiate(building, pos, Quaternion.identity);
                    go.transform.localScale = new Vector3(go.transform.localScale.x, 10, go.transform.localScale.z);
                }
                else if (result < 10)
                {
                    Vector3 pos = new Vector3(w * buildingFootPrint, 0.01f, h * buildingFootPrint);
                    GameObject go = Instantiate(grass, pos, Quaternion.identity);
                }
            }
        }
    }
}
