using System;
using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class AdditionPanelTests
{
    private CalculationPanelManager calculationPanelManager;
    private GameObject calculationPanel;
    private TextMeshProUGUI goalText;
    private TextMeshProUGUI currentNumberText;

    [SetUp]
    public void MySetUp()
    {
        calculationPanel = new GameObject();
        GameObject goal = new GameObject();
        GameObject currentNumber = new GameObject();
        goal.name = "Objetivo";
        currentNumber.name = "Suma";
        goal.transform.SetParent(calculationPanel.transform, false);
        currentNumber.transform.SetParent(calculationPanel.transform, false);

        goalText = goal.AddComponent<TextMeshProUGUI>();
        currentNumberText = currentNumber.AddComponent<TextMeshProUGUI>();

        calculationPanelManager = calculationPanel.AddComponent<CalculationPanelManager>();

    }

    [TearDown]
    public void MyTearDown()
    {
        UnityEngine.Object.Destroy(calculationPanel);
    }

    [Test]
    public void ChangeGoalNumberTest([ValueSource(nameof(NumberTestCases))] int number)
    {
        calculationPanelManager.ChangeGoalNumber(null, number);
        Assert.AreEqual(number.ToString(), goalText.text);
    }

    [Test]
    public void AddNumberTest([ValueSource(nameof(NumberTestCases))] int additionNumber)
    {
        calculationPanelManager.AddNumber(null, additionNumber);
        calculationPanelManager.AddNumber(null, additionNumber-1);

        Assert.AreEqual((additionNumber+additionNumber-1).ToString(), currentNumberText.text);
    }

    [Test]
    public void ResetCurrentNumberTest([ValueSource(nameof(NumberTestCases))] int additionNumber)
    {
        calculationPanelManager.AddNumber(null, additionNumber);
        calculationPanelManager.ResetCurrentNumber(null, null);

        Assert.AreEqual(0.ToString(), currentNumberText.text);

    }

    private static IEnumerable NumberTestCases()
    {
        for(int i = 1; i <= 10; i++)
        {
            yield return i;
        }
    }
}
