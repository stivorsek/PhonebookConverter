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
        _validation = new Validation();
    }

    [Test]
    public void DatabaseOperationsGetID_ExistingContact_ReturnsContactObject()
    {
        // Arrange
        ContactInDb contact = new ContactInDb();

        // Act
        object result = _validation.DataOperationsGetID(contact);

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
        Assert.Throws<Exception>(() => _validation.DataOperationsGetID(contact));
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
        Assert.AreEqual(int.Parse(loopTime), result);
    }
    [Test]
    public void ImportGetPathXml_ThrowException()
    {
        // Arrange
        string pathXml = ".csv";

        // Act & Assert

        // Sprawd�, czy metoda rzuci�a oczekiwany wyj�tek dla warto�ci domy�lnej
        Assert.Throws<Exception>(() => _validation.ImportGetPathXml(pathXml));
    }
    [Test]
    public void ImportGetPathXml_ReturnPathCsv()
    {     
        string pathXml = "C:\\Users\\Admin\\Downloads\\1.xml";
     
        string result = _validation.ImportGetPathXml(pathXml);
     
        Assert.AreEqual(pathXml, result);
    }
    [Test]
    public void ImportGetPathCsv_ThrowException()
    {
        string pathXml = ".xml";
             
        Assert.Throws<Exception>(() => _validation.ImportGetPathCsv(pathXml));
    }
    [Test]
    public void ImportGetPathCsv_ReturnPathCsv()
    {
        string pathXml = "C:\\Users\\Admin\\Downloads\\2.csv";
        
        string result = _validation.ImportGetPathCsv(pathXml);
        
        Assert.AreEqual(pathXml, result);
    }
    [Test]
    public void DatabaseOperationsGetID_ThrowException()
    {
        string idFromUser = "gsfdsf";
        
        Assert.Throws<FormatException>(() => _validation.DataOperationsGetID(idFromUser));
    }
    [Test]
    public void DatabaseOperationsGetID_ReturnID()
    {
        string idFromUser = "2";

        var result = _validation.DataOperationsGetID(idFromUser);

        Assert.AreEqual(int.Parse(idFromUser), result);
    }
    [Test]
    public void DatabaseOperationsEditByIdChoseParameter_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => _validation.DataOperationsEditByIdChoseParameter(idFromUser));
    }
    [Test]
    public void DatabaseOperationsEditByIdChoseParameter_ReturnID()
    {
        string idFromUser = "2";

        var result = _validation.DataOperationsEditByIdChoseParameter(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void DatabaseOperationsExportToTxt_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => _validation.DataOperationsExportToTxt(idFromUser));
    }
    [Test]
    public void DatabaseOperationsExportToTxt_ReturnID()
    {
        string idFromUser = "2";

        var result = _validation.DataOperationsExportToTxt(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void DatabaseOperationsGetType_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => _validation.DataOperationsGetType(idFromUser));
    }
    [Test]
    public void DatabaseOperationsGetType_ReturnID()
    {
        string idFromUser = "4";

        var result = _validation.DataOperationsGetType(idFromUser);

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
    [Test]
    public void DataOperationsExportToTxtDirectoryExist_ThrowException()
    {
        string directory = "C:\\test";

        Assert.Throws<Exception>(() => _validation.DataOperationsExportToTxtDirectoryExist(directory));
    }
    [Test]
    public void DataOperationsExportToTxtDirectoryExist_ReturnDirectory()
    {
        string directory = "C:\\";

        var result = _validation.DataOperationsExportToTxtDirectoryExist(directory);

        Assert.AreEqual(directory, result);
    }
    [Test]
    public void DataOperationsEditByIDGetChoise_ThrowException()
    {
        string choise = "12";

        Assert.Throws<Exception>(() => _validation.DataOperationsEditByIDGetChoise(choise));
    }
    [Test]
    public void DataOperationsEditByIDGetChoise_ReturnDirectory()
    {
        string choise = "1";

        var result = _validation.DataOperationsEditByIDGetChoise(choise);

        Assert.AreEqual(choise, result);
    }    
}