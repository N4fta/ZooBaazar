using Data_Access;
using Logic.ScheduleStuff;
using System.Reflection.PortableExecutable;

namespace Logic
{
    public class EmployeeManager
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ContractManager contractManager;
        private readonly IContractRepository contractRepository;

        public EmployeeManager(IEmployeeRepository empRep, IContractRepository contractRepository)
        {
            employeeRepository = empRep;
            this.contractRepository = contractRepository;
            contractManager = new ContractManager(contractRepository);

        }

        public List<Employee> LoadEmployeesFromDataBase()
        {
            List<Employee> employees = new();
            List<EmployeeDTO> employeeDTOs = new();
            try
            {
                employeeDTOs = employeeRepository.GetRecentEmployee(int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (var employeeDTO in employeeDTOs)
            {
                employees.Add(ConvertToEmployee(employeeDTO));
            }
            return employees;
        }

        public Result Add(Employee employee, Contract contract)
        {
            EmployeeDTO employeeDTO = ConvertToEmployeeDTO(employee);

            if (employeeDTO.Contracts == null)
            {
                employeeDTO.Contracts = new List<ContractDTO>();
            }
            ContractDTO contractDTO = contractManager.ConvertToContractDTO(contract);
            
            employeeDTO.Contracts.Add(contractDTO);

            // Result resultcontract = contractRepository.InsertContract(contractManager.ConvertToContractDTO(employee.Contract));

            Result resultemployee = employeeRepository.InsertEmployee(employeeDTO);

            if (!resultemployee.Success)
            {
                return resultemployee;
            }
            return resultemployee;
        }

        public Result Remove(Employee employee)
        {
            Result resultemployee = employeeRepository.DeleteEmployee(ConvertToEmployeeDTO(employee));
            //contractRepository.DeleteEmployeeContract(contractManager.ConvertToContractDTO(employee.Contract));
            return resultemployee;
        }

        public Result Update(Employee newEmployee, Contract newContract)
        {
            EmployeeDTO employeeDTO = ConvertToEmployeeDTO(newEmployee);
            
            if (newEmployee.EmployeeID == 0)
            {
                return Add(newEmployee, newContract);
            }

            //This calls the repository method to update the User
            return employeeRepository.UpdateEmployee(employeeDTO);
        }

        public static Employee ConvertToEmployee(EmployeeDTO newEmployeeDTO)
        {
            WorkType role = WorkType.None;
            Contract contract = null;
            if (newEmployeeDTO.Contracts.Count != 0)
            {
                //Converts the role enum to a string 
                role = (WorkType)Enum.Parse(typeof(WorkType), newEmployeeDTO.Contracts[0].role);

                contract = ContractManager.ConvertToContract(newEmployeeDTO.Contracts[0]);
            }

            return new Employee(
                    newEmployeeDTO.EmployeeID,
                    newEmployeeDTO.Username,
                    newEmployeeDTO.Password,
                    newEmployeeDTO.Name,
                    newEmployeeDTO.Email,
                    newEmployeeDTO.PhoneNumber,
                    newEmployeeDTO.Address,
                    newEmployeeDTO.Notes,
                    newEmployeeDTO.Birthday,
                    contract,
                    role
                );
        }

        public static EmployeeDTO ConvertToEmployeeDTO(Employee employee)
        {
            // this converts Employee to UserDTO
            var employeeDTO = new EmployeeDTO();
            employeeDTO.EmployeeID = employee.EmployeeID;
            employeeDTO.Username = employee.Username;
            employeeDTO.Password = employee.Password;
            employeeDTO.Name = employee.Name;
            employeeDTO.Email = employee.Email;
            employeeDTO.PhoneNumber = employee.PhoneNumber;
            employeeDTO.Address = employee.Address;
            employeeDTO.Notes = employee.Notes;
            employeeDTO.Birthday = employee.Birthday;

            return employeeDTO;
        }
        ///WebApp
        public bool CheckPassword(EmployeeDTO employeeDTO, string oldPassword)
        {
            return employeeRepository.CheckPassword(employeeDTO, oldPassword);
        }

        public Result ChangePassword(EmployeeDTO employeeDTO, string oldPassword, string newPassword)
        {
            if (!CheckPassword(employeeDTO, oldPassword))
            {
                return new Result { Success = false, Message = "Invalid old password" };
            }

            return employeeRepository.ChangePassword(employeeDTO, oldPassword, newPassword);
        }

        public EmployeeDTO GetUser(string username)
        {
            return employeeRepository.GetUser(username);
        }
    }
}
