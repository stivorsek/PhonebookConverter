using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.Data.Entities;


namespace PhonebookConverterNUNITTests;

public class UnitTest1
{
    private IValidation validation;
    [SetUp]
    public void SetUp()
    {        
        validation = new Validation();
    }

    [Test]
    public void DatabaseOperationsGetID_ExistingContact_ReturnsContactObject()
    {        
        ContactInDb contact = new ContactInDb();
 
        object result = validation.GetID(contact);

        Assert.AreSame(contact, result);
    }
    [Test]
    public void DatabaseOperationsGetID_DefaultContact_ThrowsException()
    {        
        ContactInDb contact = default;

        Assert.Throws<Exception>(() => validation.GetID(contact));
    }
    [Test]
    public void ExportToXmlGetFolder_ReturnExistPath()
    {        
        string folderPath = "\\";
     
        string result = validation.GetExportFolder(folderPath);

        Assert.AreEqual(folderPath, result);
    }
    [Test]
    public void ExportToXmlGetFolder_ThrowException()
    {       
        string folderPath = "XD";     
        
        Assert.Throws<Exception>(() => validation.GetExportFolder(folderPath));
    }
    [Test]
    public void ExportToXmlGetType_ThrowException()
    {        
        string type = "4345345";

        Assert.Throws<Exception>(() => validation.GetExportType(type));
    }
    [Test]
    public void ExportToXmlGetType_ReturnType()
    {        
        string type = "1";
     
        string result = validation.GetExportType(type);

        Assert.AreEqual(result, type);
    }
    [Test]
    public void ExportToXmlGetLoopState_ThrowException()
    {      
        string loopState = "3";     
        
        Assert.Throws<Exception>(() => validation.GetExportLoopState(loopState));
    }
    [Test]
    public void ExportToXmlGetLoopState_ReturnLoopState()
    {       
        string loopState = "1";
     
        bool result = validation.GetExportLoopState(loopState);
        
        Assert.IsTrue(result);
    }
    [Test]
    public void ExportToXmlGetLoopTime_ThrowException()
    {        
        string loopTime = "0";     
        
        Assert.Throws<Exception>(() => validation.GetExportLoopTime(loopTime));
    }
    [Test]
    public void ExportToXmlGetLoopTime_ReturnLoopTime()
    {        
        string loopTime = "30";
     
        var result = validation.GetExportLoopTime(loopTime);

        Assert.AreEqual(int.Parse(loopTime), result);
    }
    [Test]
    public void ImportGetPathXml_ThrowException()
    {      
        string pathXml = ".csv";     
        
        Assert.Throws<Exception>(() => validation.GetImportPathXml(pathXml));
    }
    [Test]
    public void ImportGetPathXml_ReturnPathCsv()
    {     
        string pathXml = "C:\\Users\\Admin\\Downloads\\1.xml";
     
        string result = validation.GetImportPathXml(pathXml);
     
        Assert.AreEqual(pathXml, result);
    }
    [Test]
    public void ImportGetPathCsv_ThrowException()
    {
        string pathXml = ".xml";
             
        Assert.Throws<Exception>(() => validation.GetImportPathCsv(pathXml));
    }
    [Test]
    public void ImportGetPathCsv_ReturnPathCsv()
    {
        string pathXml = "C:\\Users\\Admin\\Downloads\\2.csv";
        
        string result = validation.GetImportPathCsv(pathXml);
        
        Assert.AreEqual(pathXml, result);
    }
    [Test]
    public void DatabaseOperationsGetID_ThrowException()
    {
        string idFromUser = "gsfdsf";
        
        Assert.Throws<FormatException>(() => validation.GetID(idFromUser));
    }
    [Test]
    public void DatabaseOperationsGetID_ReturnID()
    {
        string idFromUser = "2";

        var result = validation.GetID(idFromUser);

        Assert.AreEqual(int.Parse(idFromUser), result);
    }
    [Test]
    public void DatabaseOperationsEditByIdChoseParameter_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => validation.GetTypeOperationChoise(idFromUser));
    }
    [Test]
    public void DatabaseOperationsEditByIdChoseParameter_ReturnID()
    {
        string idFromUser = "2";

        var result = validation.GetTypeOperationChoise(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void DatabaseOperationsExportToTxt_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => validation.ExportToTxt(idFromUser));
    }
    [Test]
    public void DatabaseOperationsExportToTxt_ReturnID()
    {
        string idFromUser = "2";

        var result = validation.ExportToTxt(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void DatabaseOperationsGetType_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => validation.GetType(idFromUser));
    }
    [Test]
    public void DatabaseOperationsGetType_ReturnID()
    {
        string idFromUser = "4";

        var result = validation.GetType(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void CheckExportSettingsExist_ThrowException()
    {
        string idFromUser = "gsfdsf";

        Assert.Throws<Exception>(() => validation.CheckExportSettingsExist(idFromUser));
    }
    [Test]
    public void CheckExportSettingsExist_ReturnID()
    {
        string idFromUser = "2";

        var result = validation.CheckExportSettingsExist(idFromUser);

        Assert.AreEqual(idFromUser, result);
    }
    [Test]
    public void DataOperationsExportToTxtDirectoryExist_ThrowException()
    {
        string directory = "C:\\test";

        Assert.Throws<Exception>(() => validation.ExportToTxtDirectoryExist(directory));
    }
    [Test]
    public void DataOperationsExportToTxtDirectoryExist_ReturnDirectory()
    {
        string directory = "C:\\";

        var result = validation.ExportToTxtDirectoryExist(directory);

        Assert.AreEqual(directory, result);
    }
    [Test]
    public void DataOperationsEditByIDGetChoise_ThrowException()
    {
        string choise = "12";

        Assert.Throws<Exception>(() => validation.EditGetChoise(choise));
    }
    [Test]
    public void DataOperationsEditByIDGetChoise_ReturnDirectory()
    {
        string choise = "1";

        var result = validation.EditGetChoise(choise);

        Assert.AreEqual(choise, result);
    }    
}