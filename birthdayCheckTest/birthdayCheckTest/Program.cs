using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Text;
//TODO: Introduce exception handling for improper input
//If input does not fit correct format, program should use 'continue' and reset to beginning of loop

//Rewritten with functions handling various tasks. Cleaner than original code
//Also put main function in while loop so program doesn't end after single execution of code

//This is an attempt to make a birthday checker
//User will enter DOB, program will compare that to current day
//If the DOB indicates the person is 21 or, program will display VALID SALE
//This is a test of concept
//I just want to see if I can do it

namespace birthdayCheckTest
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter DOB: ");
                string input = Console.ReadLine();
                
                if (getAge((readInput(input))) >= 21) //simple conditional. Does the date of birth put the person at or over 21 years old
                {
                    Console.WriteLine($"VALID SALE ({getAge(readInput(input))})");
                }
                else
                {
                    Console.WriteLine($"DENY SALE({getAge(readInput(input))})");
                }



            }

            //I can do it
        }

        static LocalDateTime readInput(string userInput)
        {
            
            LocalDateTimePattern pattern = LocalDateTimePattern.CreateWithInvariantCulture("MMddyy"); //set pattern for parsing from string

            ParseResult<LocalDateTime> parseResult = pattern.Parse(userInput); //parse string from input

            LocalDateTime dob = parseResult.Value; //returns value of parse result

            //date of birth set as variable 'dob'

            return dob;
        }

        static LocalDateTime getNow()
        {
            Instant today = SystemClock.Instance.Now; //gets current date from system clock

            var timeZone = DateTimeZoneProviders.Tzdb["US/Central"]; //gets US Central as Time zone

            ZonedDateTime zonedToday = today.InZone(timeZone); //converts instant variable to ZonedDateTime in Central Time

            LocalDateTime localToday = zonedToday.LocalDateTime; //Converts ZonedDateTime to LocalDateTime, suitable for Period

            //final, useable value for today set as variable 'localToday'

            return localToday;
        }

        static long getAge(LocalDateTime dob)
        {
            Period agePeriod = Period.Between(dob, getNow()); //gets amount of time between two dates           

            long age = agePeriod.Years; //gets years component of period. Component Years takes long data type

            return age;

            //this is the variable to use for age. We don't care about months, weeks, days, etc

            //Only LocalDateTime variables can use Period. Instant variable must be converted to LocalDateTime via ZonedDateTime
        }
    }
}
