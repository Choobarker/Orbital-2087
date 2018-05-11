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

        float startHealth = earthHealth.GetHealth();
        earthHealth.TakeDamage(null);
        float newHealth = earthHealth.GetHealth();

        Assert.AreEqual(startHealth, newHealth + earthHealth.damageTaken);
    }
}
