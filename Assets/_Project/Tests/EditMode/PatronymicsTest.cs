using NUnit.Framework;

public class PatronymicsTests
{
    private NamingManager namingManager;

    [SetUp]
    public void Setup()
    {
        // Initialize the NamingManager instance
        namingManager = new NamingManager();
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_unn()
    {
        // Arrange
        string surname = "Auounn";
        bool isMale = true;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Auounarson", result); // <--- In the case of "unn", we replace the last letter with "ar". This is a special case.
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_unn()
    {
        // Arrange
        string surname = "Auounn";
        bool isMale = false;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Auounardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_dan()
    {
        // Arrange
        string surname = "Halfdan";
        bool isMale = true;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Halfdanarson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_dan()
    {
        // Arrange
        string surname = "Haldan";
        bool isMale = false;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Haldanardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_uror()
    {
        // Arrange
        string surname = "Thuror";
        bool isMale = true;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Thurarson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_uror()
    {
        // Arrange
        string surname = "Thuror";
        bool isMale = false;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Thurardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_orn()
    {
        // Arrange
        string surname = "Bjorn";
        bool isMale = true;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Bjarnarson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_orn()
    {
        // Arrange
        string surname = "Bjorn";
        bool isMale = false;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Bjarnardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_NoSpecialEnding()
    {
        // Arrange
        string surname = "Erik"; // <--- Erik is a name that shouldn't be given any sort of special ending.
        bool isMale = true;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Erikson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_NoSpecialEnding()
    {
        // Arrange
        string surname = "Erik";
        bool isMale = false;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Eriksdottir", result); // <--- The reason there is an "s" is because in the case of no custom suffix, we add a possessive "s" which "son" already comes with.
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_iorn()
    {
        // Arrange
        string surname = "Biorn";
        bool isMale = true;

        // Act
        string result = namingManager.PatronymicSurname(surname, isMale);

        // Assert
        Assert.AreEqual("Bjarnarson", result);
    }
}
