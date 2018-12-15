using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

	public Renderer renderTekstur;
	public MeshFilter filtrMesh;
	public MeshRenderer renderMesh;

	public void RysujTekstura(Texture2D tekstura) {
		renderTekstur.sharedMaterial.mainTexture = tekstura;
		renderTekstur.transform.localScale = new Vector3 (tekstura.width, 1, tekstura.height);
	}

	public void RysujMesh(MeshData meshInfo, Texture2D tekstura) {
		filtrMesh.sharedMesh = meshInfo.GenerujMesh ();
		renderMesh.sharedMaterial.mainTexture = tekstura;
	}

}
