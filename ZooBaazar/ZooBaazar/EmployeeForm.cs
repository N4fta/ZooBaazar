using Data_Access;
using Logic;
using Logic.ScheduleStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ZooBaazar
{
    public partial class AddEmployeeForm : Form
    {
        public Employee NewEmployee { get; private set; }
        private Employee existingEmployee;
        private EmployeeManager employeeManager;
        private LocationManager locationManager;
        private ContractManager contractManager;


        public AddEmployeeForm(LocationManager manager, EmployeeManager em, ContractManager cm, Employee employee = null, bool view = false)
        {
            employeeManager = em;
            locationManager = manager;
            contractManager = cm;
            InitializeComponent();
            PopulateComboBoxes();
            PopulateCheckedListBox();
            existingEmployee = employee;

            if (existingEmployee != null)
            {
                FillFormWithEmployeeDetails(existingEmployee);
            }


            if (view)
            {
                tbBankNumber.Enabled = false;
                tbBSN.Enabled = false;
                tbAdress.Enabled = false;
                tbNameEmployeeForm.Enabled = false;
                tbPasswordEmployeeForm.Enabled = false;
                tbPhoneNumber.Enabled = false;
                tbUsernameEmployeeForm.Enabled = false;
                tbEmail.Enabled = false;
                tbHoursPerWeek.Enabled = false;
                tbNotesEmployeeForm.Enabled = false;
                tbPaidLeaveDays.Enabled = false;
                tbSalary.Enabled = false;
                tbUnpaidLeaveDays.Enabled = false;
                dtpDateOfBirth.Enabled = false;
                chkChangeShifts.Enabled = false;
                chkOvertime.Enabled = false;
                cbRole.Enabled = false;
                chkDayShifts.Enabled = false;
                chkNightShifts.Enabled = false;
                cbContractType.Enabled = false;
                dtpStartDate.Enabled = false;
                tbContractNotes.Enabled = false;
                dtpEndDate.Enabled = false;
                btnAddEmployeeForm.Enabled = false;
                clbWorkDays.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            // Validate input data
            if (ValidateInput())
            {
                // Create a new Contract object for the employee
                if (existingEmployee == null)
                {
                    Contract employeeNewContract = CreateContract(); // You need to define your CreateContract method
                                                                     // Create a new employee object with the provided details and contracts
                    Employee newEmployee = existingEmployee == null ?

                    new Employee(
                        tbUsernameEmployeeForm.Text,
                        tbPasswordEmployeeForm.Text,
                        tbNameEmployeeForm.Text,
                        tbEmail.Text,
                        tbPhoneNumber.Text,
                        tbAdress.Text,
                        tbNotesEmployeeForm.Text,
                        dtpDateOfBirth.Value,
                        employeeNewContract,
                        (WorkType)cbRole.SelectedItem
                    ) :
                     new Employee(
                        existingEmployee.EmployeeID,
                        tbUsernameEmployeeForm.Text,
                        tbPasswordEmployeeForm.Text,
                        tbNameEmployeeForm.Text,
                        tbEmail.Text,
                        tbPhoneNumber.Text,
                        tbAdress.Text,
                        tbNotesEmployeeForm.Text,
                        dtpDateOfBirth.Value,
                        employeeNewContract,
                        (WorkType)cbRole.SelectedItem
                    );

                    // Set the NewEmployee property to the newly created employee
                    Result result = employeeManager.Update(newEmployee, employeeNewContract);

                    //Result result = employeeManager.Add(newEmployee);
                    if (result.Success)
                    {
                        // Close the form and return DialogResult.OK
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        tsslblAddEmployeeForm.Text = result.Message;
                    }
                }
                else if (existingEmployee !=null)
                {
                    Contract employeeContract = GetUpdatedContract(); // You need to define your CreateContract method

                    // Create a new employee object with the provided details and contracts
                    Employee newEmployee = existingEmployee == null ?

                    new Employee(
                        tbUsernameEmployeeForm.Text,
                        tbPasswordEmployeeForm.Text,
                        tbNameEmployeeForm.Text,
                        tbEmail.Text,
                        tbPhoneNumber.Text,
                        tbAdress.Text,
                        tbNotesEmployeeForm.Text,
                        dtpDateOfBirth.Value,
                        employeeContract,
                        (WorkType)cbRole.SelectedItem
                    ) :
                     new Employee(
                        existingEmployee.EmployeeID,
                        tbUsernameEmployeeForm.Text,
                        tbPasswordEmployeeForm.Text,
                        tbNameEmployeeForm.Text,
                        tbEmail.Text,
                        tbPhoneNumber.Text,
                        tbAdress.Text,
                        tbNotesEmployeeForm.Text,
                        dtpDateOfBirth.Value,
                        employeeContract,
                        (WorkType)cbRole.SelectedItem
                    );

                    // Set the NewEmployee property to the newly created employee
                    Result result = employeeManager.Update(newEmployee, employeeContract);

                    //Result result = employeeManager.Add(newEmployee);
                    if (result.Success)
                    {
                        // Close the form and return DialogResult.OK
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        tsslblAddEmployeeForm.Text = result.Message;
                    }
                }

            }
        }

        private void FillFormWithEmployeeDetails(Employee employee)
        {
            tbNameEmployeeForm.Text = employee.Name;
            tbEmail.Text = employee.Email;
            tbPhoneNumber.Text = employee.PhoneNumber;
            tbAdress.Text = employee.Address;
            tbUsernameEmployeeForm.Text = employee.Username;
            tbPasswordEmployeeForm.Text = employee.Password;
            dtpDateOfBirth.Value = employee.Birthday;
            tbNotesEmployeeForm.Text = employee.Notes;

            Contract contract = employee.Contract;
            if (contract != null)
            {
                tbBankNumber.Text = contract.bankNumber.ToString();
                tbBSN.Text = contract.bsn.ToString();
                tbSalary.Text = contract.salary.ToString();
                dtpStartDate.Value = contract.startDate;
                dtpEndDate.Value = contract.endDate;
                tbPaidLeaveDays.Text = contract.paidLeaveDays.ToString();
                tbUnpaidLeaveDays.Text = contract.unpaidLeaveDays.ToString();
                tbHoursPerWeek.Text = contract.hoursPerWeek.ToString();
                chkChangeShifts.Checked = contract.changeShifts;
                chkOvertime.Checked = contract.overtime;
                chkDayShifts.Checked = contract.dayShifts;
                chkNightShifts.Checked = contract.nightShifts;
                cbRole.SelectedItem = contract.role;
                tbContractNotes.Text = contract.Notes;
                cbContractType.SelectedItem = contract.contractType;

                foreach (var day in contract.workDays)
                {
                    int index = clbWorkDays.Items.IndexOf(day.ToString());
                    if (index != -1)
                    {
                        clbWorkDays.SetItemChecked(index, true);
                    }
                }
            }

        }

        public Contract CreateContract()
        {
            try
            {
                // Gather contract details from form controls
                int contractid = 0;
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                double salary = double.Parse(tbSalary.Text);
                WorkType role = (WorkType)cbRole.SelectedItem;
                int bsn = int.Parse(tbBSN.Text);
                int bankNumber = int.Parse(tbBankNumber.Text);
                int hoursPerWeek = int.Parse(tbHoursPerWeek.Text);
                bool changeShifts = chkChangeShifts.Checked;
                bool overtime = chkOvertime.Checked;
                bool dayShifts = chkDayShifts.Checked;
                bool nightShifts = chkNightShifts.Checked;

                // Convert selected items in CheckedListBox to DayOfWeek enum values
                List<DayOfWeek> workDays = clbWorkDays.CheckedItems.Cast<string>()
                                                .Select(item => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item))
                                                .ToList();

                int paidLeaveDays = int.Parse(tbPaidLeaveDays.Text);
                int unpaidLeaveDays = int.Parse(tbUnpaidLeaveDays.Text);
                string notes = tbContractNotes.Text;
                ContractType contractType = (ContractType)cbContractType.SelectedItem;

                // Validate contract details
                if (startDate > endDate)
                {
                    tsslblAddEmployeeForm.Text = "End date must be after start date. Validation Error";
                    return null;
                }

                // Create and return an instance of the Contract class
                return new Contract (startDate, endDate, salary, role, bsn, bankNumber, hoursPerWeek,
                    changeShifts, overtime, dayShifts, nightShifts, workDays, paidLeaveDays, unpaidLeaveDays, notes, contractType);
            }
            catch (FormatException)
            {
                tsslblAddEmployeeForm.Text = "Invalid input format. Validation Error";

            }
            catch (ArgumentNullException)
            {
                tsslblAddEmployeeForm.Text = "Please fill in all required fields. Validation Error";
            }
            catch (NullReferenceException)
            {
                tsslblAddEmployeeForm.Text = "Please select a contract type. Validation Error";
            }
            catch (Exception ex)
            {
                tsslblAddEmployeeForm.Text = "An error occurred: " + ex.Message;
            }

            return null;
        }


        public Employee GetNewEmployee()
        {
            if (ValidateInput())
            {
                //// Create a new Schedule object for the employee
                //Schedule employeeSchedule = new Schedule(); // You need to define how the Schedule object is created

                // Create a new Contract object for the employee
                Contract employeeContract = CreateContract(); // You need to define your CreateContract method

                // Create and return a new Employee object with the provided details and contracts
                Employee newEmployee = new Employee(
                    tbUsernameEmployeeForm.Text,
                    tbPasswordEmployeeForm.Text,
                    tbNameEmployeeForm.Text,
                    tbEmail.Text,
                    tbPhoneNumber.Text,
                    tbAdress.Text,
                    tbNotesEmployeeForm.Text,
                    dtpDateOfBirth.Value,
                    employeeContract,
                    (WorkType)cbRole.SelectedItem
                );
                return newEmployee;
            }
            else
            {
                // Return null if validation fails
                return null;
            }
        }

        public Employee GetUpdatedEmployee()
        {
            if (ValidateInput())
            {
                //// Create a new Schedule object for the employee
                //Schedule employeeSchedule = new Schedule(); // You need to define how the Schedule object is created

                // Create a new Contract object for the employee
                Contract employeeContract = CreateContract(); // You need to define your CreateContract method

                // Create and return a new Employee object with the provided details and contracts
                employeeManager.Update(
                    new Employee(
                        existingEmployee.EmployeeID,
                    tbUsernameEmployeeForm.Text,
                    tbPasswordEmployeeForm.Text,
                    tbNameEmployeeForm.Text,
                    tbEmail.Text,
                    tbPhoneNumber.Text,
                    tbAdress.Text,
                    tbNotesEmployeeForm.Text,
                    dtpDateOfBirth.Value,
                    employeeContract,
                    (WorkType)cbRole.SelectedItem),
                    employeeContract
                    );
                return new Employee(
                    tbUsernameEmployeeForm.Text,
                    tbPasswordEmployeeForm.Text,
                    tbNameEmployeeForm.Text,
                    tbEmail.Text,
                    tbPhoneNumber.Text,
                    tbAdress.Text,
                    tbNotesEmployeeForm.Text,
                    dtpDateOfBirth.Value,
                    //employeeSchedule,
                    employeeContract,
                    (WorkType)cbRole.SelectedItem);
            }
            else
            {
                return null;
            }
        }

        public Contract GetUpdatedContract()
        {
            if (ValidateInput())
            {

                List<DayOfWeek> selectedWorkDays = new List<DayOfWeek>();
                foreach (var item in clbWorkDays.CheckedItems)
                {
                    if (Enum.TryParse(item.ToString(), out DayOfWeek workDay))
                    {
                        selectedWorkDays.Add(workDay);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Invalid work day: {item}");
                    }
                }

                contractManager.Update(
                    new Contract(
                        existingEmployee.Contract.ContractID,
                        existingEmployee.EmployeeID,
                        dtpStartDate.Value,
                        dtpEndDate.Value,
                        double.Parse(tbSalary.Text),
                        (WorkType)cbRole.SelectedItem,
                        int.Parse(tbBSN.Text),
                        int.Parse(tbBankNumber.Text),
                        int.Parse(tbHoursPerWeek.Text),
                        chkChangeShifts.Checked,
                        chkOvertime.Checked,
                        chkDayShifts.Checked,
                        chkNightShifts.Checked,
                        selectedWorkDays,
                        int.Parse(tbPaidLeaveDays.Text),
                        int.Parse(tbUnpaidLeaveDays.Text),
                        tbContractNotes.Text,
                        (ContractType)cbContractType.SelectedItem
                        )
                    );

                return new Contract(
                    existingEmployee.Contract.ContractID,
                    existingEmployee.EmployeeID,
                    dtpStartDate.Value,
                    dtpEndDate.Value,
                    double.Parse(tbSalary.Text),
                    (WorkType)cbRole.SelectedItem,
                    int.Parse(tbBSN.Text),
                    int.Parse(tbBankNumber.Text),
                    int.Parse(tbHoursPerWeek.Text),
                    chkChangeShifts.Checked,
                    chkOvertime.Checked,
                    chkDayShifts.Checked,
                    chkNightShifts.Checked,
                    selectedWorkDays,
                    int.Parse(tbPaidLeaveDays.Text),
                    int.Parse(tbUnpaidLeaveDays.Text),
                    tbContractNotes.Text,
                    (ContractType)cbContractType.SelectedItem
                    );
            }
            else
            {
                return null;
            }
        }

        private bool ValidateInput()
        {
            // Validate Employee Name
            if (string.IsNullOrWhiteSpace(tbNameEmployeeForm.Text))
            {
                //MessageBox.Show("Please enter a valid employee name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsslblAddEmployeeForm.Text = "Please enter a valid employee name. Validation Error";
                return false;
            }

            // Validate Employee Email
            if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
            {
                //MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsslblAddEmployeeForm.Text = "Please enter a valid email address. Validation Error";
                return false;
            }

            // Validate Employee Phone Number
            if (string.IsNullOrWhiteSpace(tbPhoneNumber.Text))
            {
                //MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsslblAddEmployeeForm.Text = "Please enter a valid phone number. Validation Error";
                return false;
            }

            // Validate Employee Address
            if (string.IsNullOrWhiteSpace(tbAdress.Text))
            {
                //MessageBox.Show("Please enter a valid address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsslblAddEmployeeForm.Text = "Please enter a valid address. Validation Error";
                return false;
            }

            // Validate Employee Date of Birth
            if (dtpDateOfBirth.Value == DateTime.MinValue)
            {
                //MessageBox.Show("Please enter a valid date of birth.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsslblAddEmployeeForm.Text = "Please enter a valid date of birth. Validation Error";
                return false;
            }

            // Validate Employee Username
            if (string.IsNullOrWhiteSpace(tbUsernameEmployeeForm.Text))
            {
                //MessageBox.Show("Please enter a valid username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsslblAddEmployeeForm.Text = "Please enter a valid username. Validation Error";
                return false;
            }

            // Validate Employee Password
            if (string.IsNullOrWhiteSpace(tbPasswordEmployeeForm.Text))
            {
                //MessageBox.Show("Please enter a valid password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsslblAddEmployeeForm.Text = "Please enter a valid password. Validation Error";
                return false;
            }

            // Validate Contract
            if (CreateContract() == null)
            {
                // Validation error message is displayed in CreateContract method
                return false;
            }

            // All validation passes, return true
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void PopulateComboBoxes()
        {
            // Populate cmbRole
            cbRole.DataSource = Enum.GetValues(typeof(WorkType));
            // Populate cmbContractType
            cbContractType.DataSource = Enum.GetValues(typeof(ContractType));
        }

        private void PopulateCheckedListBox()
        {
            // Convert DayOfWeek enum values to string representations
            var dayNames = Enum.GetNames(typeof(DayOfWeek));

            // Add the string representations to the CheckedListBox
            clbWorkDays.Items.AddRange(dayNames);
        }

        private void AddEmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Visible = true;
            }
        }
    }
}
