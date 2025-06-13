using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TargetPointTest
{
    private GameObject target;
    private TargetPointManager targetManager;

    [SetUp]
    public void MySetUp()
    {
        target = new GameObject();
        targetManager = target.AddComponent<TargetPointManager>();
    }

    [TearDown]
    public void MyTearDown()
    {
        Object.Destroy(target);
    }

    
    [Test]
    public void MoveTargetPoint([ValueSource(nameof(TestCases))] TargetPointTestCase testCase)
    {
        int steps = testCase.steps;
        Vector3 direction = testCase.direction;

        Vector3 expectedPos = target.transform.position +steps*direction;

        targetManager.MoveTargetPoint(steps, direction);
        Assert.AreEqual(expectedPos, target.transform.position);
        
    }

    [Test]
    public void CheckTargetPointCollision([ValueSource(nameof(TestCases))] TargetPointTestCase testCase)
    {
        int steps = testCase.steps;
        Vector3 direction = testCase.direction;

        targetManager.MoveTargetPoint(steps, direction);


        Assert.AreEqual(false, targetManager.GetCollisionDetected());

    }


    private static IEnumerable TestCases()
    {
        Vector3 vectorNorth = new Vector3(0, 1, 0);
        Vector3 vectorEast = new Vector3(-1, 0, 0);
        Vector3 vectorSouth = new Vector3(0, -1, 0);
        Vector3 vectorWest = new Vector3(1, 0, 0);

        yield return new TargetPointTestCase { steps = 1, direction = vectorNorth };
        yield return new TargetPointTestCase { steps = 1, direction = vectorSouth };
        yield return new TargetPointTestCase { steps = 1, direction = vectorWest};
        yield return new TargetPointTestCase { steps = 1, direction = vectorEast };

        yield return new TargetPointTestCase { steps = 2, direction = vectorNorth };
        yield return new TargetPointTestCase { steps = 2, direction = vectorSouth };
        yield return new TargetPointTestCase { steps = 2, direction = vectorWest };
        yield return new TargetPointTestCase { steps = 2, direction = vectorEast };

        yield return new TargetPointTestCase { steps = 3, direction = vectorNorth };
        yield return new TargetPointTestCase { steps = 3, direction = vectorSouth };
        yield return new TargetPointTestCase { steps = 3, direction = vectorWest };
        yield return new TargetPointTestCase { steps = 3, direction = vectorEast };
    }

    public struct TargetPointTestCase
    {
        public int steps;
        public Vector3 direction;

    }

}
