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
        Gender gender = Gender.Male;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Auounarson", result); // <--- In the case of "unn", we replace the last letter with "ar". This is a special case.
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_unn()
    {
        // Arrange
        string surname = "Auounn";
        Gender gender = Gender.Female;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Auounardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_dan()
    {
        // Arrange
        string surname = "Halfdan";
        Gender gender = Gender.Male;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Halfdanarson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_dan()
    {
        // Arrange
        string surname = "Haldan";
        Gender gender = Gender.Female;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Haldanardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_uror()
    {
        // Arrange
        string surname = "Thuror";
        Gender gender = Gender.Male;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Thurarson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_uror()
    {
        // Arrange
        string surname = "Thuror";
        Gender gender = Gender.Female;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Thurardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_orn()
    {
        // Arrange
        string surname = "Bjorn";
        Gender gender = Gender.Male;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Bjarnarson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_Ending_orn()
    {
        // Arrange
        string surname = "Bjorn";
        Gender gender = Gender.Female;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Bjarnardottir", result);
    }

    [Test]
    public void TestPatronymicSurname_Male_NoSpecialEnding()
    {
        // Arrange
        string surname = "Erik"; // <--- Erik is a name that shouldn't be given any sort of special ending.
        Gender gender = Gender.Male;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Erikson", result);
    }

    [Test]
    public void TestPatronymicSurname_Female_NoSpecialEnding()
    {
        // Arrange
        string surname = "Erik";
        Gender gender = Gender.Female;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Eriksdottir", result); // <--- The reason there is an "s" is because in the case of no custom suffix, we add a possessive "s" which "son" already comes with.
    }

    [Test]
    public void TestPatronymicSurname_Male_Ending_iorn()
    {
        // Arrange
        string surname = "Biorn";
        Gender gender = Gender.Male;

        // Act
        string result = namingManager.PatronymicSurname(surname, gender);

        // Assert
        Assert.AreEqual("Bjarnarson", result);
    }
}
