using System.Collections.ObjectModel;
using System.Reflection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class CsvFileManagerTests
    {
        FileManager _fileManager;


        [Test]
        public void CsvFileManager_ShouldBeInitializedWithACsv()
        {
            // Arrange
            var filename = "ToDoItemsCsv.csv";

            // Act
            _fileManager = new FileManager(filename);

            // Assert
            Assert.DoesNotThrowAsync(async () => await _fileManager.ReadToDoItemsFromFile());
        }


        [Test]
        public void CsvFileManager_ShouldThrowAnException_IfFileDoesNotExist()
        {
            // Arrange
            var filename = "aaaa.txt";

            // Act
            _fileManager = new FileManager(filename);

            // Assert
            Assert.Throws<FileNotFoundException>(() => File.Open(filename, FileMode.Open));
        }

    }
}