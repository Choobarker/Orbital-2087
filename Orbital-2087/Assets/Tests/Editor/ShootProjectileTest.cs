﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ShootProjectileTest : MonoBehaviour
{
    GameObject shot;

    [Test]
    public void DestroyShotsTest()
    {
        PlayerWeapon sp = new PlayerWeapon();
        shot = sp.CreateShot();
        StartCoroutine("Waiting");
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2);
        Assert.IsNotNull(shot);
        yield return new WaitForSeconds(2);
        Assert.IsNull(shot);
    }
}
