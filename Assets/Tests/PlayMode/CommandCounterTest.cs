using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class CommandCounterTest
{
    GameObject containerComenzarPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Comandos/Containers/ContainerComenzar.prefab");
    GameObject containerRepetirPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Comandos/Containers/ContainerRepetir.prefab");
    GameObject commandSimplePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Comandos/Commands/CommandAvanzar.prefab");
    GameObject commandSimple;
    GameObject containerComenzar;
    GameObject containerRepetir;

    private GameObject panel;
    private CommandCounter counter;
    [SetUp]
    public void MySetUp()
    {
        panel = new GameObject();
        counter = panel.AddComponent<CommandCounter>();
        containerComenzar = Object.Instantiate(containerComenzarPrefab);
        containerRepetir = Object.Instantiate(containerRepetirPrefab);
        commandSimple = Object.Instantiate(commandSimplePrefab);
    }

    [TearDown]
    public void MyTearDown()
    {
        Object.Destroy(containerComenzar);
        Object.Destroy(containerRepetir);
        Object.Destroy(commandSimple);
        Object.Destroy(panel);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CounterTest([NUnit.Framework.Range(1,4)]int firstChildRow, [NUnit.Framework.Range(0, 4)] int secondChildRow)
    {
        containerRepetir.transform.SetParent(containerComenzar.GetComponent<Container>().GetContainerPanel().transform, false);
        for (int i = 1; i<firstChildRow;i++)
        {
            GameObject aux = Object.Instantiate(containerRepetirPrefab);
            aux.transform.SetParent(containerComenzar.GetComponent<Container>().GetContainerPanel().transform, false);

        }
        for(int i = 1; i <= secondChildRow; i++)
        {
            GameObject aux = Object.Instantiate(commandSimplePrefab);
            aux.transform.SetParent(containerRepetir.GetComponent<Container>().GetContainerPanel().transform, false);

        }

        containerComenzar.transform.SetParent(panel.transform, false);
        
        int expectedNumber = firstChildRow+secondChildRow+1;

        yield return null;
        Assert.AreEqual(expectedNumber, counter.GetDesktopCommandCount());
    }

    

    public struct CounterTestCases
    {
        public List<GameObject> commandList;
        public int expectedNumber;
    }
}
