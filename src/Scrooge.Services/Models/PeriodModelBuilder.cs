using System.Globalization;

namespace Scrooge.Services.Models;

public class PeriodModelBuilder : PeriodModel
{
    private const char DefaultDecorator = '-';
    private const string DefaultFormat = "yyyy-MM-dd";
    private const string DefaultPeriodToStringRepresentation = "yyyy-MM-dd_yyyy-MM-dd";
    private const int DefaultDaysInPeriod = 15;
    private const char DefaultPeriodSeparator = '_';
    private readonly char _defaultDecorator;

    public PeriodModelBuilder()
    {
        DaysInPeriod = DefaultDaysInPeriod;
        _defaultDecorator = DefaultDecorator;
        DaysInPeriod = DefaultDaysInPeriod;
        StartPeriod = DateTime.Now;
        EndPeriod = StartPeriod.AddDays(DaysInPeriod);
    }

    public PeriodModelBuilder(int daysInPeriod)
    {
        DaysInPeriod =
            daysInPeriod > 0 ? daysInPeriod : daysInPeriod < 0 ? daysInPeriod * -1 : DefaultDaysInPeriod;
        _defaultDecorator = DefaultDecorator;
        if (daysInPeriod < 0)
        {
            StartPeriod = DateTime.Now.AddDays(-DaysInPeriod);
            EndPeriod = DateTime.Now;
        }
        else
        {
            StartPeriod = DateTime.Now;
            EndPeriod = DateTime.Now.AddDays(DaysInPeriod);
        }
    }
    
    public PeriodModelBuilder(char decorator)
    {
        _defaultDecorator = decorator;
        DaysInPeriod = DefaultDaysInPeriod;
    }
    
    public PeriodModelBuilder(string periodHash, char decorator = DefaultDecorator)
    {
        _defaultDecorator = decorator == DefaultDecorator
            ? GetDateDecoratorFromDateString(periodHash)
            : decorator;
        DaysInPeriod = DefaultDaysInPeriod;
        var datesInString = new DateTime[2];
        var periodsString = periodHash.Split(DefaultPeriodSeparator);
        try
        {
            var pointer = 0;
            int year;
            int month;
            int day;
            if (periodsString.Length == 2)
            {
                foreach (var periodString in periodsString)
                {
                    if (pointer > 1)
                    {
                        throw new ArgumentException();
                    }
                    year = Int32.Parse(periodString.Substring(0, 4));
                    month = Int32.Parse(periodString.Substring(5, 2));
                    day = Int32.Parse(periodString.Substring(8, 2));
                    datesInString[pointer++] = new DateTime(year, month, day, 0, 0, 0);
                }
            }else if (periodsString.Length == 6)
            {
                var counter = 0;
                var datesPointer = 0;
                while (counter < 6)
                {
                    year = Int32.Parse(periodsString[counter]);
                    month = Int32.Parse(periodsString[counter + 1]);
                    day = Int32.Parse(periodsString[counter + 2]);
                    datesInString[datesPointer++] = new DateTime(year, month, day, 0, 0, 0);
                    counter += 3;
                }
            }
            else
            {
                throw new ArgumentException();
            }
            StartPeriod = datesInString[0];
            EndPeriod = datesInString[1];
        }
        catch (Exception)
        {
            throw new ArgumentException();
        }

        if (StartPeriod == EndPeriod)
        {
            StartPeriod = new DateTime(StartPeriod.Year, StartPeriod.Month, StartPeriod.Day, 0, 0, 0);
            EndPeriod = new DateTime(EndPeriod.Year, EndPeriod.Month, EndPeriod.Day, 11, 59, 59);
        }
        
        if (StartPeriod > EndPeriod)
        {
            (StartPeriod, EndPeriod) = (EndPeriod, StartPeriod);
        }

        DaysInPeriod = (EndPeriod - StartPeriod).Days;
    }

    public PeriodModelBuilder(DateTime startPeriod, DateTime endPeriod, char decorator = DefaultDecorator)
    {
        StartPeriod = startPeriod;
        EndPeriod = endPeriod;
        _defaultDecorator = decorator;
        if (StartPeriod > EndPeriod)
        {
            (StartPeriod, EndPeriod) = (EndPeriod, StartPeriod);
        }

        DaysInPeriod = (EndPeriod - StartPeriod).Days;
    }
    
    public PeriodModelBuilder(string startPeriod, string endPeriod, char decorator = DefaultDecorator)
    {
        if (startPeriod.Length != DefaultFormat.Length || endPeriod.Length != DefaultFormat.Length)
        {
            throw new ArgumentException();
        }

        try
        {
            var startPeriodDecorator = GetDateDecoratorFromDateString(startPeriod);
            var endPeriodDecorator = GetDateDecoratorFromDateString(endPeriod);
            var dateElementsStartPeriod = startPeriod.Split(startPeriodDecorator);
            var dateElementsEndPeriod = startPeriod.Split(endPeriodDecorator);
            
            var year = Int32.Parse(dateElementsStartPeriod[0]);
            var month = Int32.Parse(dateElementsStartPeriod[1]);
            var day = Int32.Parse(dateElementsStartPeriod[2]);
            StartPeriod = new DateTime(year, month, day, 0, 0, 0);
            
            year = Int32.Parse(dateElementsEndPeriod[0]);
            month = Int32.Parse(dateElementsEndPeriod[1]);
            day = Int32.Parse(dateElementsEndPeriod[2]);
            EndPeriod = new DateTime(year, month, day, 0, 0, 0);
            
            if (StartPeriod > EndPeriod)
            {
                (StartPeriod, EndPeriod) = (EndPeriod, StartPeriod);
            }

            DaysInPeriod = (EndPeriod - StartPeriod).Days;
            _defaultDecorator = decorator != DefaultDecorator ? decorator : startPeriodDecorator;
        }
        catch (Exception)
        {
            throw new ArgumentException();
        }
        
        _defaultDecorator = DefaultDecorator;
        DaysInPeriod = DefaultDaysInPeriod;
        if (StartPeriod > EndPeriod)
        {
            (StartPeriod, EndPeriod) = (EndPeriod, StartPeriod);
        }

        DaysInPeriod = (EndPeriod - StartPeriod).Days;
    }

    public override string ToString()
    {
        var startPeriodString = StartPeriod.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var endPeriodString = EndPeriod.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        return $"{startPeriodString}_{endPeriodString}";
    }

    private char GetDateDecoratorFromDateString(string dateString)
    {
        if (dateString.Length == DefaultPeriodToStringRepresentation.Length)
        {
            dateString = dateString.Split(DefaultPeriodSeparator)[0];
        }
        foreach (var item in dateString)
        {
            if (!char.IsNumber(item))
            {
                return item;
            }
        }
        return _defaultDecorator;
    }

    private void GetInitialDefaultValues()
    {
        var startPeriod = DateTime.Now;
        
    }
}