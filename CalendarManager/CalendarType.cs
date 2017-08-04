﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CalendarManager
{
    public enum moonPhase { full, waningGib, lastQuarter, waningCresc, newMoon, waxingCrsec, firstQuarter, waxingGib };
    public class CalendarType
    {


        #region jsonData
        [JsonProperty]
        public string calendarName { get; set; }

        [JsonProperty]
        static int numDaysInYear;
        [JsonProperty]
        static bool celestialEvents;
        [JsonProperty]
        static int numMonthsInYear;
        [JsonProperty]
        static string[] monthNames;
        [JsonProperty]
        static int[] numDaysInMonth;
        [JsonProperty]
        static int numDaysInWeek;
        [JsonProperty]
        static string[] weekdayNames;
        [JsonProperty]
        static int numOfMoons;
        [JsonProperty]
        static string[] moonNames;
        [JsonProperty]
        static int[] moonCycle;
        [JsonProperty]
        static int[] moonShift;
        [JsonProperty]
        static List <moonPhase[]> moonPhases;
        [JsonProperty]
        static int startYear;
        [JsonProperty]
        static int startDay;
        #endregion

        #region Current data
        int month;
        int day;
        int year;
        int dayOfWeek;
        int[] moonCounters;
        #endregion



        public CalendarType(string json, string name)
        {
            try
            {
                LoadFromDonjonJSON(JsonConvert.DeserializeObject(json));
                createMoonPhaseArray();
                setDate(1, 1, 0);
            }
            catch
            {
            }

            calendarName = name;
        }

        public CalendarType(System.IO.StreamReader sr)
        {
            //loadData(sr);
            setDate(1, 1, 0);
        }

        /// <summary>
        /// Load data FROM SAVE FILE
        /// </summary>
        /// <param name="json"></param>
        public CalendarType(dynamic json)
        {
            LoadFromJSON(json);
            createMoonPhaseArray();
            setDate(1, 1, 0);
        }


        public void saveData(System.IO.StreamWriter writer)
        {
            writer.WriteLine(calendarName);
            writer.WriteLine(numDaysInYear);
            writer.WriteLine(celestialEvents);
            writer.WriteLine(numMonthsInYear);
            for (int i = 0; i < numMonthsInYear; i++)
            {
                writer.WriteLine(monthNames[i+1]);
                writer.WriteLine(numDaysInMonth[i+1]);
            }
            writer.WriteLine(numDaysInWeek);
            for (int i = 0; i < numDaysInWeek; i++)
                writer.WriteLine(weekdayNames[i]);
            writer.WriteLine(numOfMoons);
            for (int i = 0; i < numOfMoons; i++)
            {
                writer.WriteLine(moonNames[i]);
                writer.WriteLine(moonCycle[i]);
                writer.WriteLine(moonShift[i]);
            }
            writer.WriteLine(startYear);
            writer.WriteLine(startDay);
        }


        public void LoadFromJSON(dynamic json)
        {
            try
            {
                calendarName = json["calendar"]["calendarName"];
                numDaysInYear = json["calendar"]["numDaysInYear"];
                celestialEvents = json["calendar"]["celestialEvents"];

                numMonthsInYear = json["calendar"]["numMonthsInYear"];

                monthNames = new string[numMonthsInYear + 1];
                numDaysInMonth = new int[numMonthsInYear + 1];
                for (int i = 0; i < numMonthsInYear; i++)
                {
                    monthNames[i + 1] = json["calendar"]["monthNames"][i+1];

                    try
                    {
                        numDaysInMonth[i + 1] = json["calendar"]["numDaysInMonth"][i+1];
                    }
                    catch
                    {
                        numDaysInMonth[i + 1] = numDaysInYear / numMonthsInYear;
                    }
                }

                numDaysInWeek = json["calendar"]["numDaysInWeek"];
                weekdayNames = new string[numDaysInWeek];
                if (weekdayNames == null)
                {
                    weekdayNames = new string[numDaysInWeek];
                    for (int i = 0; i < numDaysInWeek; i++)
                        weekdayNames[i] = null;
                }

                for (int i = 0; i < numDaysInWeek; i++)
                    weekdayNames[i] = json["calendar"]["weekdayNames"][i];

                numOfMoons = json["calendar"]["numOfMoons"];
                moonNames = new string[numOfMoons];
                moonCycle = new int[numOfMoons];
                moonShift = new int[numOfMoons];
                try
                {
                    for (int i = 0; i < numOfMoons; i++)
                    {
                        moonNames[i] = json["calendar"]["moonNames"][i];
                        moonCycle[i] = json["calendar"]["moonCycle"][i];
                        moonShift[i] = json["calendar"]["moonShift"][i];
                    }
                }
                catch
                {
                    numOfMoons = 0;
                    moonNames = new string[numOfMoons];
                    moonCycle = new int[numOfMoons];
                    moonShift = new int[numOfMoons];
                }
                startYear = json["calendar"]["startYear"];
                startDay = json["calendar"]["startDay"];
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// Load JSON data FROM DONJON
        /// </summary>
        /// <param name="json"></param>
        public void LoadFromDonjonJSON(dynamic json)
        {
            try
            {
                numDaysInYear = json["year_len"];
                celestialEvents = json["events"];

                numMonthsInYear = json["n_months"];
                monthNames = new string[numMonthsInYear + 1];
                numDaysInMonth = new int[numMonthsInYear + 1];
                for (int i = 0; i < numMonthsInYear; i++)
                {
                    monthNames[i + 1] = json["months"][i];

                    try
                    {
                        numDaysInMonth[i + 1] = json["month_len"][monthNames[i + 1]];
                    }
                    catch
                    {
                        numDaysInMonth[i + 1] = numDaysInYear / numMonthsInYear;
                    }
                }

                numDaysInWeek = json["week_len"];
                weekdayNames = new string[numDaysInWeek];
                if (weekdayNames == null)
                {
                    weekdayNames = new string[numDaysInWeek];
                    for (int i = 0; i < numDaysInWeek; i++)
                        weekdayNames[i] = null;
                }

                for (int i = 0; i < numDaysInWeek; i++)
                    weekdayNames[i] = json["weekdays"][i];

                numOfMoons = json["n_moons"];
                moonNames = new string[numOfMoons];
                moonCycle = new int[numOfMoons];
                moonShift = new int[numOfMoons];
                try
                {
                    for (int i = 0; i < numOfMoons; i++)
                    {
                        moonNames[i] = json["moons"][i];
                        moonCycle[i] = json["lunar_cyc"][moonNames[i]];
                        moonShift[i] = json["lunar_shf"][moonNames[i]];
                    }
                }
                catch
                {
                    numOfMoons = 0;
                    moonNames = new string[numOfMoons];
                    moonCycle = new int[numOfMoons];
                    moonShift = new int[numOfMoons];
                }
                startYear = json["year"];
                startDay = json["first_day"];
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /*
        public void loadData(System.IO.StreamReader reader)
        {
            calendarName = reader.ReadLine();
            numDaysInYear = Int32.Parse(reader.ReadLine());
            celestialEvents = Convert.ToBoolean(reader.ReadLine());
            numMonthsInYear = Int32.Parse(reader.ReadLine());
            monthNames = new string[numMonthsInYear+1];
            numDaysInMonth = new int[numMonthsInYear+1];
            for (int i = 0; i < numMonthsInYear; i++)
            {
                monthNames[i+1] = reader.ReadLine();
                numDaysInMonth[i+1] = Int32.Parse(reader.ReadLine());
            }

            numDaysInWeek = Int32.Parse(reader.ReadLine());
            weekdayNames = new string[numDaysInWeek];
            for (int i = 0; i < numDaysInWeek; i++)
                weekdayNames[i] = reader.ReadLine();

            numOfMoons = Int32.Parse(reader.ReadLine());
            moonNames = new string[numOfMoons];
            moonCycle = new int[numOfMoons];
            moonShift = new int[numOfMoons];
            for (int i = 0; i < numOfMoons; i++)
            {
                moonNames[i] = reader.ReadLine();
                moonCycle[i] = (int)Math.Round(Double.Parse(reader.ReadLine()));
                moonShift[i] = Int32.Parse(reader.ReadLine());
            }
            createMoonPhaseArray();
            startYear = Int32.Parse(reader.ReadLine());
            startDay = Int32.Parse(reader.ReadLine());
        }*/

        private void createMoonPhaseArray()
        {
            moonPhases = new List<moonPhase[]>(numOfMoons);
            moonCounters = new int[numOfMoons];
            for (int i = 0; i < numOfMoons; i++)
            {
                moonPhase[] arrayToAdd;
                int fullMoonLocation = 0;
                int newMoonLocation = (moonCycle[i] - 1) / 2 + 1;
                int lastQuarterLocation = newMoonLocation / 2 + 1;
                int firstQuarterLocation = newMoonLocation + newMoonLocation / 2 + 1;
                arrayToAdd = new moonPhase[moonCycle[i]];
                arrayToAdd[fullMoonLocation] = moonPhase.full;
                arrayToAdd[newMoonLocation] = moonPhase.newMoon;
                arrayToAdd[firstQuarterLocation] = moonPhase.firstQuarter;
                arrayToAdd[lastQuarterLocation] = moonPhase.lastQuarter;

                for (int j = fullMoonLocation + 1; j < lastQuarterLocation; j++)
                    arrayToAdd[j] = moonPhase.waningGib;

                for (int j = lastQuarterLocation + 1; j < newMoonLocation; j++)
                    arrayToAdd[j] = moonPhase.waningCresc;

                for (int j = newMoonLocation + 1; j < firstQuarterLocation; j++)
                    arrayToAdd[j] = moonPhase.waxingCrsec;

                for (int j = firstQuarterLocation + 1; j < moonCycle[i]; j++)
                    arrayToAdd[j] = moonPhase.waxingGib;

                moonPhases.Add(arrayToAdd);
            }
        }


        #region forward in time
        public void addDay()
        {
            day++;
            if (day > numDaysInMonth[month])
            {
                day = 1;
                month++;
                if (month > numMonthsInYear)
                {
                    month = 1;
                    year++;
                }
            }
            addDayOfWeek();
            addMoonPhase();

            if (year > 9999)
            {
                year = 9999;
                month = 16;
                day = numDaysInMonth[16];
                // Kinda jank, if this if statement happens, the date doesnt change, so reverse the adddayofweek and addmoonphase
                subDayOfWeek();
                subMoonPhase();
            }
        }
        public void addDay(int numDays)
        {
            for (int i = 0; i < numDays; i++)
                addDay();
        }
        public void addWeek()
        {
            addDay(numDaysInWeek);
        }
        public void addMonth()
        {
            addDay(numDaysInMonth[month]);
        }
        public void addYear()
        {
            year++;
            if (year > 9999)
                year = 9999;
            determineDayOfWeek();
            determineMoonCounters();
        }
        public void addDayOfWeek()
        {
            dayOfWeek++;
            if (dayOfWeek >= numDaysInWeek - 1) // >= because dayOfWeek goes from 0 to numDaysInWeek - 1
                dayOfWeek = 0;
        }

        public void addMoonPhase()
        {
            for (int i = 0; i < moonCounters.Length; i++)
            {
                moonCounters[i]++;
                if (moonCounters[i] >= moonCycle[i])
                    moonCounters[i] = 0;
            }
        }
        #endregion

        #region backward in time
        public void subDay()
        {
            day--;
            if (day < 1)
            {
                month--;
                if (month < 1)
                {
                    year--;
                    month = numMonthsInYear;
                }
                day = numDaysInMonth[month];
            }
            subDayOfWeek();
            subMoonPhase();

            if (year < 0)
            {
                day = 1;
                month = 1;
                year = 0;
                addDayOfWeek();
                addMoonPhase();
            }


        }
        public void subDay(int numDays)
        {
            for (int i = 0; i < numDays; i++)
            {
                subDay();
            }
        }
        public void subWeek()
        {
            subDay(numDaysInWeek);
        }
        public void subMonth()
        {
            subDay(numDaysInMonth[month]);
        }
        public void subYear()
        {
            year--;
            if (year < 0)
                year = 0;
            determineDayOfWeek();
            determineMoonCounters();
        }
        public void subDayOfWeek()
        {
            dayOfWeek--;
            if (dayOfWeek < 0)
                dayOfWeek = numDaysInWeek - 1;
        }

        public void subMoonPhase()
        {
            for (int i = 0; i < moonCounters.Length; i++)
            {
                moonCounters[i]--;
                if (moonCounters[i] < 0)
                    moonCounters[i] = moonCycle[i] - 1;
            }
        }
        #endregion


        #region setDate and all functions determining day of week, moonphase, etc

        public void setDate(string dateString)
        {
            setDate(Int32.Parse(dateString.Substring(0, 2)), Int32.Parse(dateString.Substring(2, 2)), Int32.Parse(dateString.Substring(4, 4)));
        }

        public void setDate(int m, int d, int y)
        {
            if (m > numMonthsInYear)
                m = numMonthsInYear;
            else if (m <= 0)
                m = 1;

            month = m;

            if (d > numDaysInMonth[month])
                d = numDaysInMonth[month];
            else if (d <= 0)
                d = 1;

            day = d;

            if (y < 0)
                y = 1;
            else if (y > 9999)
                year = 9999;

            year = y;

            dayOfWeek = determineDayOfWeek();
            determineMoonCounters();
        }

        public int determineDayOfWeek()
        {
            return determineDayOfWeek(month, day, year);
        }

        public static int determineDayOfWeek(string currentDate)
        {
            return determineDayOfWeek(Int32.Parse(currentDate.Substring(0, 2)), Int32.Parse(currentDate.Substring(2, 2)), Int32.Parse(currentDate.Substring(4, 4)));
        }

        public static int determineDayOfWeek(int m, int d, int y)
        {
            int totalDaysPassedSinceStart = Math.Abs(y - startYear) * numDaysInYear + (determineDayOfYear(m, d, y) - 1);
            int modResult = (totalDaysPassedSinceStart) % (numDaysInWeek);
            return ((modResult + startDay) % (numDaysInWeek));
        }

        public void determineMoonCounters()
        {
            int daysSinceFirstDay = 0;

            for (int i = 0; i < Math.Abs(year - startYear); i++)
                daysSinceFirstDay += numDaysInYear;

            daysSinceFirstDay += determineDayOfYear();

            for (int i = 0; i < numOfMoons; i++)
            {
                moonCounters[i] = (daysSinceFirstDay + moonShift[i]) % moonCycle[i];
            }

        }
        #endregion


        #region determineDayOfYear
        public int determineDayOfYear()
        {
            return determineDayOfYear(month, day, year);
        }

        public static int determineDayOfYear(string currentDate)
        {
            return determineDayOfYear(Int32.Parse(currentDate.Substring(0, 2)), Int32.Parse(currentDate.Substring(2, 2)), Int32.Parse(currentDate.Substring(4, 4)));
        }

        public static int determineDayOfYear(int m, int d, int y)
        {
            int dayAccumulator = 0;

            // Add the days of the month before current month
            for (int i = 0; i < m - 1; i++)
            {
                dayAccumulator += numDaysInMonth[i + 1];
            }
            dayAccumulator += d; // add current day to sum
            return dayAccumulator;
        }

#endregion

        #region returning date, moonphases, names, etc.

        public string getMonthName()
        {
            return monthNames[month];
        }

        public string returnMoonNames()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in moonNames)
            {
                sb.Append(s + "\n\n\n\n");
            }

            return sb.ToString();
        }

        public string[] currentMoonPhase()
        {
            string[] returnString = new string[numOfMoons];
            for (int i = 0; i < numOfMoons; i++)
            {
                switch (moonPhases[i][moonCounters[i]])
                {
                    case moonPhase.full:
                        returnString[i] =  "Full Moon";
                        break;
                    case moonPhase.waningGib:
                        returnString[i] =  "Waning Gibbous";
                        break;
                    case moonPhase.lastQuarter:
                        returnString[i] =  "Last Quarter";
                        break;
                    case moonPhase.waningCresc:
                        returnString[i] =  "Waning Crescent";
                        break;
                    case moonPhase.newMoon:
                        returnString[i] =  "New Moon";
                        break;
                    case moonPhase.waxingCrsec:
                        returnString[i] =  "Waxing Crescent";
                        break;
                    case moonPhase.firstQuarter:
                        returnString[i] =  "First Quarter";
                        break;
                    case moonPhase.waxingGib:
                        returnString[i] =  "Waxing Gibbous";
                        break;
                    default:
                        returnString[i] = null;
                        break;
                }
            }
            return returnString;
        }

        public static string returnGivenDate(int m, int d, int y)
        {
            StringBuilder dateString = new StringBuilder();
            if (m > numMonthsInYear || m <= 0 || d > numDaysInMonth[m] || d <= 0)
                return null;

            dateString.Append(weekdayNames[determineDayOfWeek(m, d, y)]);
            dateString.Append(", " + monthNames[m] + " " + d);
            dateString.Append(", " + y);
            return dateString.ToString();
        }

        public static string returnGivenDate(string dateString)
        {
            if (dateString.Length == 8)
            {
                return returnGivenDate(Int32.Parse(dateString.Substring(0, 2)), Int32.Parse(dateString.Substring(2, 2)), Int32.Parse(dateString.Substring(4, 4)));
            }

            else
                return null;
        }


        public string returnConciseDate()
        {
            return returnGivenDate(month, day, year);
        }

#endregion


        #region Date relation functions, sameDate, isAnniversary, yearsAgo, farthestInTime

        public bool sameDate(int testM, int testD, int testY)
        {
            if (testM == month && testD == day && testY == year)
                return true;
            else
                return false;
        }

        public bool sameDate(string testDate)
        {
            if (testDate.Length != 8)
                return false;
            else
                return sameDate(Int32.Parse(testDate.Substring(0, 2)), Int32.Parse(testDate.Substring(2, 2)), Int32.Parse(testDate.Substring(4, 4)));
        }

        public bool isAnniversary(string testDate)
        {
            if (testDate.Length != 8)
                return false;
            else
                return isAnniversary(Int32.Parse(testDate.Substring(0, 2)), Int32.Parse(testDate.Substring(2, 2)));
        }

        public bool isAnniversary(int testM, int testD)
        {
            if (testM == month && testD == day)
                return true;
            else
                return false;
        }

        public int yearsAgo(int y)
        {
            return year - y;
        }

        public int yearsAgo(string inputDate)
        {
            return yearsAgo(Int32.Parse(inputDate.Substring(4, 4)));
        }

        public static int FarthestInTime(string date1, string date2)
        {
            int year1 = Int32.Parse(date1.Substring(4, 4));
            int year2 = Int32.Parse(date2.Substring(4, 4));
            int month1 = Int32.Parse(date1.Substring(0, 2));
            int month2 = Int32.Parse(date2.Substring(0, 2));
            int day1 = Int32.Parse(date1.Substring(2, 2));
            int day2 = Int32.Parse(date2.Substring(2, 2));

            if (year1 > year2)
                return 1;
            else if (year2 > year1)
                return -1;
            else // if year1 == year2
            {
                if (month1 > month2)
                    return 1;
                else if (month2 > month1)
                    return -1;
                else // if month1 == month2
                {
                    if (day1 > day2)
                        return 1;
                    else if (day2 > day1)
                        return -1;
                    else // day1 == day 2
                        return 0;
                }
            }

        }

        #endregion

        public override string ToString()
        {
            StringBuilder stringDate = new StringBuilder();
            if (month < 10)
                stringDate.Append("0" + month);
            else
                stringDate.Append(month);

            if (day < 10)
                stringDate.Append("0" + day);
            else
                stringDate.Append(day);

            string yString = year.ToString();
            while (yString.Length < 4)
                yString = yString.Insert(0, "0");

            stringDate.Append(yString);

            return stringDate.ToString();
        }

        #region functions for format enforcement
        /// <summary>
        /// Takes a number and changes it to a valid month number if it is not already one
        /// (if a number is larger than 12, returns 12, for example)
        /// </summary>
        /// <param name="testMonth">input number to test as a month</param>
        /// <returns>returns a correct month number</returns>
        public static string enforceMonthFormat(string testMonth)
        {
            if (testMonth.Length < 2)
            {
                if (testMonth.Length == 0)
                    testMonth = "0" + testMonth;
                testMonth = "0" + testMonth;
            }

            if (testMonth == "00" || testMonth == "")
            {
                testMonth = "01";
            }

            if (testMonth.Length > 2 || Int32.Parse(testMonth) > numMonthsInYear)
            {
                testMonth = numMonthsInYear.ToString();
            }
            return testMonth;
        }

        //TODO: Prevent days and months from being above 2 digits, and year from being above 4

        /// <summary>
        /// Returns a valid day value, depending on what year and month
        /// </summary>
        /// <param name="month">month value used to determine valid day (some months have 31 days)</param>
        /// <param name="testDay">day value, being tested</param>
        /// <param name="year">year value used to determine valid day (possible leap year)</param>
        /// <returns>returns a valid day value corresponding with the month and year</returns>
        public static string enforceDayFormat(string month, string testDay, string year)
        {
            if (testDay.Length < 2)
            {
                if (testDay.Length == 0)
                    testDay = "0" + testDay;
                testDay = "0" + testDay;
            }

            if (testDay == "00" || testDay == "")
            {
                testDay = "01";
            }
            if (year != "" && month != "")
            {
                testDay = verifyDay(month + testDay + year).ToString();
                if (testDay.Length == 1)
                    testDay = "0" + testDay;
            }
            else if (Int32.Parse(testDay) > numDaysInMonth[Int32.Parse(month)])
            {
                testDay = numDaysInMonth[Int32.Parse(month)].ToString();
            }

            return testDay;
        }

        /// <summary>
        /// Returns year in valid format, input of 0 returns 0000, 1 returns 0001, etc.
        /// </summary>
        /// <param name="testYear">year being tested</param>
        /// <returns>formatted year</returns>
        public static string enforceYearFormat(string testYear)
        {
            if (testYear.Length > 4)
                testYear = testYear.Substring(0, 4);

            if (testYear.Length == 3)
                testYear = "0" + testYear;

            if (testYear.Length == 2)
                testYear = "00" + testYear;

            if (testYear.Length == 1)
                testYear = "000" + testYear;

            if (testYear == "")
                testYear = "0000";

            return testYear;
        }

        public static int verifyDay(int m, int d, int y)
        {
            if (m > numDaysInYear || m < 1)
                return d;
            if (d <= numDaysInMonth[m])
                return d;
            else if (d <= 0)
                return 1;
            else
                return d;
        }

        public static int verifyDay(string date)
        {
            return verifyDay(Int32.Parse(date.Substring(0, 2)), Int32.Parse(date.Substring(2, 2)), Int32.Parse(date.Substring(4, 4)));
        }
        #endregion

        #region 'dateIn' and 'daysTo' and 'daysBetween' functions

        /// <summary>
        /// Calculates the date after numDays days
        /// </summary>
        /// <param name="numDays">number of days</param>
        /// <returns></returns>
        public string dateIn(int numDays)
        {
            return dateIn(month, day, year, numDays);
        }

        /// <summary>
        /// Finds the date it will be from startDate after (numDays) days pass
        /// </summary>
        /// <param name="startDate">Starting date, MMDDYYYY </param>
        /// <param name="numDays">Number of days that pass</param>
        /// <returns></returns>
        public static string dateIn(string startDate, int numDays)
        {
            return dateIn(Int32.Parse(startDate.Substring(0, 2)), Int32.Parse(startDate.Substring(2, 2)), Int32.Parse(startDate.Substring(4, 4)), numDays);
        }

        /// <summary>
        /// Finds the date it will be from startDate after (numDays) days pass 
        /// </summary>
        /// <param name="startMonth">Starting date's month</param>
        /// <param name="startDay">Starting date's day</param>
        /// <param name="startYear">starting date's year</param>
        /// <param name="numDays">Number of days that pass</param>
        /// <returns></returns>
        public static string dateIn(int startMonth, int startDay, int startYear, int numDays)
        {

            if (numDaysInMonth[startMonth] < startDay + numDays)
            {
                do
                {
                    numDays -= numDaysInMonth[startMonth] - startDay;
                    if (numDays > 0)
                    {
                        startMonth++;
                        if (startMonth > numMonthsInYear)
                        {
                            startMonth = 1;
                            startYear++;
                        }
                        startDay = 0;
                    }
                } while (numDays >= numDaysInMonth[startMonth]);
                // if numdays is 0, means the date landed at the very end of the month, assign d to numDays unless it's 0
                startDay = numDays != 0 ? numDays : numDaysInMonth[startMonth] - startDay;
            }
            else
            {
                startDay += numDays;
            }

            string monthString = enforceMonthFormat(startMonth.ToString());
            string yearString = enforceYearFormat(startYear.ToString());
            string dayString = enforceDayFormat(monthString, startDay.ToString(), yearString);
            return monthString + dayString + yearString;
        }

        /// <summary>
        /// returns the amount of days between current date and input date
        /// TODO: Make this not disgusting
        /// </summary>
        /// <param name="toMonth"></param>
        /// <param name="toDay"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public int daysTo(int toMonth, int toDay, int toYear)
        {
            return daysBetween(month, day, year, toMonth, toDay, toYear);
           /* int m = month;
            int d = day;
            int y = year;
            int numDays = 0;

            if (m != toMonth && toYear == y)
            {
                while (toMonth != m)
                {
                    numDays += numDaysInMonth[m] - d;
                    d = 0;
                    if (++m > numMonthsInYear)
                    {
                        m = 1;
                        y++;
                    }
                }
                numDays += toDay;
            }
            else if (m == toMonth && toYear == y && toDay > d)
            {
                numDays = toDay - d;
            }
            else if (toYear != y)
            {
                while (toYear - y > 2)
                {
                    numDays += numDaysInYear;
                    y++;
                }
                while (toMonth != m || toYear != y)
                {
                    numDays += numDaysInMonth[m] - d;
                    d = 0;
                    if (++m > numMonthsInYear)
                    {
                        m = 1;
                        y++;
                    }
                }
                numDays += toDay;
            }
            return numDays;*/
        }

        /// <summary>
        /// Calculates how many days there are between current date and input date
        /// </summary>
        /// <param name="dateString">Input date formatted as MMDDYYYY</param>
        /// <returns></returns>
        public int daysTo(string dateString)
        {
            return daysTo(Int32.Parse(dateString.Substring(0, 2)), Int32.Parse(dateString.Substring(2, 2)), Int32.Parse(dateString.Substring(4, 4)));
        }

        /// <summary>
        /// Calculates how many days between the 'begin' date and 'to' date
        /// </summary>
        /// <param name="beginMonth">beginning date's month</param>
        /// <param name="beginDay">beginning date's day</param>
        /// <param name="beginYear">beginning date's year</param>
        /// <param name="toMonth">target date's month</param>
        /// <param name="toDay">target date's day</param>
        /// <param name="toYear">target date's year</param>
        /// <returns>days between the input date</returns>
        public static int daysBetween(int beginMonth, int beginDay, int beginYear, int toMonth, int toDay, int toYear)
        {
            int numDays = 0;

            if (beginMonth != toMonth && toYear == beginYear)
            {
                while (toMonth != beginMonth)
                {
                    numDays += numDaysInMonth[beginMonth] - beginDay;
                    beginDay = 0;
                    if (++beginMonth > numMonthsInYear)
                    {
                        beginMonth = 1;
                        beginYear++;
                    }
                }
                numDays += toDay;
            }
            else if (beginMonth == toMonth && toYear == beginYear && toDay > beginDay)
            {
                numDays = toDay - beginDay;
            }
            else if (toYear != beginYear)
            {
                while (toYear - beginYear > 2)
                {
                    numDays += numDaysInYear;
                    beginYear++;
                }
                while (toMonth != beginMonth || toYear != beginYear)
                {
                    numDays += numDaysInMonth[beginMonth] - beginDay;
                    beginDay = 0;
                    if (++beginMonth > numMonthsInYear)
                    {
                        beginMonth = 1;
                        beginYear++;
                    }
                }
                numDays += toDay;
            }
            return numDays;
        }

        /// <summary>
        /// Calculates how many days between the 'begin' date and 'to' date
        /// </summary>
        /// <param name="beginDate">starting date formatted as MMDDYYYY</param>
        /// <param name="toDate">target date formatted as MMDDYYYY</param>
        /// <returns>days between the input date</returns>
        public static int daysBetween(string beginDate, string toDate)
        {
            return daysBetween(Int32.Parse(beginDate.Substring(0, 2)), Int32.Parse(beginDate.Substring(2, 2)), Int32.Parse(beginDate.Substring(4, 4)),
                Int32.Parse(toDate.Substring(0, 2)), Int32.Parse(toDate.Substring(2, 2)), Int32.Parse(toDate.Substring(4, 4)));
        }
        #endregion
    }
}
