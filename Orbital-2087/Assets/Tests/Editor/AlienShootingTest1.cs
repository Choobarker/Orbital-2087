﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class AlienShootingTest 
{
	[Test]
    public void DamageTest()
    {
        EarthHealth earthHealth = new EarthHealth();

        float startHealth = earthHealth.health;
        earthHealth.DamageTaken(null);
        float newHealth = earthHealth.health;

        Assert.AreEqual(startHealth, newHealth + earthHealth.damageTaken);
    }
}