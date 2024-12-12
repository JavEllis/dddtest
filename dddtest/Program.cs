using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    //sets passcodes for the ST and PT so only them can access there part 
    const string PS_PASSCODE = "12345";
    const string ST_PASSCODE = "67890";
    const string SUPERVISOR_NAME = "Nia Lee";
    const string TUTOR_NAME = "Andy Grey";

    static void Main(string[] args)
    {
        //asks user to select the role they what to access
        Console.WriteLine("Welcome to our program, please select what the following applies to you:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Personal Supervisor (PS)");
        Console.WriteLine("3. Senior Tutor (ST)");
        string role = Console.ReadLine();

        switch (role)
        {
            //calls functions based on the role they have selected 
            case "1":
                StudentInteraction();
                break;
            case "2":
                PSInteraction();
                break;
            case "3":
                STInteraction();
                break;
            default:
                Console.WriteLine("Invalid role, Please select the correct role.");
                break;
        }
    }

    static void StudentInteraction()
    {
        //handles student interactions
        Console.Write("please Enter your correct name name: ");
        string studentName = Console.ReadLine();

        //asks user to select how they are feeling through selections 
        Console.WriteLine("hello, How are you feeling right now?");
        string[] feelings = { "not good", "unsure", "ok", "good", "great" };
        for (int i = 0; i < feelings.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {feelings[i]}");
        }
        int feelingChoice = int.Parse(Console.ReadLine());
        string feeling = feelings[feelingChoice - 1];

        //asks user to select the reason why they are reporting
        Console.WriteLine("Are you self-reporting about:");
        Console.WriteLine("1. Current feelings");
        Console.WriteLine("2. University/School related");
        string reportType = Console.ReadLine() == "1" ? "current" : "university/school";

        Console.Write("Please describe in detail what happened and what you are current feeling: ");
        string statusDescription = Console.ReadLine();
        // asks user if they would like to book a meeting with nia lee

        Console.WriteLine("Would you like to book a meeting with personal supervisor nia lee?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        string bookMeetingChoice = Console.ReadLine();
        string meetingInfo = "No meeting booked";
        if (bookMeetingChoice == "1")
        {
            (string date, string time) = BookMeeting();
            meetingInfo = $"{date} at {time} with {SUPERVISOR_NAME}";
        }

        string data = $"{studentName}\n{feeling}\n{statusDescription}\n{bookMeetingChoice}\n{meetingInfo}";
        SaveToFile(data);
        Console.WriteLine("Your status has been saved, hopeful yo see you really soon, goodbye and have a nive day :)).");
    }

    static (string, string) BookMeeting()
    {
        //asks user the date and time they want to have their meeting with the personal supervisor 
        Console.Write("Enter the date for the meeting (YYYY-MM-DD): ");
        string date = Console.ReadLine();
        Console.WriteLine("Available time slots:");
        Console.WriteLine("1. 12pm to 1pm");
        Console.WriteLine("2. 2pm to 3pm");
        Console.WriteLine("3. 4pm to 5pm");
        Console.WriteLine("4. 6pm to 7pm");
        string timeSlot = Console.ReadLine();
        string[] timeSlots = { "12pm to 1pm", "2pm to 3pm", "4pm to 5pm", "6pm to 7pm" };
        string time = timeSlots[int.Parse(timeSlot) - 1];
        return (date, time);
    }

    //saves user data to the text file 
    static void SaveToFile(string data)
    {
        using (StreamWriter sw = new StreamWriter("info.txt", true))
        {
            sw.WriteLine(data);
        }
    }

    static void PSInteraction()
    {
        // asks personal supervisor to enter their passcode and if they would like to review student status or to book a meeting with students 
        Console.Write("Enter the PS passcode: ");
        string passcode = Console.ReadLine();
        if (passcode == PS_PASSCODE)
        {
            Console.WriteLine("Access granted welcome to the system nia lee.");
            Console.WriteLine("1. Review student statuses");
            Console.WriteLine("2. Book a meeting with a student");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                ReviewStudentStatuses();
            }
            else if (choice == "2")
            {
                BookMeetingWithStudent();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        else
        {
            Console.WriteLine("Access denie, please try again");
        }
    }

    static void ReviewStudentStatuses()
    {
        if (File.Exists("info.txt"))
        {
            string[] lines = File.ReadAllLines("info.txt");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("No student statuses found.");
        }
    }

    static void BookMeetingWithStudent()
    {
        Console.Write("Enter the student's name: ");
        string studentName = Console.ReadLine();
        (string date, string time) = BookMeeting();
        string meetingInfo = $"{date} at {time} with {SUPERVISOR_NAME}";
        string data = $"{studentName}\nMeeting booked\n{meetingInfo}";
        SaveToFile(data);
        Console.WriteLine("Meeting has been booked.thanking for booking with us");
    }

    static void STInteraction()
    {
        // asks Senior tutor to enter their passcode and if they would like to review student status 
        Console.Write("Enter the ST passcode: ");
        string passcode = Console.ReadLine();
        if (passcode == ST_PASSCODE)
        {
            Console.WriteLine("Access granted, welcome to our system andy grey.");
            ReviewStudentStatuses();
        }
        else
        {
            Console.WriteLine("Access denied.");
        }
    }
}