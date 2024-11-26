using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    [SerializeField] int width = 3;
    [SerializeField] int length = 3;
    [Tooltip("prefab of farmland")]
    public GameObject prefab;
    public static List<HarvestTile> tiles = new List<HarvestTile>();
    // Start is called before the first frame update
    public void Init()
    {
        tiles.Clear();
        //print(transform.position);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                GameObject g = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
                g.transform.localPosition = new Vector3(x, 0, y);
                tiles.Add(g.GetComponent<HarvestTile>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
