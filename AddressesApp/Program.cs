using System;
using AddressBooks;
using AddressBooks_Data;
using AddressBooks_Logger;
using Logger.Strategy;

namespace AddressBooks_Main
{
    class Program
    {
        static Loggers logger;
        static void Main(string[] args)
        {
            AddressBook addressBook = AddressBook.GetInstance();
            addressBook.UserAdded += new EventHandler(UserAddedEvent);
            addressBook.UserRemoved += new EventHandler(UserRemovedEvent);

            ConsoleLogger cl = new ConsoleLogger();
            WindowsEventLogger wl = new WindowsEventLogger();
            FileLogger fl = new FileLogger();
            //Comment if WindowsEventLogger
            //or FileLogger not working
            logger = new Loggers(wl);  
            logger = new Loggers(fl);  
            logger = new Loggers(cl);

            //logger.Debug test
            logger.Debug("AddressApp started at: ");

            //Add user for test
            Users user = new Users();
            try
            {
                user.LastName = "Barnes";
                user.FirstName = "Bill";
                user.BirthDate = new DateTime(1990, 1, 18);
                user.TimeAdded = DateTime.Now;
                user.City = "Lviv";
                user.Address = "ul. Gorodoc'ka, 100";
                user.PhoneNumber = "+380951234567";
                user.Gender = GenderEnum.Male;
                user.Email = "Barnes@gmail.com";

                addressBook.AddUser(user);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            user.ShowUsers();

            //Delete user
            try
            {
                string lastName = "Barnes";
                addressBook.RemoveUser(lastName);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            Console.ReadKey();

            logger.Debug("AddressApp closed at: ");
        }

        private static void UserAddedEvent(object sender, EventArgs e)
        {
            Console.WriteLine();
            string addMessage = "User was added to address book! ";
            logger.Info(addMessage);
             
        }
        private static void UserRemovedEvent(object sender, EventArgs e)
        {
            Console.WriteLine();
            string removeMessage = "User has been deleted from address book! ";
            logger.Warning(removeMessage);
        }
    }
}
