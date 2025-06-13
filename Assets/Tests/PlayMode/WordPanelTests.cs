using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;


public class WordPanelTests
{
    WordObjectivePanelManager wordObjectivePanelManager;
    GameObject wordPanel;
    TextMeshProUGUI goalWordText;
    [SetUp]
    public void MySetUp()
    {
        
        GameObject goalWord = new GameObject();
        goalWord.name = "GoalWord";
        goalWordText = goalWord.AddComponent<TextMeshProUGUI>();
        wordPanel = new GameObject();

        goalWord.transform.SetParent(wordPanel.transform, false);
        wordObjectivePanelManager = wordPanel.AddComponent<WordObjectivePanelManager>();

    }

    [TearDown]
    public void MyTearDown()
    {
        Object.Destroy(wordPanel);
    }

    [Test]
    public void SetGoalWordTest([ValueSource(nameof(WordTestCases))] string word)
    {
        wordObjectivePanelManager.SetGoalWord(null, word);
        Assert.AreEqual(word, goalWordText.text);
    }

    [Test]
    public void IsNextLetterTest([ValueSource(nameof(IsNextLetterTestCases))] NextLetterTestCases nextLetterTestCases)
    {
        string word = nextLetterTestCases.word;
        char letter = nextLetterTestCases.letter;
        int currentLetter = nextLetterTestCases.currentLetter;
        bool isCurrentLetter = nextLetterTestCases.isCurrentLetter;

        wordObjectivePanelManager.SetGoalWord(null, word);
        
        Assert.AreEqual(isCurrentLetter, wordObjectivePanelManager.IsNextLetter(letter, currentLetter));

    }

    [Test]
    public void UpdateGoalWordTest([ValueSource(nameof(UpdateGoalWordTestCases))]GoalWordTestCases goalWordTestCases)
    {
        string word = goalWordTestCases.word;
        string expectedWord = goalWordTestCases.expectedWord;

        wordObjectivePanelManager.SetGoalWord(null, word);

        foreach(char c in word)
        {
            wordObjectivePanelManager.UpdateGoalWord(null,c);
        }
        Assert.AreEqual(expectedWord, goalWordText.text);

    }

    [Test]
    public void ResetGoalWordTest([ValueSource(nameof(WordTestCases))] string word)
    {
        wordObjectivePanelManager.SetGoalWord(null , word);

        foreach (char c in word)
        {
            wordObjectivePanelManager.UpdateGoalWord(null, c);
        }
        wordObjectivePanelManager.ResetGoal(null, null);
        Assert.AreEqual (word, goalWordText.text);
    }

   

    private static IEnumerable UpdateGoalWordTestCases()
    {
        yield return new GoalWordTestCases 
        { 
            word = "Hola", 
            expectedWord = "<color=green>H</color><color=green>o</color><color=green>l</color><color=green>a</color>" 
        };

        yield return new GoalWordTestCases
        {
            word = "Eñe",
            expectedWord = "<color=green>E</color><color=green>ñ</color><color=green>e</color>"
        };
    }

    private static IEnumerable IsNextLetterTestCases()
    {
        string auxWord = "Hola";
        for(int i = 0; i < auxWord.Length; i++)
        {
            yield return new NextLetterTestCases { word = "Hola", letter = auxWord[i], currentLetter = i, isCurrentLetter = true };

        }

        yield return new NextLetterTestCases { word = "Eñe", letter = 'ñ', currentLetter = 1, isCurrentLetter = true };
        yield return new NextLetterTestCases { word = "Esdrújula", letter = 'ú', currentLetter = 4, isCurrentLetter = true };

        yield return new NextLetterTestCases { word = "Ejemplo", letter = 'j', currentLetter = 0, isCurrentLetter = false };
        yield return new NextLetterTestCases { word = "Ejemplo", letter = 'o', currentLetter= 2, isCurrentLetter = false };

    }

    

    private static IEnumerable WordTestCases()
    {
        yield return "Hola";
        yield return "Ejemplo";
        yield return "Eñe";
        yield return "Esdrújula";
    }

    public struct NextLetterTestCases
    {
        public string word;
        public char letter;
        public int currentLetter;
        public bool isCurrentLetter;
    }

    public struct GoalWordTestCases
    {
        public string word;
        public string expectedWord;
    }
}
