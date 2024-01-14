using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sagittarius.Tests;

[TestClass]
public class ArgumentsTests {
    private static readonly Arguments Arguments = Parser.ParseArguments("--null \"\" --text abcd --validint 5 --validdouble 5.5")!;

    [TestMethod]
    public void GetIntegerValueTest_NotFound() {
        Assert.IsNull(Arguments.GetValue<int>("InvalidKey"));
    }

    [TestMethod]
    public void GetIntegerValueTest_Null() {
        Assert.IsNull(Arguments.GetValue<int>("Null"));
    }

    [TestMethod]
    public void GetIntegerValueTest_Text() {
        Assert.IsNull(Arguments.GetValue<int>("Text"));
    }

    [TestMethod]
    public void GetIntegerValueTest_Valid() {
        Assert.AreEqual(5, Arguments.GetValue<int>("ValidInt"));
    }

    [TestMethod]
    public void GetIntegerValueTest_Throw_NotFound() {
        Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetValue<int>("InvalidKey", true));
    }

    [TestMethod]
    public void GetIntegerValueTest_Throw_Null() {
        Assert.ThrowsException<ArgumentException>(() => Arguments.GetValue<int>("Null", true));
    }

    [TestMethod]
    public void GetIntegerValueTest_Throw_Text() {
        Assert.ThrowsException<ArgumentException>(() => Arguments.GetValue<int>("Text", true));
    }

    [TestMethod]
    public void GetIntegerValueTest_Throw_Valid() {
        Assert.AreEqual(5, Arguments.GetValue<int>("ValidInt", true));
    }

    [TestMethod]
    public void GetStringValueTest_NotFound() {
        Assert.IsNull(Arguments.GetValue("InvalidKey"));
    }

    [TestMethod]
    public void GetStringValueTest_Found() {
        Assert.AreEqual("abcd", Arguments.GetValue("Text"));
    }

    [TestMethod]
    public void GetStringValueTest_NotFound_Throw() {
        Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetValue("InvalidKey", true));
    }

    [TestMethod]
    public void GetStringValueTest_FoundNull_Throw() {
        Assert.ThrowsException<ArgumentException>(() => Arguments.GetValue("Null", true));
    }

    [TestMethod]
    public void Validate_ThrowsException_WhenValueIsInvalid() {
        Assert.ThrowsException<ArgumentException>(() => Arguments.Validate("Text", v => v.All(char.IsDigit), "Value must be numeric"));
    }

    [TestMethod]
    public void GetInnerDictionaryTest() {
        var inner = Arguments.GetInnerDictionary();
        Assert.IsNotNull(inner);
        var expected = new Dictionary<string, string> {
            { "null", string.Empty },
            { "text", "abcd" },
            { "validint", "5" },
            { "validdouble", "5.5" },
        };
        CollectionAssert.AreEqual(expected, inner);
    }

    [TestMethod]
    public void ForwardPositionalArgumentsTest() {
        var str = "command help";
        var args = Parser.ParseArguments(str);
        args!.ForwardPositionalArguments();
        Assert.AreEqual("help", args!.GetValue("0")!);
    }
}