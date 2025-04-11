using Data_Access;

namespace Logic
{
    public class ContractManager
    {
        public List<Contract> contracts;
        private readonly IContractRepository contractRepository;
        private readonly LocationManager locationManager;
        private readonly ILocationRepository ilocationRepository;


        public ContractManager(IContractRepository icntRep)
        {
            contracts = new List<Contract>();
            contractRepository = icntRep;
            ilocationRepository = new LocationRepository();
            //locationManager = new(ilocationRepository);

        }

        public Contract LoadEmployeeContract(EmployeeDTO employeeDTO)
        {
            List<ContractDTO> contractDTOs = contractRepository.GetEmployeeContract(employeeDTO.EmployeeID);

            if (contractDTOs.Count == 0)
            {
                return null;
            }
            return ConvertToContract(contractDTOs[0]);
        }

        public Result Add(Contract contract)
        {
            ContractDTO contractDTO = ConvertToContractDTO(contract);
            
            Result resultAdd = SendToTheDb(contractDTO);

            return resultAdd;
        }

        public bool Remove(Contract contract)
        {
            return contracts.Remove(contract);
        }

        public Contract[] GetArray()
        {
            return contracts.ToArray();
        }

        public Result Update(Contract newContract)
        {
            ContractDTO contractDTO = ConvertToContractDTO(newContract);
            
            if (newContract.ContractID == 0)
            {
                return Add(newContract);
            }

            Result resultUpdated = contractRepository.UpdateEmployeeContract(contractDTO);

            return resultUpdated;
        }

        public Result SendToTheDb(ContractDTO newContract)
        {
            return contractRepository.InsertContract(newContract);
        }

        public ContractDTO ConvertToContractDTO(Contract contract)
        {
            // this converts contract to ContractDTO
            var contractDTO = new ContractDTO();
            contractDTO.ContractID = contract.ContractID;
            contractDTO.EmployeeID = contract.EmployeeID;
            contractDTO.startDate = contract.startDate;
            contractDTO.endDate = contract.endDate;
            contractDTO.salary = contract.salary;
            contractDTO.role = contract.role.ToString();
            contractDTO.bsn = contract.bsn;
            contractDTO.bankNumber = contract.bankNumber;
            contractDTO.hoursPerWeek = contract.hoursPerWeek;
            contractDTO.changeShifts = contract.changeShifts;
            contractDTO.overtime = contract.overtime;
            contractDTO.dayShifts = contract.dayShifts;
            contractDTO.nightShifts = contract.nightShifts;
            contractDTO.workDays = contract.workDays;
            contractDTO.paidLeaveDays = contract.paidLeaveDays;
            contractDTO.unpaidLeaveDays = contract.unpaidLeaveDays;
            contractDTO.Notes = contract.Notes;
            contractDTO.contractType = contract.contractType.ToString();

            return contractDTO;

        }

        public static Contract ConvertToContract(ContractDTO contractDTO)
        {
            //Converts the role enum to a string 
            ContractType contractType = (ContractType)Enum.Parse(typeof(ContractType), contractDTO.contractType.Replace(" ", ""));

            //Converts the role enum to a string 
            WorkType role = (WorkType)Enum.Parse(typeof(WorkType), contractDTO.role);


            // this converts contract to ContractDTO
            return new Contract(
                    contractDTO.ContractID,
                    contractDTO.startDate,
                    contractDTO.endDate,
                    contractDTO.salary,
                    role,
                    contractDTO.bsn,
                    contractDTO.bankNumber,
                    contractDTO.hoursPerWeek,
                    contractDTO.changeShifts,
                    contractDTO.overtime,
                    contractDTO.dayShifts,
                    contractDTO.nightShifts,
                    contractDTO.workDays,
                    contractDTO.paidLeaveDays,
                    contractDTO.unpaidLeaveDays,
                    contractDTO.Notes,
                    contractType
                );
        }
    }
}
