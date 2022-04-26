using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrahedronMesh : MonoBehaviour
{
    private Vector3[] verts;  // the vertices of the mesh
	private int[] tris;       // the triangles of the mesh (triplets of integer references to vertices)
	private int ntris = 0;    // the number of triangles that have been created so far
    public int seed = 42;
	// Create a mesh and put multiple copies of it into the scene.

	void Start() {

		// call the routine that makes a mesh (a cube) from scratch
		Mesh my_mesh = CreateMyMesh();

		// make multiple copies of this mesh and place these copies in the scene

		// float k = 10.0f;
		int num_objects = 40;
		int count = 1;
		while (count!=41){
			float k1 = Random.Range(-(float) seed, (float) seed);
			float k2 = Random.Range(0f, (float) seed*2);
			float k3 = Random.Range(-(float) seed/2, (float) seed/2);

			float t = count / (float) num_objects;    // t in range [0,1]
			float x = k1 * (2 * t - 1);            // x translate in range of [-k1,k1]
			float y = k2 * (2 * t - 1);
			float z = k3 * (2 * t - 1);			// z translate in range of [-k2,k2]
			
			// radius of the sphere = 12.0
			if (-12f<x && x<12f && -12f<y && y<12f && -12<z && z<12f){ 
				// create a new GameObject and give it a MeshFilter and a MeshRenderer
				GameObject s = new GameObject(count.ToString("Object 0"));
				s.AddComponent<MeshFilter>();
				s.AddComponent<MeshRenderer>();
			
				s.transform.position = new Vector3 (x, y, z);  // move this object to a new location
				s.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);  // shrink the object

				// associate my mesh with this object
				s.GetComponent<MeshFilter>().mesh = my_mesh;

				// change the color of the object
				Renderer rend = s.GetComponent<Renderer>();

				rend.material.color = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));  // random colors
				count++;
			}
			else{
				count++;
			}
		}
	}

	Mesh CreateMyMesh() {
		
		// create a mesh object
		Mesh mesh = new Mesh();

		// vertices
		int num_verts = 16;
		verts = new Vector3[num_verts];

		// vertices for triangles and sqaure at the bottom of tetrahedron

		verts[0] = new Vector3 (0, 3, 0);
		verts[1] = new Vector3 (1, 0, 1);
		verts[2] = new Vector3 (1, 0, -1);

        verts[3] = new Vector3 (0, 3, 0);
		verts[4] = new Vector3 (1, 0, -1);
		verts[5] = new Vector3 (-1, 0, -1);

        verts[6] = new Vector3 (0, 3, 0);
		verts[7] = new Vector3 (-1, 0, -1);
		verts[8] = new Vector3 (-1, 0, 1);

        verts[9] = new Vector3 (0, 3, 0);
		verts[10] = new Vector3 (-1, 0, 1);
		verts[11] = new Vector3 (1, 0, 1);

        verts[12] = new Vector3 (1, 0, -1);
		verts[13] = new Vector3 (1, 0, 1);
		verts[14] = new Vector3 (-1, 0, 1);
		verts[15] = new Vector3 (-1, 0, -1);
		
		int num_tris = 6;  
		tris = new int[num_tris * 3];  // need 3 vertices per triangle
		
		MakeQuad (12, 13, 14, 15);
		
		MakeTri (0, 1, 2);
        MakeTri (3, 4, 5);
        MakeTri (6, 7, 8);
        MakeTri (9, 10, 11);
		

		// save the vertices and triangles in the mesh object
		mesh.vertices = verts;
		mesh.triangles = tris;

		mesh.RecalculateNormals();  // automatically calculate the vertex normals

		return (mesh);
	}

	// make a triangle from three vertex indices (clockwise order)
	void MakeTri(int i1, int i2, int i3) {
		int index = ntris * 3;  // figure out the base index for storing triangle indices
		ntris++;

		tris[index]     = i1;
		tris[index + 1] = i2;
		tris[index + 2] = i3;
	}

	// make a quadrilateral from four vertex indices (clockwise order)
	void MakeQuad(int i1, int i2, int i3, int i4) {
		MakeTri (i1, i2, i3);
		MakeTri (i1, i3, i4);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
