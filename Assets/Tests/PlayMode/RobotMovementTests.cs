using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class RobotMovementTests
{
    private GameObject robotPrefab;
    private GameObject robot;
    private RobotManager robotManager;

    [SetUp]
    public void MySetUp()
    {
        robotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Robot.prefab");
        robot = Object.Instantiate(robotPrefab, new Vector3(0,0,0), Quaternion.identity);
        robotManager = robot.GetComponent<RobotManager>();
    }

    [TearDown]
    public void MyTearDown()
    {
        Object.Destroy(robotManager.GetTargetPoint());
        Object.Destroy(robot);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestRobotMovement([ValueSource(nameof(RobotMovementTestCases))] RobotMovementTestCase testCase)
    {
        Vector3 endPoint = testCase.endPoint;
        bool collisionDetected = testCase.collisionDetected;
        Vector3 originalPosition = robot.transform.position;

        yield return robotManager.MoveRobot(endPoint, collisionDetected);

        if(collisionDetected )
        {
            Assert.AreEqual(originalPosition, robot.transform.position);
        }
        else
        {
            Assert.AreEqual(endPoint, robot.transform.position);
        }
    }

    [UnityTest]
    public IEnumerator TestRobotRotation([ValueSource(nameof(RobotRotationTestCases))] int angle)
    {
        int newAngle = angle + (int)robot.transform.rotation.eulerAngles.z;
        Quaternion newRotation = Quaternion.Euler(0, 0, newAngle);


        yield return robotManager.RotateRobot(angle);
      
        Assert.AreEqual(newRotation.eulerAngles, robot.transform.rotation.eulerAngles);
        
    }

    [UnityTest]
    public IEnumerator TestSpeedChange([ValueSource(nameof(ChangeSpeedTestCases))] float speed)
    {
        robotManager.ChangeSpeed(null, speed);
        yield return null;
        Assert.AreEqual(speed, robotManager.GetMovementSpeed());
    }

    [UnityTest]
    public IEnumerator TestPositionChange([ValueSource(nameof(RobotPositionTestCases))] Vector3 newPosition)
    {
        robotManager.ChangePosition(null, newPosition);
        yield return null;
        Assert.AreEqual(newPosition, robot.transform.position);
    }

    [UnityTest]
    public IEnumerator TestParametersRestart([ValueSource(nameof(RobotPositionTestCases))] Vector3 newPosition, [ValueSource(nameof(ChangeSpeedTestCases))] float speed)
    {
        Vector3 initialPosition = robot.transform.position;
        Quaternion initialRotation = robot.transform.rotation;

        float initialSpeed = robotManager.GetMovementSpeed();
        
        yield return robotManager.RotateRobot(90);
        robotManager.ChangePosition(null, newPosition);
        robotManager.ChangeSpeed(null, speed);
        

        yield return new WaitForSeconds(0.2f);

        robotManager.StopExecution(null, null);
        Assert.AreEqual(initialSpeed, robotManager.GetMovementSpeed());
        Assert.AreEqual(initialPosition, robot.transform.position);
        Assert.AreEqual(initialRotation.eulerAngles, robot.transform.rotation.eulerAngles);

    }

    private static IEnumerable RobotPositionTestCases()
    {
        for(int i = 1; i <=5;  i++)
        {
            for(int j = 1; j <=5; j++)
            {
                yield return new Vector3(i, j, 0);
            }
        }
    }

    private static IEnumerable ChangeSpeedTestCases()
    {
        for(float i = 0f;  i <=10; i++)
        {
            yield return i;
        }
    }

    private static IEnumerable RobotRotationTestCases()
    {
        for(int i = 90; i<=360; i += 90)
        {
            yield return i;
        }

        for(int i = -90; i>=-360; i -= 90)
        {
            yield return i;
        }

    }

    private static IEnumerable RobotMovementTestCases()
    {
        yield return new RobotMovementTestCase { endPoint = new Vector3(1, 0, 0), collisionDetected = false };
        yield return new RobotMovementTestCase { endPoint = new Vector3(1,1,0), collisionDetected = false };
        yield return new RobotMovementTestCase { endPoint = new Vector3(-1,0,0), collisionDetected = false };
        yield return new RobotMovementTestCase { endPoint = new Vector3(0,-1,0), collisionDetected = false };

        yield return new RobotMovementTestCase { endPoint = new Vector3(1, 0, 0), collisionDetected = true };
        yield return new RobotMovementTestCase { endPoint = new Vector3(1, 1, 0), collisionDetected = true };
        yield return new RobotMovementTestCase { endPoint = new Vector3(-1, 0, 0), collisionDetected = true };
        yield return new RobotMovementTestCase { endPoint = new Vector3(0, -1, 0), collisionDetected = true };
    }

    public struct RobotMovementTestCase
    {
        public Vector3 endPoint;
        public bool collisionDetected;
    }
}
