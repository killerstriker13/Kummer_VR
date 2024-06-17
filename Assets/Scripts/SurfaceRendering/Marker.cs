using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{

    private int function = 9;
    public GameObject markerPrefab;
    private List<Chunk> markers;
    private Chunk marker;
    private float size = 1.0f;
    private float scale = 16.5f;
    private float otherSize = 0.08f;

    void Start()
    {
        float offsetVal = (GridMetrics.PointsPerChunk / 2 - 0.5f) * (size / 100f);
        markers = new List<Chunk>();


    }

    public void CreateMarker(Vector3 pos)
    {
        marker = Instantiate(markerPrefab, transform.position, Quaternion.identity, transform).GetComponent<Chunk>();
        marker.transform.position = transform.position + pos;
        marker.size = size;
        marker.otherSize = otherSize;
        markers.Add(marker);
        Render(markers.Count-1);
    }

    public void DestroyMarkers()
    {
        int ct = markers.Count;
        for (int i=0; i<ct; i++) 
        {
            Destroy(markers[i].gameObject);
        }
        markers.Clear();
    }

  

    private void Render(int i)
    {
        NoiseGenerator ng = markers[i].GetComponent<NoiseGenerator>();
        ng.scale = scale;
        ng.size = size;
        ng.function = function;
        ng.otherSize = otherSize;
        ng.Offset = markers[i].transform.position - transform.position;
        markers[i].Render();
    }

    public int Count()
    {
        return markers.Count;
    }
















}
