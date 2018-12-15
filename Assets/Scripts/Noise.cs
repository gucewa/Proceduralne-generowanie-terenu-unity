using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] GenerujMapaSzum(int szumX, int szumY, int seed, float skala, int oktawy, float persistance, float lacunarity, Vector2 przest) {
		float[,] mapaSzum = new float[szumX,szumY];

		System.Random rand = new System.Random (seed);
		Vector2[] oktawyPrzest = new Vector2[oktawy];
		for (int i = 0; i < oktawy; i++) {
			float przestX = rand.Next (-100000, 100000) + przest.x;
			float przestY = rand.Next (-100000, 100000) + przest.y;
			oktawyPrzest [i] = new Vector2 (przestX, przestY);
		}

		if (skala <= 0) {
			skala = 0.0001f;
		}

		float maxSzumY = float.MinValue;
		float minSzumY = float.MaxValue;

		float polX = szumX / 2f;
		float polY = szumY / 2f;


		for (int y = 0; y < szumY; y++) {
			for (int x = 0; x < szumX; x++) {
		
				float amplituda = 1;
				float czest = 1;
				float wysSzumY = 0;

				for (int i = 0; i < oktawy; i++) {
					float probkaX = (x-polX) / skala * czest + oktawyPrzest[i].x;
					float probkaY = (y-polY) / skala * czest + oktawyPrzest[i].y;

					float wartoscPerlina = Mathf.PerlinNoise (probkaX, probkaY) * 2 - 1;
					wysSzumY += wartoscPerlina * amplituda;

					amplituda *= persistance;
					czest *= lacunarity;
				}

				if (wysSzumY > maxSzumY) {
					maxSzumY = wysSzumY;
				} else if (wysSzumY < minSzumY) {
					minSzumY = wysSzumY;
				}
				mapaSzum [x, y] = wysSzumY;
			}
		}

		for (int y = 0; y < szumY; y++) {
			for (int x = 0; x < szumX; x++) {
				mapaSzum [x, y] = Mathf.InverseLerp (minSzumY, maxSzumY, mapaSzum [x, y]);
			}
		}

		return mapaSzum;
	}

}
