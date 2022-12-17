using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser.Tests {
    [TestClass]
    public class ParserExtensionsTests {
        private static Dictionary<string, string> Arguments = new(StringComparer.InvariantCultureIgnoreCase) {
            { "Null", string.Empty },
            { "Text", "abcd" },
            { "ValidInt", "5" },
            { "ValidDouble", "5.5" }
        };

        [TestMethod]
        public void GetIntegerValueTest_NotFound() {
            Assert.IsNull(Arguments.GetIntegerValue("InvalidKey"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Null() {
            Assert.IsNull(Arguments.GetIntegerValue("Null"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Text() {
            Assert.IsNull(Arguments.GetIntegerValue("Text"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Valid() {
            Assert.AreEqual(5, Arguments.GetIntegerValue("ValidInt"));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_NotFound() {
            Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetIntegerValue("InvalidKey", true));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_Null() {
            Assert.ThrowsException<ArgumentNullException>(() => Arguments.GetIntegerValue("Null", true));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_Text() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetIntegerValue("Text", true));
        }

        [TestMethod]
        public void GetIntegerValueTest_Throw_Valid() {
            Assert.AreEqual(5, Arguments.GetIntegerValue("ValidInt", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_NotFound() {
            Assert.IsNull(Arguments.GetDoubleValue("InvalidKey"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Null() {
            Assert.IsNull(Arguments.GetDoubleValue("Null"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Text() {
            Assert.IsNull(Arguments.GetDoubleValue("Text"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Valid() {
            Assert.AreEqual(5.5, Arguments.GetDoubleValue("ValidDouble"));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_NotFound() {
            Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetDoubleValue("InvalidKey", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_Null() {
            Assert.ThrowsException<ArgumentNullException>(() => Arguments.GetDoubleValue("Null", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_Text() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetDoubleValue("Text", true));
        }

        [TestMethod]
        public void GetDoubleValueTest_Throw_Valid() {
            Assert.AreEqual(5.5, Arguments.GetDoubleValue("ValidDouble", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_NotFound() {
            Assert.IsNull(Arguments.GetDecimalValue("InvalidKey"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Null() {
            Assert.IsNull(Arguments.GetDecimalValue("Null"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Text() {
            Assert.IsNull(Arguments.GetDecimalValue("Text"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Valid() {
            Assert.AreEqual(5.5m, Arguments.GetDecimalValue("ValidDouble"));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_NotFound() {
            Assert.ThrowsException<KeyNotFoundException>(() => Arguments.GetDecimalValue("InvalidKey", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_Null() {
            Assert.ThrowsException<ArgumentNullException>(() => Arguments.GetDecimalValue("Null", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_Text() {
            Assert.ThrowsException<ArgumentException>(() => Arguments.GetDecimalValue("Text", true));
        }

        [TestMethod]
        public void GetDecimalValueTest_Throw_Valid() {
            Assert.AreEqual(5.5m, Arguments.GetDecimalValue("ValidDouble", true));
        }
    }
}