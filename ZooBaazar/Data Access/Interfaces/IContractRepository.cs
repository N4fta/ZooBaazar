namespace Data_Access
{
    public interface IContractRepository
    {
        Result InsertContract(ContractDTO contractDTO);

        List<ContractDTO> GetEmployeeContract(int employeeID);

        Result UpdateEmployeeContract(ContractDTO contractDTO);

        Result DeleteEmployeeContract(ContractDTO contractDTO);

    }
}