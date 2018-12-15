using UnityEngine;
using System.Collections;

public static class TextureGenerator {

	public static Texture2D MapaTeksturaZKolor(Color[] mapaKolor, int szerX, int wysY) {
		Texture2D tekstura = new Texture2D (szerX, wysY);
		tekstura.filterMode = FilterMode.Point;
		tekstura.wrapMode = TextureWrapMode.Clamp;
		tekstura.SetPixels (mapaKolor);
		tekstura.Apply ();
		return tekstura;
	}


	public static Texture2D MapaTeksturaZWysY(float[,] wysMapY) {
		int szerX = wysMapY.GetLength (0);
		int wysY = wysMapY.GetLength (1);

		Color[] mapaKolor = new Color[szerX * wysY];
		for (int y = 0; y < wysY; y++) {
			for (int x = 0; x < szerX; x++) {
				mapaKolor [y * szerX + x] = Color.Lerp (Color.black, Color.white, wysMapY [x, y]);
			}
		}

		return MapaTeksturaZKolor (mapaKolor, szerX, wysY);
	}

}
