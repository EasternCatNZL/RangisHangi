using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
	}

    void GetTouchInput()
    {
        if(Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                    {
                        //get the tri in mesh that was hit
                        int hitTri = rayHit.triangleIndex;

                        //seperate clicked tri
                        SeperateTri(hitTri);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0)/* && !mouseDown*/)
        {
            //print("Mouse pressed");
            //mouseDown = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                //get the tri in mesh that was hit
                int hitTri = rayHit.triangleIndex;

                //seperate clicked tri
                SeperateTri(hitTri);
            }
        }
    }

    void CheckInput()
    {
        GetTouchInput();
        GetMouseInput();
    }

    void SeperateTri(int index)
    {
        //destroy the mesh collider on the whole object
        //need this step as removing a piece changes the mesh
        Destroy(gameObject.GetComponent<MeshCollider>());
        //get the mesh
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        //create int arrays that represent the new and old meshes
        int[] oldTris = mesh.triangles;
        int[] newTris = new int[mesh.triangles.Length - 3];
        int[] peeledTris = new int[3];
        //tracking ints for index when readding tris to new mesh
        int i = 0;
        int j = 0;
        while (j < mesh.triangles.Length)
        {
            //if this tri is still part of mesh
            if(j != index * 3)
            {
                newTris[i++] = oldTris[j++];
                newTris[i++] = oldTris[j++];
                newTris[i++] = oldTris[j++];
            }
            //else it is part of new mesh
            else
            {
                peeledTris[0] = oldTris[j++];
                peeledTris[1] = oldTris[j++];
                peeledTris[2] = oldTris[j++];
            }
        }
        //reattach new mesh and collider to current object
        transform.GetComponent<MeshFilter>().mesh.triangles = newTris;
        gameObject.AddComponent<MeshCollider>();
        //make peeled off tris into own object
        GameObject peeledOff = new GameObject("Peeled Off Stuff");
        MeshFilter peeledMesh = peeledOff.AddComponent<MeshFilter>();
        //set up vertices
        peeledMesh.mesh.vertices = new Vector3[9];
        peeledMesh.mesh.triangles = peeledTris;
        peeledMesh.mesh.RecalculateBounds();
        peeledOff.AddComponent<MeshCollider>();
        //add a rigidbody to allow new object to fall away
        peeledOff.AddComponent<Rigidbody>();
        
    }
}
