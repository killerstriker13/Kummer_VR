using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is used to define the 2x2x2 grid of chunks that makes up the rendered object. 
Each chunk has a limit to the amount of triangles it can define because of the memory size therefore,
it gets split up to keep the triangle count lower.

This object holds the parameters of the object itself.

*/

public class Surface : MonoBehaviour
{
    public int function;

    public int orientation;

    public bool visible;

    public GameObject chunkPrefab;

    public List<Chunk> chunks;

    private List<Renderer> ren;

    public float size = 1;
    public float scale = 10;
    

    void Start()
    {
        chunks = new List<Chunk>();
        ren = new List<Renderer>();

        for (int i = 0; i < 8; i++)
            {
                chunks.Add(Instantiate(chunkPrefab, transform.position, Quaternion.identity, transform).GetComponent<Chunk>());
                chunks[i].size = size;
                ren.Add(chunks[i].GetComponent<Renderer>());
            }

        

        // Each chunk's origin is defined in it's center so each chunk needs to be offset from the origin of the whole volume.
        float offsetVal = (GridMetrics.PointsPerChunk / 2 - 0.5f) * (size / 100f);
        chunks[0].transform.position = transform.position + new Vector3(-offsetVal, -offsetVal, offsetVal);
        chunks[1].transform.position = transform.position + new Vector3(offsetVal,  - offsetVal, offsetVal);
        chunks[2].transform.position = transform.position + new Vector3(-offsetVal, - offsetVal, -offsetVal);
        chunks[3].transform.position = transform.position + new Vector3(offsetVal, - offsetVal, -offsetVal);
        chunks[4].transform.position = transform.position + new Vector3(-offsetVal, offsetVal, offsetVal);
        chunks[5].transform.position = transform.position + new Vector3(offsetVal, offsetVal, offsetVal);
        chunks[6].transform.position = transform.position + new Vector3(-offsetVal, offsetVal, -offsetVal);
        chunks[7].transform.position = transform.position + new Vector3(offsetVal, offsetVal, -offsetVal);

        Render();

        if (visible)
        {
            for (int i = 0; i < 8; i++)
            {
                ren[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                ren[i].enabled = false;
            }
        }


    }

   
    public void Render()
    {

        // Here we dispatch all of the parameters to the chunks and call for each to be rerendered
        for (int i = 0; i < 8; i++)
        {
            NoiseGenerator ng = chunks[i].GetComponent<NoiseGenerator>();
            ng.scale = scale;
            ng.size = size;
            ng.function = function;
            ng.orientation = orientation;
            // ng.planeForward = slicePlane.transform.forward;
            // ng.planeRight = slicePlane.transform.right;
            ng.Offset = chunks[i].transform.position - transform.position;
            
            chunks[i].Render();
        }

    }

    public void RenderLeft()
    {

        // Here we dispatch all of the parameters to the chunks and call for each to be rerendered
        for (int i = 0; i < 8; i++)
        {
            NoiseGenerator ng = chunks[i].GetComponent<NoiseGenerator>();
            ng.scale = scale;
            ng.size = size;
            ng.function = function;
            ng.orientation = orientation;
            // ng.planeForward = slicePlane.transform.forward;
            // ng.planeRight = slicePlane.transform.right;
            ng.Offset = chunks[i].transform.position - transform.position;
            ng.muSquared -= 0.05f;
            chunks[i].Render();
        }

    }
    public void RenderRight()
    {

        // Here we dispatch all of the parameters to the chunks and call for each to be rerendered
        for (int i = 0; i < 8; i++)
        {
            NoiseGenerator ng = chunks[i].GetComponent<NoiseGenerator>();
            ng.scale = scale;
            ng.size = size;
            ng.function = function;
            ng.orientation = orientation;
            // ng.planeForward = slicePlane.transform.forward;
            // ng.planeRight = slicePlane.transform.right;
            ng.Offset = chunks[i].transform.position - transform.position;
            ng.muSquared += 0.05f;
            chunks[i].Render();
        }

    }

    public void RenderSmall()
    {
        // Here we dispatch all of the parameters to the chunks and call for each to be rerendered
        for (int i = 0; i < 8; i++)
        {
            NoiseGenerator ng = chunks[i].GetComponent<NoiseGenerator>();
            ng.scale = scale;
            ng.size = size;
            ng.function = function;
            ng.orientation = orientation;
            // ng.planeForward = slicePlane.transform.forward;
            // ng.planeRight = slicePlane.transform.right;
            ng.Offset = chunks[i].transform.position - transform.position;
            ng.muSquared -= 1.0f;
            chunks[i].Render();
        }
    }
    public void RenderBig()
    {
        // Here we dispatch all of the parameters to the chunks and call for each to be rerendered
        for (int i = 0; i < 8; i++)
        {
            NoiseGenerator ng = chunks[i].GetComponent<NoiseGenerator>();
            ng.scale = scale;
            ng.size = size;
            ng.function = function;
            ng.orientation = orientation;
            // ng.planeForward = slicePlane.transform.forward;
            // ng.planeRight = slicePlane.transform.right;
            ng.Offset = chunks[i].transform.position - transform.position;
            ng.muSquared += 1.0f;
            chunks[i].Render();
        }
    }

    public void ChangeVisibility()
    {
        if (visible)
        {
            visible = false;
            for (int i = 0; i < 8; i++)
            {
                ren[i].enabled = false;
            }
        }
        else
        {
            visible = true;
            for (int i = 0; i < 8; i++)
            {
                ren[i].enabled = true;
            }
        }
    }

    public void ChangeColor()
    {
        for (int i = 0; i < 8; i++)
            {
                ren[i].material.color = Color.red;
            }
    }


}