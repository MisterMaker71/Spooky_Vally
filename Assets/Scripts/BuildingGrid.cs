using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public LayerMask Mask;
    public List<GridTile> covertGrid = new List<GridTile>();
    public bool isSelected;
    public List<Buildebel> objecsOnGrid = new List<Buildebel>();
    // Start is called before the first frame update
    void Start()
    {
        Bounds b = GetComponent<MeshRenderer>().bounds;
        for (float x = 0; x < b.size.x; x += 0.5f)
        {
            for (float z = 0; z < b.size.z; z += 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(b.min + new Vector3(x + 0.25f, 10, z + 0.25f), Vector3.down, out hit, 15, Mask))
                {
                    if (hit.transform == transform)
                    {
                        covertGrid.Add(new GridTile(hit.point));
                    }
                }
            }
        }
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 15, Mask))
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
    }
    public void Place(string g, Vector3 v)
    {
        if(Resources.Load("Buildebels/" + g) != null)
        {
            GameObject gam = Resources.Load<GameObject>("Buildebels/" + g);
            Buildebel b = gam.GetComponent<Buildebel>();
            if (gam != null && b != null)
            {
                GridTile t = nearestTile(v);
                GameObject placed = Instantiate(gam, t.position + new Vector3(-b.placeOffset.x, 0, -b.placeOffset.y), Quaternion.identity);
                placed.transform.Rotate(b.placeRotationOffset);
                foreach (Vector2 vector in b.coverdTiles)
                {
                    nearestTile(placed.transform.position + new Vector3(b.placeOffset.x / 2, 0, b.placeOffset.y / 2) + new Vector3(vector.x / 2, 0, vector.y / 2)).ocupied = true;
                }
            }
        }
    }
    public GridTile nearestTile(Vector3 pos)
    {
        float dist = 1000;
        GridTile tile = new GridTile(Vector3.zero);
        foreach (GridTile item in covertGrid)
        {
            if(Vector3.Distance(pos, item.position) < dist)
            {
                dist = Vector3.Distance(pos, item.position);
                tile = item;
            }
        }
        return tile;
    }


    [System.Serializable]
    public class GridTile
    {
        public Vector3 position;
        public bool ocupied = false;
        public GridTile(Vector3 _position)
        {
            position = _position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (GridTile pos in covertGrid)
        {
            if (pos.ocupied)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.gray;
            
            Gizmos.DrawWireCube(pos.position, new Vector3(0.4f, 0, 0.4f));
        }
    }
}
