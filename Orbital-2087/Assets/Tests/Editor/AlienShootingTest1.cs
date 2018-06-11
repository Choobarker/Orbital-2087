using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class AlienShootingTest 
{
	[Test]
    public void DamageTest()
    {
        float damage = 10;
        EarthHealth earthHealth = new EarthHealth();

        float startHealth = earthHealth.GetHealth();
        earthHealth.TakeDamage(damage);
        float newHealth = earthHealth.GetHealth();

        Assert.AreEqual(startHealth, newHealth + damage);
    }
}
