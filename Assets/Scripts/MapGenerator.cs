using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public enum TRysuj {MapaSzum, MapaKolor, Mesh};
	public TRysuj tRysuj;

	const int mapChunkSize = 241;
	[Range(0,6)]
	public int poziomDetali;
	public float skalaSzum;

	public int oktawy;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 przest;

	public float meshMnozY;
	public AnimationCurve meshKrzywY;

	public bool autoUpdate;

	public TypTerenu[] regions;

	public void GenerujMapa() {
		float[,] mapaSzum = Noise.GenerujMapaSzum (mapChunkSize, mapChunkSize, seed, skalaSzum, oktawy, persistance, lacunarity, przest);

		Color[] mapaKolor = new Color[mapChunkSize * mapChunkSize];
		for (int y = 0; y < mapChunkSize; y++) {
			for (int x = 0; x < mapChunkSize; x++) {
				float aktualY = mapaSzum [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (aktualY <= regions [i].wysY) {
						mapaKolor [y * mapChunkSize + x] = regions [i].kolor;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (tRysuj == TRysuj.MapaSzum) {
			display.RysujTekstura (TextureGenerator.MapaTeksturaZWysY (mapaSzum));
		} else if (tRysuj == TRysuj.MapaKolor) {
			display.RysujTekstura (TextureGenerator.MapaTeksturaZKolor (mapaKolor, mapChunkSize, mapChunkSize));
		} else if (tRysuj == TRysuj.Mesh) {
			display.RysujMesh (MeshGenerator.GenerujMeshTeren (mapaSzum, meshMnozY, meshKrzywY, poziomDetali), TextureGenerator.MapaTeksturaZKolor (mapaKolor, mapChunkSize, mapChunkSize));
		}
	}

	void OnValidate() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (oktawy < 0) {
			oktawy = 0;
		}
	}
}

[System.Serializable]
public struct TypTerenu {
	public string nazwa;
	public float wysY;
	public Color kolor;
}