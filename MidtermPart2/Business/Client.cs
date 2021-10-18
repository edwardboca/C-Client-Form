using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MidtermPart2.DataAccess;

namespace MidtermPart2.Business
{
    public class Client // starts with a capital letter; singular noun
    {
        //private class variables
        // private : to implement the concept Encapsulation
        private int ClientId;
        private string firstName;
        private string lastName;
        private string jobTitle;

        //properties
        public int ClientId { get => ClientId; set => ClientId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }

        public void SaveClient(Client emp)
        {
            ClientDB.SaveRecord(emp);
        }
        public Client SearchClient(int empId)
        {

            return ClientDB.SearchRecord(empId);
        }
        public List<Client> SearchClient(string name)
        {

            return ClientDB.SearchRecord(name);
        }

        public List<Client> ListClients()
        {
            return ClientDB.ListAllRecords();
        }

        public void UpdateClient(Client emp)
        {
            ClientDB.UpdateRecord(emp);
        }

        public void DeleteClient(int eId)
        {
            ClientDB.DeleteRecord(eId);
        }
        public void DeleteRecord(Client emp)
        {
            ClientDB.DeleteRecord(emp);
        }
        public Client SearchRecord(int empId)
        {
            ClientDB.SearchRecord(empId);
            return ClientDB.SearchRecord(empId);
        }
        public List<Client> SearchRecord(string input)
        {
            return ClientDB.SearchRecord(input);
        }
    }
}
