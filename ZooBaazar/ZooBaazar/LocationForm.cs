using Data_Access;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZooBaazar
{
    public partial class LocationForm : Form
    {
        public Location NewLocation { get; private set; }
        private Location existingLocation;
        private LocationManager _locationManager;
        private AnimalManager animalManager;

        public LocationForm(LocationManager locationManager, AnimalManager amlManager, Location location = null, bool view = false)
        {
            _locationManager = locationManager;
            animalManager = amlManager;
            InitializeComponent();
            InitializeDangerComboBox();
            PopulateSpecies();
            RefreshListBox();
            LoadAnimaCheckListBox();
            existingLocation = location;

            if (existingLocation != null)
            {
                FillFormWithLocationDetails(existingLocation);
            }

            if (view)
            {
                tbNameLocationForm.Enabled = false;
                cbSpecies_LocationForm.Enabled = false;
                tbDescriptionLocationForm.Enabled = false;
                nudCapacity.Enabled = false;
                cbDangerLevel.Enabled = false;
                btnAddLocationForm.Enabled = false;
                tbSearch.Enabled = false;
                lblSearch.Enabled = false;
                lblListOfAnimals.Enabled = false;
                clbListOfAnimals.Enabled = false;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnAddLocation_Click(object sender, EventArgs e)
        {
            // This will validate input data
            if (ValidateLocationInput())
            {
                List<Animal> selectedAnimals = GetSelectedAnimals();

                Location newLocation = existingLocation == null ?
                    new Location(
                        tbNameLocationForm.Text,
                        (int)nudCapacity.Value,
                        selectedAnimals.Count,
                        tbDescriptionLocationForm.Text,
                        (DangerEnum)cbDangerLevel.SelectedItem,
                        cbSpecies_LocationForm.Text,
                        selectedAnimals
                    ) :
                    new Location(
                        existingLocation.Id,
                        tbNameLocationForm.Text,
                        (int)nudCapacity.Value,
                        selectedAnimals.Count,
                        tbDescriptionLocationForm.Text,
                        (DangerEnum)cbDangerLevel.SelectedItem,
                        cbSpecies_LocationForm.Text,
                        selectedAnimals
                    );

                // Set the newLocation property to the newly created location
                Result result = existingLocation == null ? _locationManager.Add(newLocation) : _locationManager.Update(newLocation);

                // Close the form and return DialogResult.OK
                if (result.Success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    tsslblLocationForm.Text = result.Message;
                }
            }
        }


        private List<Animal> GetSelectedAnimals()
        {
            List<Animal> selectedAnimals = new List<Animal>();
            foreach (var item in clbListOfAnimals.CheckedItems)
            {
                selectedAnimals.Add((Animal)item);
            }
            return selectedAnimals;
        }

        private void PopulateSpecies()
        {
            cbSpecies_LocationForm.Items.Clear();
            cbSpecies_LocationForm.DataSource = animalManager.GetAllSpecies();
        }

        private void FillFormWithLocationDetails(Location location)
        {
            // Set the text of various form controls based on the location object
            tbNameLocationForm.Text = location.Name;
            nudCapacity.Value = location.Capacity;
            cbSpecies_LocationForm.Text = location.Species;
            cbDangerLevel.SelectedItem = location.Danger;
            tbDescriptionLocationForm.Text = location.Description;

            // Check the items in the CheckedListBox that are present in the location.Animals list
            foreach (var animal in location.Animals)
            {
                for (int i = 0; i < clbListOfAnimals.Items.Count; i++)
                {
                    if (((Animal)clbListOfAnimals.Items[i]).AnimalID == animal.AnimalID)
                    {
                        clbListOfAnimals.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }


        public Location GetNewLocation()
        {
            if (ValidateLocationInput())
            {
                List<Animal> selectedAnimals = GetSelectedAnimals();
                return new Location(
                    tbNameLocationForm.Text,
                    (int)nudCapacity.Value,
                    selectedAnimals.Count,
                    tbDescriptionLocationForm.Text,
                    (DangerEnum)cbDangerLevel.SelectedItem,
                    cbSpecies_LocationForm.Text,
                    selectedAnimals
                );
            }
            else
            {
                return null;
            }
        }

        public Location GetUpdatedLocation()
        {
            if (ValidateLocationInput())
            {
                List<Animal> selectedAnimals = GetSelectedAnimals();
                return new Location(
                    existingLocation.Id,
                    tbNameLocationForm.Text,
                    (int)nudCapacity.Value,
                    selectedAnimals.Count,
                    tbDescriptionLocationForm.Text,
                    (DangerEnum)cbDangerLevel.SelectedItem,
                    cbSpecies_LocationForm.Text,
                    selectedAnimals
                );
            }
            else
            {
                return null;
            }
        }

        private void InitializeDangerComboBox()
        {
            cbDangerLevel.DataSource = Enum.GetValues(typeof(DangerEnum));
        }

        private void LoadAnimaCheckListBox()
        {
            clbListOfAnimals.Items.Clear();
            var animals = animalManager.LoadAnimalFromDataBase();
            foreach (var animal in animals)
            {
                clbListOfAnimals.Items.Add(animal);
            }
        }

        private void RefreshListBox()
        {
            clbListOfAnimals.Items.Clear();
            // thiss will convert search text to lowercase for case-insensitive comparison
            string searchText = tbSearch.Text.ToLower();
            List<Animal> filteredAnimals = animalManager.LoadAnimalFromDataBase().Where(aml => aml.Name.ToLower().Contains(searchText)).ToList();

            clbListOfAnimals.Items.AddRange(filteredAnimals.ToArray());
        }

        private bool ValidateLocationInput()
        {
            if (string.IsNullOrEmpty(tbNameLocationForm.Text))
            {
                tsslblLocationForm.Text = "Please input a valid location name. Validation Error";
                return false;
            }

            if (string.IsNullOrEmpty(cbSpecies_LocationForm.Text))
            {
                tsslblLocationForm.Text = "Please input valid species information. Validation Error";
                return false;

            }
            if (string.IsNullOrEmpty(tbDescriptionLocationForm.Text))
            {
                tsslblLocationForm.Text = "Please input a valid Description. Validation Error";
                return false;

            }

            int capacity;
            if (!int.TryParse(nudCapacity.Text, out capacity))
            {
                tsslblLocationForm.Text = "Look like the capacity value is below zero. Please input a valid Capacity greater than zero. Validation Error";
                return false;

            }

            if (cbDangerLevel.SelectedIndex == -1)
            {
                tsslblLocationForm.Text = "Please choose a danger level to continue. Validation Error";
                return false;
            }

            return true;
        }

        public void BtnCreateNewStatus(bool status)
        {
            btnAddLocationForm.Enabled = status;
        }

        private void LocationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Visible = true;
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = tbSearch.Text.Trim();

            List<Animal> filteredAnimal;

            try
            {

                filteredAnimal = animalManager.LoadAnimalFromDataBase()
                        .Where(anm => anm.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                      anm.Species.ToLower().Contains(searchTerm.ToLower()) ||
                                      anm.Origin.ToLower().Contains(searchTerm.ToLower())).ToList();
               
                clbListOfAnimals.DataSource = filteredAnimal;
            }
            catch (Exception ex)
            {
                tsslblLocationForm.Text = ex.Message;
            }
        }
    }
}
