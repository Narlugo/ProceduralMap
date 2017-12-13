using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalCity : MonoBehaviour {

    public GameObject go;
    public int numObjects = 20;
    public int radius = 10;
    public int nbCircle = 5;
    public int space = 1;

    private int oldNumObjects = 20;
    private int oldRadius = 10;
    private int oldNbCircle = 5;
    private int oldSpace = 1;


    private bool valueChanged = false;
    // Use this for initialization
    void Start () {

        generateInside();
    }
	
    public void generateInside()
    {
        int objectsByCircle = numObjects;
        for(int n = 1; n < nbCircle; n++)
        {
            for (int i = 0; i < objectsByCircle; i++)
            {
                float theta = i * 2 * Mathf.PI / objectsByCircle;
                float x = Mathf.Sin(theta) * (radius - n);
                float y = Mathf.Cos(theta) * (radius - n);


                //go.transform.SetParent(transform);
                float scale = Random.Range(n, n + 2);
                go.transform.localScale = new Vector3(1,scale , 1);
                go.transform.position = new Vector3(x, scale / 2 + (0.01f), y);
                GameObject obj = Instantiate(go, transform);
                //obj.transform.LookAt(transform.position);
            }
            objectsByCircle-=2;
        }

    }
	// Update is called once per frame
	void Update () {
        bool valueHasChanged = ChangeValue();
        if (valueChanged != valueHasChanged)
        {
            clearCity();
            generateInside();
        }
    }

    public bool ChangeValue()
    {
        if (numObjects != oldNumObjects)
        {
            oldNumObjects = numObjects;
            return true;
        }
        if (radius != oldRadius)
        {
            oldRadius = radius;
            return true;
        }
        if (nbCircle != oldNbCircle)
        {
            oldNbCircle = nbCircle;
            return true;
        }
        if (space != oldSpace)
        {
            oldSpace = space;
            return true;
        }

        return false;
    }

    public void clearCity()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("building");
        for(int i = 0; i < buildings.Length; i++)
        {
            Destroy(buildings[i]);
        }
    }
}
