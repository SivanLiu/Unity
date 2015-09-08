﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class SetPivot : EditorWindow{

	Vector3 p; //Pivot value -1..1, calculated from Mesh bounds
	Vector3 last_p; //Last used pivot
	
	GameObject obj; //Selected object in the Hierarchy
	MeshFilter meshFilter; //Mesh Filter of the selected object
	Mesh mesh; //Mesh of the selected object
	Collider col; //Collider of the selected object
	
	bool pivotUnchanged; //Flag to decide when to instantiate a copy of the mesh

    [MenuItem("GameObject/SetPivot")] //Place the Set Pivot menu item in the GameObject menu
    static void Init () {
        SetPivot window = (SetPivot)EditorWindow.GetWindow (typeof (SetPivot));
		window.RecognizeSelectedObject(); //Initialize the variables by calling RecognizeSelectedObject on the class instance
        window.Show ();
    }
	
	void OnGUI() {
		if(obj) {
			if(mesh) {
				p.x = EditorGUILayout.Slider("X", p.x, -1.0f, 1.0f);
				p.y = EditorGUILayout.Slider("Y", p.y, -1.0f, 1.0f);
				p.z = EditorGUILayout.Slider("Z", p.z, -1.0f, 1.0f);
				if(p != last_p) { //Detects user input on any of the three sliders
					//Only create instance of mesh when user changes pivot
					if(pivotUnchanged) mesh = meshFilter.mesh; pivotUnchanged = false;
					UpdatePivot();
					last_p = p;
				}
				if(GUILayout.Button("Center")) { //Set pivot to the center of the mesh bounds
					//Only create instance of mesh when user changes pivot
					if(pivotUnchanged) mesh = meshFilter.mesh; pivotUnchanged = false;
					p = Vector3.zero;
					UpdatePivot();
					last_p = p;
				}
				GUILayout.Label("Boundsmesh.bounds.ToString()");
			} else {
				GUILayout.Label("Selectedct does not have a Mesh specified.");
			}
		} else {
			GUILayout.Label("Noct selected in Hierarchy.");
		}
	}
	
	//Achieve the movement of the pivot by moving the transform position in the specified direction
	//and then moving all vertices of the mesh in the opposite direction back to where they were in world-space
	void UpdatePivot() { 
		Vector3 diff = Vector3.Scale(mesh.bounds.extents, last_p - p); //Calculate difference in 3d position
		obj.transform.position -= Vector3.Scale(diff, obj.transform.localScale); //Move object position
		//Iterate over all vertices and move them in the opposite direction of the object position movement
		Vector3[] verts = mesh.vertices; 
		for(int i=0; i<verts.Length; i++) {
			verts[i] += diff;
		}
		mesh.vertices = verts; //Assign the vertex array back to the mesh
		mesh.RecalculateBounds(); //Recalculate bounds of the mesh, for the renderer's sake
		//The 'center' parameter of certain colliders needs to be adjusted
		//when the transform position is modified
		if(col) {
			if(col is BoxCollider) {
				((BoxCollider) col).center += diff;
			} else if(col is CapsuleCollider) {
				((CapsuleCollider) col).center += diff;
			} else if(col is SphereCollider) {
				((SphereCollider) col).center += diff;
			}
		}
	}
	
	//Look at the object's transform position in comparison to the center of its mesh bounds
	//and calculate the pivot values for xyz
	void UpdatePivotVector() {
		Bounds b = mesh.bounds;
		Vector3 offset = -1 * b.center;
		p = last_p = new Vector3(offset.x / b.extents.x, offset.y / b.extents.y, offset.z / b.extents.z);
	}
	
	//When a selection change notification is received
	//recalculate the variables and references for the new object
	void OnSelectionChange() {
		RecognizeSelectedObject();
	}
	
	//Gather references for the selected object and its components
	//and update the pivot vector if the object has a Mesh specified
	void RecognizeSelectedObject() {
		Transform t = Selection.activeTransform;
		obj = t ? t.gameObject : null;
		if(obj) {
			meshFilter = obj.GetComponent(typeof(MeshFilter)) as MeshFilter;
			mesh = meshFilter ? meshFilter.sharedMesh : null;
			if(mesh)
				UpdatePivotVector();
			col = obj.GetComponent(typeof(Collider)) as Collider;
			pivotUnchanged = true;
		} else {
			mesh = null;
		}
	}
}
