namespace Data_Access
{
    public interface IEmployeeRepository
    {
        Result InsertEmployee(EmployeeDTO employeeDTO);

        List<EmployeeDTO> GetRecentEmployee(int count);

        Result UpdateEmployee(EmployeeDTO employeeDTO);

        Result DeleteEmployee(EmployeeDTO employeeDTO);


        EmployeeDTO GetUser(string username);
        bool CheckPassword(EmployeeDTO employeeDTO, string oldPassword);

        Result ChangePassword(EmployeeDTO employeeDTO, string oldPassword, string newPassword);



    }
}