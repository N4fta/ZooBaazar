using Logic.ScheduleStuff;

namespace Logic
{
    public class Employee
    {
        public int EmployeeID { get; }
        public string Username { get; }
        public string Password { get; }
        public string Name { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string Address { get; }
        public string Notes { get; set; }
        public DateTime Birthday { get; set; }
        public Contract Contract { get; set; } 
        public WorkType Role { get; set; }

        public Employee(int employeeID,string username, string password, string name, string email, string phoneNumber, string address, string notes, DateTime birthday, Contract contract, WorkType role)
        {
            EmployeeID = employeeID;
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Notes = notes;
            Birthday = birthday;
            Contract = contract;
            Role = role;
        }
        public Employee(string username, string password, string name, string email, string phoneNumber, string address, string notes, DateTime birthday, Contract contract, WorkType role)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Notes = notes;
            Birthday = birthday;
            Contract = contract;
            Role = role;
        }

        public bool CheckPassword(string password)
        {
            if (Password == password) return true;
            else return false;
        }
        public List<string> GetInfo()
        {
            List<string> info = new List<string>
            {
                $"ID: {EmployeeID}",
                $"Name: {Name}",
                $"Username: {Username}",
                $"Birthday: {Birthday.ToString("ddd dd-MM-yyyy")}",
                $"Email: {Email}",
                $"Phone Number: {PhoneNumber}",
                $"Address: {Address}",
                $"Progress report: {Notes}"
            };
            return info;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
