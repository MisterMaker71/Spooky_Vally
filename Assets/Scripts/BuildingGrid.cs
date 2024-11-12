using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public LayerMask Mask;
    public List<GridTile> covertGrid = new List<GridTile>();

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
