using UnityEngine;
using System.Collections;

public static class MeshGenerator {

	public static MeshData GenerujMeshTeren(float[,] wysY, float wysMnozY, AnimationCurve wysKrzywY, int poziomDetali) {
		int szerX = wysY.GetLength (0);
		int wys = wysY.GetLength (1);
		float lewyGornyX = (szerX - 1) / -2f;
		float lewyGornyZ = (wys - 1) / 2f;

		int meshUpraszczanie = (poziomDetali == 0)?1:poziomDetali * 2;
		int wierzcholkiNaLinie = (szerX - 1) / meshUpraszczanie + 1;

		MeshData meshData = new MeshData (wierzcholkiNaLinie, wierzcholkiNaLinie);
		int indexWierzch = 0;

		for (int y = 0; y < wys; y += meshUpraszczanie) {
			for (int x = 0; x < szerX; x += meshUpraszczanie) {
				meshData.wierzcholki [indexWierzch] = new Vector3 (lewyGornyX + x, wysKrzywY.Evaluate(wysY [x, y]) * wysMnozY, lewyGornyZ - y);
				meshData.uvs [indexWierzch] = new Vector2 (x / (float)szerX, y / (float)wys);

				if (x < szerX - 1 && y < wys - 1) {
					meshData.DodajTrojkat (indexWierzch, indexWierzch + wierzcholkiNaLinie + 1, indexWierzch + wierzcholkiNaLinie);
					meshData.DodajTrojkat (indexWierzch + wierzcholkiNaLinie + 1, indexWierzch, indexWierzch + 1);
				}

				indexWierzch++;
			}
		}

		return meshData;

	}
}

public class MeshData {
	public Vector3[] wierzcholki;
	public int[] trojkaty;
	public Vector2[] uvs;

	int indexTrojkata;

	public MeshData(int meshSzerX, int meshWysY) {
		wierzcholki = new Vector3[meshSzerX * meshWysY];
		uvs = new Vector2[meshSzerX * meshWysY];
		trojkaty = new int[(meshSzerX-1)*(meshWysY-1)*6];
	}

	public void DodajTrojkat(int a, int b, int c) {
		trojkaty [indexTrojkata] = a;
		trojkaty [indexTrojkata + 1] = b;
		trojkaty [indexTrojkata + 2] = c;
		indexTrojkata += 3;
	}

	public Mesh GenerujMesh() {
		Mesh mesh = new Mesh ();
		mesh.vertices = wierzcholki;
		mesh.triangles = trojkaty;
		mesh.uv = uvs;
		mesh.RecalculateNormals ();
		return mesh;
	}

}