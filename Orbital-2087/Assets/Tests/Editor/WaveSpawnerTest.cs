using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class WaveSpawnerTest
{
	[Test]
	public void SinTest()
	{
		WaveSpawner ws = new WaveSpawner();
		
		// testData is a multidimentional double array with value pairs with the format
		// testData[x, 0] = test angle in degrees, testData[x, 1] correct sine value of angle
		double[,] testData = new double[9,2];

		testData[0, 0] = 5;
		testData[0, 1] = 0.0872;

		testData[1, 0] = 10;
		testData[1, 1] = 0.1736;

		testData[2, 0] = 20;
		testData[2, 1] = 0.3420;

		testData[3, 0] = 30;
		testData[3, 1] = 0.5;

		testData[4, 0] = 40;
		testData[4, 1] = 0.6428;

		testData[5, 0] = 50;
		testData[5, 1] = 0.7660;

		testData[6, 0] = 100;
		testData[6, 1] = 0.9848;

		testData[7, 0] = 150;
		testData[7, 1] = 0.5;

		testData[8, 0] = 200;
		testData[8, 1] = -0.3420;

		for(int i = 0; i < testData.GetLength(0); ++i)
		{
			double testAnswer = System.Math.Round(ws.Sin((float)testData[i, 0]), 4);

			Assert.AreEqual(testData[i, 1], testAnswer);
		}
	}

	[Test]
	public void DegreesToRadiansTest()
	{
		WaveSpawner ws = new WaveSpawner();

		// testData is a multidimentional array with double pairs with the format
		// testData[x][0] = test degree value, testData[x][1] = correct converted radian value
		double[,] testData = new double[12, 2];

		testData[0, 0] = 57.296;
		testData[0, 1] =  1;

		testData[1, 0] = 85.944;
		testData[1, 1] = 1.5;

		testData[2, 0] = 114.592;
		testData[2, 1] = 2;

		testData[3, 0] = 143.239;
		testData[3, 1] = 2.5;

		testData[4, 0] = 171.887;
		testData[4, 1] = 3;

		testData[5, 0] = 200.535;
		testData[5, 1] = 3.5;

		testData[6, 0] = 229.183;
		testData[6, 1] = 4;

		testData[7, 0] = 257.831;
		testData[7, 1] = 4.5;

		testData[8, 0] = 286.479;
		testData[8, 1] = 5;

		testData[9, 0] = 315.127;
		testData[9, 1] = 5.5;

		testData[10, 0] = 343.775;
		testData[10, 1] = 6;

		testData[11, 0] = 360;
		testData[11, 1] = 6.283;

		for(int i = 0; i < testData.GetLength(0); ++i)
		{
			double testAnswer = System.Math.Round(ws.DegreesToRadians(testData[i, 0]), 3);

			Assert.AreEqual(testData[i, 1], testAnswer);
		}		
	}
}
