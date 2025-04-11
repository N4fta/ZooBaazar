namespace Data_Access
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public DateTime Birthday { get; set; }
        public List<ContractDTO> Contracts { get; set; }
    }
}
