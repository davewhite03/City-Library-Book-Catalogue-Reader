using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace JsonReaderTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var book = new Book
            {
                Title = "Test Title",
                Author = "Test Author",
                Year = 2020,
                Description = "Test Description"
            };
            var expectedString = "Title: Test Title\nAuthor: Test Author\nDescription: Test Description\nYear: 2020";

            // Act
            var result = book.ToString();

            // Assert
            Assert.AreEqual(expectedString, result);
        }
        [TestMethod]
        public void ToString_ReturnsIncorrectFormat()
        {
            // Arrange
            var book = new Book
            {
                Title = "Test Title",
                Author = "Test Author",
                Year = 2020,
                Description = "Test Description"
            };
            var expectedString = "Incorrect Format";

            // Act
            var result = book.ToString();

            // Assert
            Assert.AreNotEqual(expectedString, result);
        }
        [TestMethod]
        public void PrintAllFileNames_PrintsCorrectFileNames()
        {
            // Arrange
            IEnumerable<string> files = new[]
        {
           "file1",
            "file2"
        };
            var expectedOutput = "- file1\r\n- file2\r\n"; // Expected console output

            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                // Act
                FileEnumeratorWrapper.PrintAllFileNames(files);

                // Assert
                var actualOutput = stringWriter.ToString();
                Assert.AreEqual(expectedOutput, actualOutput);
            }

            // Cleanup: Reset the console to its original state
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }
    }
}