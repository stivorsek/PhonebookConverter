using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverterL.Data.Entities;
using NUnit.Framework;


namespace PhonebookConverterNUNITTests;

public class UnitTest1
{
    private IValidation _validation;
    [SetUp]
    public void SetUp()
    {
        IExceptions exceptions = new Exceptions();
        _validation = new Validation(exceptions);
    }

    [Test]
    public void DatabaseOperationsGetID_ExistingContact_ReturnsContactObject()
    {
        // Arrange
        ContactInDb contact = new ContactInDb();

        // Act
        object result = _validation.DatabaseOperationsGetID(contact);

        // Assert
        // Sprawd�, czy zwr�cona warto�� jest taka sama jak obiekt contact
        Assert.AreSame(contact, result);
    }

    [Test]
    public void DatabaseOperationsGetID_DefaultContact_ThrowsException()
    {
        // Arrange
        ContactInDb contact = default;

        // Act & Assert
        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.Throws<Exception>(() => _validation.DatabaseOperationsGetID(contact));
    }
    [Test]
    public void ExportToXmlGetFolder_ReturnExistPath()
    {
        // Arrange
        string folderPath = "\\";

        // Act & Assert
        string result = _validation.ExportToXmlGetFolder(folderPath);
        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.AreEqual(folderPath, result);
    }
    [Test]
    public void ExportToXmlGetFolder_ThrowException()
    {
        // Arrange
        string folderPath = "XD";

        // Act & Assert

        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetFolder(folderPath));
    }
    [Test]
    public void ExportToXmlGetType_ThrowException()
    {
        // Arrange
        string type = "4345345";

        // Act & Assert

        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetType(type));
    }
    [Test]
    public void ExportToXmlGetType_ReturnType()
    {
        // Arrange
        string type = "1";

        // Act & Assert
        string result = _validation.ExportToXmlGetType(type);
        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.AreEqual(result, type);
    }
    [Test]
    public void ExportToXmlGetLoopState_ThrowException()
    {
        // Arrange
        string loopState = "3";

        // Act & Assert

        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetLoopState(loopState));
    }
    [Test]
    public void ExportToXmlGetLoopState_ReturnLoopState()
    {
        // Arrange
        string loopState = "1";

        // Act & Assert
        bool result = _validation.ExportToXmlGetLoopState(loopState);
        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.IsTrue(result);
    }
    [Test]
    public void ExportToXmlGetLoopTime_ThrowException()
    {
        // Arrange
        string loopTime = "0";

        // Act & Assert

        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetLoopTime(loopTime));
    }
    [Test]
    public void ExportToXmlGetLoopTime_ReturnLoopTime()
    {
        // Arrange
        string loopTime = "30";

        // Act & Assert
        var result = _validation.ExportToXmlGetLoopTime(loopTime);
        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.AreEqual(int.Parse(loopTime),result);
    }
}