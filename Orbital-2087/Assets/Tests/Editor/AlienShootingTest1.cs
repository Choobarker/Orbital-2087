using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class AlienShootingTest 
{
	[Test]
    public void DamageTest()
    {
        EarthHealth earthHealth = new EarthHealth();

        float startHealth = EarthHealth.health;
        earthHealth.DamageTaken(null);
        float newHealth = EarthHealth.health;

        Assert.AreEqual(startHealth, newHealth + earthHealth.damageTaken);
    }
}
