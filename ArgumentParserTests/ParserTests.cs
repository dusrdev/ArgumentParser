using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser.Tests;

[TestClass]
public class ParserTests {
    [TestMethod]
    public void SplitTest_Empty() {
        Assert.IsTrue(!Parser.Split("").Any());
    }

    [TestMethod]
    public void SplitTest_WithQuotes() {
        string input = "my name is \"Tacitus Kilgore\"";
        var expected = new[] { "my", "name", "is", "Tacitus Kilgore" };
        CollectionAssert.AreEqual(expected, Parser.Split(input).ToArray());
    }

    [TestMethod]
    public void SplitTest_WithoutQuotes() {
        string input = "my name is Tacitus Kilgore";
        var expected = new[] { "my", "name", "is", "Tacitus", "Kilgore" };
        CollectionAssert.AreEqual(expected, Parser.Split(input).ToArray());
    }

    [TestMethod]
    public void ParseArgumentsTest_Empty() {
        Assert.ThrowsException<ArgumentException>(() => Parser.ParseArguments(Parser.Split("")));
    }

    [TestMethod]
    public void ParseArgumentsTest_Quotes() {
        string input = "--names \"Tacitus Kilgore\"";
        var expected = new Dictionary<string, string> { { "names", "Tacitus Kilgore" } };
        CollectionAssert.AreEqual(expected, Parser.ParseArguments(input));
    }

    [TestMethod]
    public void ParseArgumentsTest_CommandAndParameterWithValues() {
        string input = "command -p value1 --param2 value2";
        var expected = new Dictionary<string, string> {
            { "command", string.Empty },
            { "p", "value1" },
            { "param2", "value2" }
        };
        CollectionAssert.AreEqual(expected, Parser.ParseArguments(input));
    }

    [TestMethod]
    public void ParseArgumentsTest_CommandAndParameterWithoutValue() {
        string input = "command -p -f text.txt";
        var expected = new Dictionary<string, string> {
            { "command", string.Empty },
            { "p", string.Empty },
            { "f", "text.txt" }
        };
        CollectionAssert.AreEqual(expected, Parser.ParseArguments(input));
    }

    [TestMethod]
    public void ParseArgumentsTest_GeneralWithIEnumerable() {
        var input = Parser.Split("command -p -f text.txt");
        var expected = new Dictionary<string, string> {
            { "command", string.Empty },
            { "p", string.Empty },
            { "f", "text.txt" }
        };
        CollectionAssert.AreEqual(expected, Parser.ParseArguments(input));
    }
}