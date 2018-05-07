using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class AlienShootingTest {

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
