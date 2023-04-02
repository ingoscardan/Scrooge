using Scrooge.Services.Models;

namespace Scroge.Services.Test.Models;

[TestFixture]
public class PeriodTest
{
    private string _todayStringRep = DateTime.Now.ToString("yyyy-MM-dd");
    
    [Test]
    public void CreateANewPeriodWithNoParaMeters()
    {
        // Prepare
        var defaultDaysInPeriod = 15;
        var defaultDateFormat = "yyyy-MM-dd";
        var dateDefaultDaysInPeriodFromToday = DateTime.Now.AddDays(defaultDaysInPeriod).ToString(defaultDateFormat);
        // Act
        var period = new PeriodModelBuilder();
        
        // Assert
        Assert.That(period.StartPeriod.ToString(defaultDateFormat), Is.EqualTo(_todayStringRep));
        Assert.That(period.EndPeriod.ToString(defaultDateFormat), Is.EqualTo(dateDefaultDaysInPeriodFromToday));
        Assert.That(period.DaysInPeriod, Is.EqualTo(defaultDaysInPeriod));
    }
    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(16)]
    [TestCase(100)]
    [TestCase(365)]
    [TestCase(92)]
    public void CreateANewPeriodWithDaysInPeriod(int days)
    {
        // Prepare
        var daysInPeriod = days;
        var defaultDateFormat = "yyyy-MM-dd";
        var dateDefaultDaysInPeriodFromToday = DateTime.Now.AddDays(daysInPeriod).ToString(defaultDateFormat);
        // Act
        var period = new PeriodModelBuilder(daysInPeriod);
        // Assert
        Assert.That(period.StartPeriod.ToString(defaultDateFormat), Is.EqualTo(_todayStringRep));
        Assert.That(period.EndPeriod.ToString(defaultDateFormat), Is.EqualTo(dateDefaultDaysInPeriodFromToday));
        Assert.That(period.DaysInPeriod, Is.EqualTo(daysInPeriod));
    }

    [Test]
    [TestCase('@')]
    [TestCase('/')]
    [TestCase('-')]
    [TestCase('_')]
    [TestCase('^')]
    [TestCase('1')]
    public void CreateANewPeriodModelWithDecorator(char decorator)
    {
        // Prepare
        // Prepare
        var daysInPeriod = 15;
        var defaultDateFormat = "yyyy-MM-dd".Replace('-', decorator);
        var dateDefaultDaysInPeriodFromToday = DateTime.Now.AddDays(daysInPeriod).ToString(defaultDateFormat);

        if (char.IsNumber(decorator))
        {

            // Act and Assert
            Assert.Throws<ArgumentException>(() => new PeriodModelBuilder(decorator));
        }
        else
        {
            // Act
            var period = new PeriodModelBuilder(decorator);

            // Assert
            Assert.That(period.StartPeriod.ToString(defaultDateFormat),
                Is.EqualTo(_todayStringRep.Replace('-', decorator)));
            Assert.That(period.EndPeriod.ToString(defaultDateFormat), Is.EqualTo(dateDefaultDaysInPeriodFromToday));
            Assert.That(period.DaysInPeriod, Is.EqualTo(daysInPeriod));
        }
    }
    
    [Test]
    [TestCase("2023-01-31","2023-02-15")]
    [TestCase("2023-02-15","2023-01-31")]
    [TestCase("2023-12-31","2023-02-15")]
    [TestCase("2023-01-31","2023-12-15")]
    [TestCase("2023-12-15","2023-01-31")]
    [TestCase("2023-01-31","2023-04-15")]
    public void CreateANewPeriodModelWithStartAndEndPeriod(DateTime start, DateTime end)
    {
        // Prepare
        var daysInPeriod = (end - start).Days;
        var defaultDateFormat = "yyyy-MM-dd";
        var dateDefaultDaysInPeriod = DateTime.Now.AddDays(daysInPeriod).ToString(defaultDateFormat);
        // Act
        var period = new PeriodModelBuilder(start, end);
        // Assert
        if (start > end)
        {
            Assert.That(period.StartPeriod.ToString(defaultDateFormat), Is.EqualTo(end.ToString(defaultDateFormat)));
            Assert.That(period.EndPeriod.ToString(defaultDateFormat), Is.EqualTo(start.ToString(defaultDateFormat)));
            Assert.That(period.DaysInPeriod, Is.EqualTo(daysInPeriod * -1));
        }
        else
        {
            Assert.That(period.StartPeriod.ToString(defaultDateFormat), Is.EqualTo(start.ToString(defaultDateFormat)));
            Assert.That(period.EndPeriod.ToString(defaultDateFormat), Is.EqualTo(end.ToString(defaultDateFormat)));
            Assert.That(period.DaysInPeriod, Is.EqualTo(daysInPeriod));
        }
    }
    
    [Test]
    [TestCase("2023-01-31","2023-02-15")]
    [TestCase("2023-02-15","2023-01-31")]
    [TestCase("2023-12-31","2023-02-15")]
    [TestCase("2023-01-31","2023-12-15")]
    [TestCase("2023-12-15","2023-01-31")]
    [TestCase("2023-01-31","2023-04-15")]
    public void CreateANewPeriodModelWithStartAndEndPeriod(string start, string end)
    {
        // Prepare
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        
        var daysInPeriod = (endDate - startDate).Days;
        var defaultDateFormat = "yyyy-MM-dd";
        var dateDefaultDaysInPeriod = DateTime.Now.AddDays(daysInPeriod).ToString(defaultDateFormat);
        // Act
        var period = new PeriodModelBuilder(start, end);
        // Assert
        if (startDate > endDate)
        {
            Assert.That(period.StartPeriod.ToString(defaultDateFormat), Is.EqualTo(endDate.ToString(defaultDateFormat)));
            Assert.That(period.EndPeriod.ToString(defaultDateFormat), Is.EqualTo(startDate.ToString(defaultDateFormat)));
            Assert.That(period.DaysInPeriod, Is.EqualTo(daysInPeriod * -1));
        }
        else
        {
            Assert.That(period.StartPeriod.ToString(defaultDateFormat), Is.EqualTo(startDate.ToString(defaultDateFormat)));
            Assert.That(period.EndPeriod.ToString(defaultDateFormat), Is.EqualTo(endDate.ToString(defaultDateFormat)));
            Assert.That(period.DaysInPeriod, Is.EqualTo(daysInPeriod));
        }
    }
}