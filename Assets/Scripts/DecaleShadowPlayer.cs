using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SamDriver.Decal;

public class DecaleShadowPlayer : MonoBehaviour
{
    public DecalMesh DM;
    public GameObject[] MeshObjects;
    public int MeshMaxLimit;
    public MeshFilter[] PossibleFloors;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> tmp = new List<GameObject>();
        int cpt = 0;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ReceiveShadow"))
        {
            if (g.GetComponent<MeshFilter>())
            {
                tmp.Add(g);
                cpt++;
            }
        }
        MeshObjects = new GameObject[cpt];
        cpt = 0;
        foreach (GameObject g in tmp)
        {
            MeshObjects[cpt] = g;
            cpt++;
        }
        
        /*
        for (int i = 0; i < MeshObjects.Length; i++)
        {
            DM.MeshesToProjectAgainst.Add(MeshObjects[i].GetComponent<MeshFilter>());
        }
        */

        PossibleFloors = new MeshFilter[MeshMaxLimit];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
