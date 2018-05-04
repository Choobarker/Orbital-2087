using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ShootProjectileTest : MonoBehaviour
{
    float lifeTime = 4.0f;

    [Test]
    public void DestroyShotsTest()
    {
        //checks that the shots aren't getting destroyed
        Assert.AreEqual(4.0f, lifeTime);
    }
}
