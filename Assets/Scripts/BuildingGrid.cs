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
            if (hit.transform == transform)
            {
                isSelected = true;
            }
            else
            {
                isSelected = false;
            }
        }
        else
        {
            isSelected = false;
        }
    }
    /// <summary>
    /// not used
    /// </summary>
    public void Clean()
    {
        //foreach (Buildebel buildebel in objecsOnGrid)
        //{
        //    if (buildebel == null)
        //    {
        //        objecsOnGrid.Remove(buildebel);
        //    }
        //}
    }
    public void ResetCoverd()
    {
        foreach (var item in covertGrid)
        {
            item.ocupied = false;
        }
    }
    public Buildebel Place(string g, Vector3 v)
    {
        return Place(g, v, new Vector3());
    }
    public Buildebel Place(string g, Vector3 v, Vector3 r)
    {
            return Place(g, v, r, "");
    }
    public Buildebel Place(string g, Vector3 v, string id)
    {
        return Place(g, v, new Vector3(), id);
    }
    public Buildebel Place(string g, Vector3 v, Vector3 r, string id)
    {
        return Place(g, v, r, id, false);
    }
    public Buildebel Place(string g, Vector3 v, Vector3 r, string id, bool saved)
    {
        //print(v);
        if(Resources.Load("Buildebels/" + g) != null)
        {
            GameObject gam = Resources.Load<GameObject>("Buildebels/" + g);
            Buildebel b = gam.GetComponent<Buildebel>();
            if (gam != null && b != null)
            {
                b.SetID(id);
                //GridTile t = nearestTile(v + new Vector3(r.x * b.placeOffset.x / 2, 0, r.z * b.placeOffset.y / 2), 0.5f);
                //GridTile t = nearestTile(v, 0.5f);

                GameObject placed = Instantiate(gam, v, Quaternion.identity, transform);

                if (saved)
                {
                    placed.transform.localEulerAngles = r;
                    //placed.transform.position = v;
                }
                else
                {
                    //if (v != Vector3.zero)
                    placed.transform.forward = r;
                    placed.transform.Rotate(b.placeRotationOffset);
                    //placed.transform.position = v;
                    //placed.transform.position = v + placed.transform.right * b.placeOffset.x + placed.transform.forward * b.placeOffset.y;
                }

                if (TestFreeTiles(b.coverdTiles, v, placed.transform, b.placeOffset, 0.35f))
                {
                    //GameObject placed = Instantiate(gam, Interactor.prepos, Quaternion.identity, transform);

                    placed.name = gam.name;
                    objecsOnGrid.Add(placed.GetComponent<Buildebel>());


                    foreach (Vector2 vector in b.coverdTiles)
                    {
                        //if (saved)
                        //{
                            //Debug.DrawLine(v, placed.transform.position + placed.transform.right * -vector.x + placed.transform.forward * -vector.y, Color.yellow, 2);
                            nearestTile(v + (placed.transform.right * b.placeOffset.x + placed.transform.forward * b.placeOffset.y) + (placed.transform.right * vector.x / 2 + placed.transform.right * vector.y / 2), 0.25f).ocupied = true;
                        /*}
                        else
                        {
                            Debug.DrawLine(t.position, placed.transform.position + new Vector3(r.x * vector.x / 2, 0, r.z * vector.y / 2), Color.yellow, 2);
                            nearestTile(t.position + new Vector3(r.x * vector.x / 2, 0, r.z * vector.y / 2), 0.25f).ocupied = true;
                            //nearestTile(t.position + new Vector3(r.x * vector.x / 2, 0, r.z * vector.y / 2), 0.25f).ocupied = true;
                        }*/
                    }
                    return b;
                }
                else
                {
                    Destroy(placed);
                    return null;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vr">right normal</param>
    /// <param name="vl">left normal</param>
    public bool TestFreeTiles(Vector2[] positions, Vector3 origin, Transform dir, Vector2 offset, float searchRadius)
    {
        foreach (Vector2 pos in positions)
        {
            GridTile t = nearestTile(origin + (dir.right * offset.x + dir.forward * offset.y) + (dir.right * pos.x / 2 + dir.right * pos.y / 2), searchRadius);
            //GridTile t = nearestTile((dir.right * pos.x / 2 + dir.forward * pos.y / 2) + origin, searchRadius);
            if (t.ocupied)
            {
                Debug.DrawRay(t.position, Vector3.up, Color.red, 5);
                return false;
            }
            else
                Debug.DrawRay(t.position, Vector3.up, Color.green, 5);
        }
        return true;
    }
    public GridTile nearestTile(Vector3 pos, float maxDistance)
    {
        float dist = 1000;
        GridTile tile = new GridTile(Vector3.one * 100, true);
        foreach (GridTile item in covertGrid)
        {
            if(Vector3.Distance(pos, item.position) < dist && Vector3.Distance(pos, item.position) < maxDistance)
            {
                dist = Vector3.Distance(pos, item.position);
                tile = item;
            }
        }
        Debug.DrawLine(pos, tile.position, Color.gray, 5);
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
        public GridTile(Vector3 _position, bool _ocupied)
        {
            ocupied = _ocupied;
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
