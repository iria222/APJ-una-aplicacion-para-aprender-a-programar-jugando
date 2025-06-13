using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Level1Tests
{
    private GameObject robot;
    private GameObject escritorioPanel;
    private GameObject panelVictoria;

    private GameObject containerComenzarPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Comandos/Containers/ContainerComenzar.prefab");
    private GameObject commandAvanzarPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Comandos/Commands/CommandAvanzar.prefab");
    private GameObject commandRetrocederPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Comandos/Commands/CommandRetroceder.prefab");
    private GameObject commandGirarIzqPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Comandos/Commands/CommandGirarIzquierda.prefab");

    private GameObject containerComenzar;

    [SetUp]
    public void MySetUp()
    {
        SceneManager.LoadScene("Assets/Scenes/Levels/Nivel 1.unity");
        
        //containerComenzar = Object.Instantiate(containerComenzarPrefab);
        
        //containerComenzar.transform.SetParent(escritorioPanel.transform);
    }

    [TearDown]
    public void MyTearDown()
    {
        Object.Destroy(containerComenzar);
        panelVictoria = GameObject.Find("CanvasWin");

        panelVictoria.GetComponent<Canvas>().enabled=false;
        robot = GameObject.Find("Robot");
        robot.GetComponent<RobotManager>().StopExecution(null, null);
    }

    [UnityTest]
    public IEnumerator CommandRobotConnectionTest()
    {
        containerComenzar = Object.Instantiate(containerComenzarPrefab);

        yield return null;
        escritorioPanel = GameObject.Find("EscritorioScrollContent");
        robot = GameObject.Find("Robot");

        Vector3 initialPos = robot.transform.position;

        containerComenzar.transform.SetParent(escritorioPanel.transform, true);
        for (int i = 0; i < 2; i++)
        {
            GameObject commandAvanzar = Object.Instantiate(commandAvanzarPrefab);
            commandAvanzar.transform.SetParent(containerComenzar.GetComponent<Container>().GetContainerPanel().transform, true);
            commandAvanzar.transform.SetAsLastSibling();
        }
        yield return null;
        yield return containerComenzar.GetComponent<ContainerComenzar>().StartExecution();
        yield return null;

        Assert.AreEqual(initialPos+ new Vector3(0,2,0), robot.transform.position);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator VictoryTest()
    {
        containerComenzar = Object.Instantiate(containerComenzarPrefab);

        yield return null;
        panelVictoria = GameObject.Find("CanvasWin");
        escritorioPanel = GameObject.Find("EscritorioScrollContent");
        
        containerComenzar.transform.SetParent(escritorioPanel.transform, true);
        for(int i =0; i<2; i++)
        {
            GameObject commandAvanzar = Object.Instantiate(commandAvanzarPrefab);
            commandAvanzar.transform.SetParent(containerComenzar.GetComponent<Container>().GetContainerPanel().transform, true);
            commandAvanzar.transform.SetAsLastSibling();
        }
        GameObject commandGirarIzq = Object.Instantiate(commandGirarIzqPrefab);
        commandGirarIzq.transform.SetParent(containerComenzar.GetComponent<Container>().GetContainerPanel().transform, true);
        commandGirarIzq.transform.SetAsLastSibling();

        for (int i = 0; i < 2; i++)
        {
            GameObject commandRetroceder = Object.Instantiate(commandRetrocederPrefab);
            commandRetroceder.transform.SetParent(containerComenzar.GetComponent<Container>().GetContainerPanel().transform, true);
            commandRetroceder.transform.SetAsLastSibling();
        }

        yield return null;
        yield return containerComenzar.GetComponent<ContainerComenzar>().StartExecution();
        yield return null;

        Assert.AreEqual(true, panelVictoria.GetComponent<Canvas>().enabled);
    }

    [UnityTest]
    public IEnumerator ObstacleHittedTest()
    {
        containerComenzar = Object.Instantiate(containerComenzarPrefab);

        yield return null;
        robot = GameObject.Find("Robot");
        if(robot == null)
        {
            Debug.Log("Robot null");
        }
        escritorioPanel = GameObject.Find("EscritorioScrollContent");
        Vector3 originalPos = robot.transform.position;

        containerComenzar.transform.SetParent(escritorioPanel.transform, true);

        for (int i = 0; i < 2; i++)
        {
            GameObject commandRetroceder = Object.Instantiate(commandRetrocederPrefab);
            commandRetroceder.transform.SetParent(containerComenzar.GetComponent<Container>().GetContainerPanel().transform, true);
            commandRetroceder.transform.SetAsLastSibling();
        }

        yield return null;
        yield return containerComenzar.GetComponent<ContainerComenzar>().StartExecution();
        yield return null;

        Assert.AreEqual(originalPos, robot.transform.position);
    }
}
