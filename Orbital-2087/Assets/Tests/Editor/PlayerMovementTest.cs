using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerMovementTest
{
    [Test]
    public void MovementTest()
    {
        PlayerMovement pm = new PlayerMovement();

        //Compares moveVar loactions to x,y coordinates
        //testata is the known locations
        //testPos is the moveVar
        Vector3[] testData = new Vector3[8];
        float[] testPos = new float[8];

        testData[0] = new Vector3(0, 3, 0);
        testData[1] = new Vector3(2.1f, 2.1f, 0);
        testData[2] = new Vector3(3, 0, 0);
        testData[3] = new Vector3(2.1f, -2.1f, 0);
        testData[4] = new Vector3(0, -3, 0);
        testData[5] = new Vector3(-2.1f, -2.1f, 0);
        testData[6] = new Vector3(-3, 0, 0);
        testData[7] = new Vector3(-2.1f, 2.1f, 0);

        testPos[0] = 0;
        testPos[1] = 0.785f;
        testPos[2] = 1.57f;
        testPos[3] = 2.37f;
        testPos[4] = 3.14f;
        testPos[5] = 3.92f;
        testPos[6] = 4.71f;
        testPos[7] = 5.49f;

        for (int i = 0; i < testData.GetLength(0); ++i)
        {
            Vector3 testAnswer = pm.getNextLocation(testPos[i]);

            Assert.AreEqual(testAnswer.x, testData[i].x, 0.06);
            Assert.AreEqual(testAnswer.y, testData[i].y, 0.06);
        }
    }
}
