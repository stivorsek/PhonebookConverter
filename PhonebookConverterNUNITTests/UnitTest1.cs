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
        // Sprawdü, czy zwrÛcona wartoúÊ jest taka sama jak obiekt contact
        Assert.AreSame(contact, result);
    }

    [Test]
    public void DatabaseOperationsGetID_DefaultContact_ThrowsException()
    {
        // Arrange
        ContactInDb contact = default;

        // Act & Assert
        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.Throws<Exception>(() => _validation.DatabaseOperationsGetID(contact));
    }
    [Test]
    public void ExportToXmlGetFolder_ReturnExistPath()
    {
        // Arrange
        string folderPath = "\\";

        // Act & Assert
        string result = _validation.ExportToXmlGetFolder(folderPath);
        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.AreEqual(folderPath, result);
    }
    [Test]
    public void ExportToXmlGetFolder_ThrowException()
    {
        // Arrange
        string folderPath = "XD";

        // Act & Assert

        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetFolder(folderPath));
    }
    [Test]
    public void ExportToXmlGetType_ThrowException()
    {
        // Arrange
        string type = "4345345";

        // Act & Assert

        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetType(type));
    }
    [Test]
    public void ExportToXmlGetType_ReturnType()
    {
        // Arrange
        string type = "1";

        // Act & Assert
        string result = _validation.ExportToXmlGetType(type);
        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.AreEqual(result, type);
    }
    [Test]
    public void ExportToXmlGetLoopState_ThrowException()
    {
        // Arrange
        string loopState = "3";

        // Act & Assert

        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetLoopState(loopState));
    }
    [Test]
    public void ExportToXmlGetLoopState_ReturnLoopState()
    {
        // Arrange
        string loopState = "1";

        // Act & Assert
        bool result = _validation.ExportToXmlGetLoopState(loopState);
        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.IsTrue(result);
    }
    [Test]
    public void ExportToXmlGetLoopTime_ThrowException()
    {
        // Arrange
        string loopTime = "0";

        // Act & Assert

        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.Throws<Exception>(() => _validation.ExportToXmlGetLoopTime(loopTime));
    }
    [Test]
    public void ExportToXmlGetLoopTime_ReturnLoopTime()
    {
        // Arrange
        string loopTime = "30";

        // Act & Assert
        var result = _validation.ExportToXmlGetLoopTime(loopTime);
        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.AreEqual(int.Parse(loopTime), result);
    }
    [Test]
    public void ImportGetPathXml_ThrowException()
    {
        // Arrange
        string pathXml = ".csv";

        // Act & Assert

        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.Throws<Exception>(() => _validation.ImportGetPathXml(pathXml));
    }
    [Test]
    public void ImportGetPathXml_ReturnPathCsv()
    {
        // Arrange
        string pathXml = "C:\\Users\\Admin\\Downloads\\1.xml";

        // Act & Assert
        string result = _validation.ImportGetPathXml(pathXml);
        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.AreEqual(pathXml, result);
    }
    [Test]
    public void ImportGetPathCsv_ThrowException()
    {
        // Arrange
        string pathXml = ".xml";

        // Act & Assert

        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.Throws<Exception>(() => _validation.ImportGetPathCsv(pathXml));
    }
    [Test]
    public void ImportGetPathCsv_ReturnPathCsv()
    {
        // Arrange
        string pathXml = "C:\\Users\\Admin\\Downloads\\2.csv";

        // Act & Assert
        string result = _validation.ImportGetPathCsv(pathXml);
        // Sprawdü, czy metoda rzuci≥a oczekiwany wyjπtek dla wartoúci domyúlnej
        Assert.AreEqual(pathXml, result);
    }
    [Test]
    public void DatabaseOperationsGetID_ThrowException()
    {
        string idFromUser = "gsfdsf";
        
        Assert.Throws<FormatException>(() => _validation.DatabaseOperationsGetID(idFromUser));
    }
    [Test]
    public void DatabaseOperationsGetID_ReturnID()
    {
        string idFromUser = "2";

        var result = _validation.DatabaseOperationsGetID(idFromUser);

        Assert.AreEqual(int.Parse(idFromUser), result);
    }
    [Test]
    public void DatabaseOperationsEditByIdChoseParameter_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => _validation.DatabaseOperationsEditByIdChoseParameter(idFromUser));
    }
    [Test]
    public void DatabaseOperationsEditByIdChoseParameter_ReturnID()
    {
        string idFromUser = "2";

        var result = _validation.DatabaseOperationsEditByIdChoseParameter(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void DatabaseOperationsExportToTxt_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => _validation.DatabaseOperationsExportToTxt(idFromUser));
    }
    [Test]
    public void DatabaseOperationsExportToTxt_ReturnID()
    {
        string idFromUser = "2";

        var result = _validation.DatabaseOperationsExportToTxt(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void DatabaseOperationsGetType_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => _validation.DatabaseOperationsGetType(idFromUser));
    }
    [Test]
    public void DatabaseOperationsGetType_ReturnID()
    {
        string idFromUser = "5";

        var result = _validation.DatabaseOperationsGetType(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void CheckExportSettingsExist_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => _validation.CheckExportSettingsExist(idFromUser));
    }
    [Test]
    public void CheckExportSettingsExist_ReturnID()
    {
        string idFromUser = "2";

        var result = _validation.CheckExportSettingsExist(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }

}