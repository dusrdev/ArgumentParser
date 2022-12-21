using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArgumentParser;

namespace ArgumentParser.Tests {
    [TestClass]
    public class ArgumentsTests {
        private static readonly Arguments Arguments = Parser.ParseArguments("--null \"\" --text abcd --validint 5 --validdouble 5.5")!;

        [TestMethod]
        public void GetIntegerValueTest_NotFound() {
            Assert.IsNull(Arguments.GetValueAsInteger("InvalidKey"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Null() {
            Assert.IsNull(Arguments.GetValueAsInteger("Null"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Text() {
            Assert.IsNull(Arguments.GetValueAsInteger("Text"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Valid() {
            Assert.AreEqual(5, Arguments.GetValueAsInteger("ValidInt"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_NotFound() {
            Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetValueAsInteger("InvalidKey", true));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_Null() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetValueAsInteger("Null", true));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_Text() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetValueAsInteger("Text", true));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_Valid() {
            Assert.AreEqual(5, Arguments.GetValueAsInteger("ValidInt", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_NotFound() {
            Assert.IsNull(Arguments.GetValueAsDouble("InvalidKey"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Null() {
            Assert.IsNull(Arguments.GetValueAsDouble("Null"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Text() {
            Assert.IsNull(Arguments.GetValueAsDouble("Text"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Valid() {
            Assert.AreEqual(5.5, Arguments.GetValueAsDouble("ValidDouble"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_NotFound() {
            Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetValueAsDouble("InvalidKey", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_Null() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetValueAsDouble("Null", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_Text() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetValueAsDouble("Text", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_Valid() {
            Assert.AreEqual(5.5, Arguments.GetValueAsDouble("ValidDouble", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_NotFound() {
            Assert.IsNull(Arguments.GetValueAsDecimal("InvalidKey"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Null() {
            Assert.IsNull(Arguments.GetValueAsDecimal("Null"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Text() {
            Assert.IsNull(Arguments.GetValueAsDecimal("Text"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Valid() {
            Assert.AreEqual(5.5m, Arguments.GetValueAsDecimal("ValidDouble"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_NotFound() {
            Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetValueAsDecimal("InvalidKey", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_Null() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetValueAsDecimal("Null", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_Text() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetValueAsDecimal("Text", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_Valid() {
            Assert.AreEqual(5.5m, Arguments.GetValueAsDecimal("ValidDouble", true));
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
        public void GetStringValueTest_FoundNull() {
            Assert.IsNull(Arguments.GetValueAsDecimal("Null"));
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
        public void Validate_ThrowsException_WhenKeyDoesntExist() {
            Assert.ThrowsException<KeyNotFoundException>(() => Arguments.Validate("InvalidKey"));
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
}