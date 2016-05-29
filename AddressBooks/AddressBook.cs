using AddressBooks_Data;
using System;
using System.Collections.Generic;

namespace AddressBooks
{
    public class AddressBook
    {
        List<Users> addresses;
        public AddressBook()
        {
            addresses = new List<Users>();
        }
        private readonly string lastName = string.Empty;

        //Події при додаванні та видаленні користувача
        public event EventHandler UserAdded;
        public event EventHandler UserRemoved;
         
        private void UserAddedEvent()
        {
            UserAdded?.Invoke(this, EventArgs.Empty);
        }
        private void UserRemovedEvent()
        {
            UserRemoved?.Invoke(this, EventArgs.Empty);    
        }

        public bool AddUser(Users user)
        {
            Users users = new Users();
            Users result = Find(lastName);

            if (result == null)
            {
                addresses.Add(users);
                UserAddedEvent();
                return true;
            }
            else
            {
                return false;
                throw new Exception("Cannot add a new user!");
            }
        }

        public bool RemoveUser(string lastName)
        {
            Users users = Find(lastName);
             
            if (users == null)
            {
                addresses.Remove(users);
                UserRemovedEvent();
                return true;
            }
            else
            {
                return false;
                throw new Exception("Cannot remove a user!");
            }
        }

        public Users Find(string lastName)
        {
            Users users = addresses.Find(
              delegate (Users u)
              {
                  return u.LastName == lastName;
              }
            );
            return users;
        }
    }
}
