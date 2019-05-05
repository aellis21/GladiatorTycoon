﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GladiatorTycoon.Enums;
using System.Linq;
using GladiatorTycoon.Services.Models;
using GladiatorTycoon.Repositories.Repositories;
using GladiatorTycoon.DataContext;
using GladiatorTycoon.Services.Services;
using GladiatorTycoon.Repositories.Interfaces;

namespace GladiatorTycoon
{
    public partial class PersonForm : Form
    {
        GladiatorTycoonDataContext _context;
        private PersonService _personService;
        private RaceService _raceService;
        private CityService _cityService;
        private List<Person> _persons;
        private List<Race> _races;
        private List<City> _cities;

        public PersonForm(GladiatorTycoonDataContext context, IPersonRepository personRepo, IRaceRepository raceRepo, ICityRepository cityRepo)
        {
            _context = context;
            _personService = new PersonService(personRepo);
            _raceService = new RaceService(raceRepo);
            _cityService = new CityService(cityRepo);
            InitializeComponent();
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            LoadFromDatabase();

            ReloadUiData();
        }

        private void ReloadUiData()
        {
            ResetRaces();
            ResetStatus();
            ResetPeople();
            ResetCities();
            //listBoxOwners.Items.Clear();
            //foreach (var owner in _persons.Where(p => p.SocialStatus != SocialStatus.Slave))
            //{
            //    listBoxOwners.Items.Add(owner.FullName());
            //}
        }

        private void ResetStatus()
        {
            comboStatus.Items.Clear();
            foreach (var status in (SocialStatus[])Enum.GetValues(typeof(SocialStatus)))
            {
                comboStatus.Items.Add(status);
            }
        }

        private void ResetCities()
        {
            comboCities.Items.Clear();
            foreach (var city in _cities)
            {
                comboCities.Items.Add($"{city.Name} ({city.Habitat})");
            }
        }

        private void ResetPeople()
        {
            listPeople.Items.Clear();
            foreach (var person in _persons)
            {
                listPeople.Items.Add(person.FullName());
            }
        }

        private void ResetRaces()
        {
            comboRace.Items.Clear();
            foreach (var race in _races)
            {
                comboRace.Items.Add(race.Name);
            }
        }

        private void LoadFromDatabase()
        {
            _persons = _personService.GetAll();
            _races = _raceService.GetAll();
            _cities = _cityService.GetAll();
        }

        private void listPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPersonData();
        }

        private void btnSavePeople_Click(object sender, EventArgs e)
        {
            SavePersonData();
            LoadFromDatabase();
        }

        private void btnNewPerson_Click(object sender, EventArgs e)
        {
            _persons.Add(new Person("New", "Person", SocialStatus.Lowborn, null, true));
            ReloadUiData();
        }

        private void ShowPersonData()
        {
            if (listPeople.SelectedIndex == -1) { return; }

            var person = _persons[listPeople.SelectedIndex];

            textFirstName.Text = person.FirstName;
            textLastName.Text = person.LastName;
            comboRace.SelectedIndex = person.Race?.Id != null ? person.Race.Id - 1 : 0;
            comboStatus.SelectedIndex = (int)person.SocialStatus;
            comboCities.SelectedIndex = person.HomeCity?.Id != null ? person.HomeCity.Id - 1 : 0;

            if (person.IsMale) { radioButtonMale.Checked = true; }
            else { radioButtonFemale.Checked = true; }

            numPower.Value = person.Power;
            numWit.Value = person.Wits;
            numSkill.Value = person.Skill;
            numCharisma.Value = person.Charisma;
        }

        private void SavePersonData()
        {
            var person = _persons[listPeople.SelectedIndex];

            person.FirstName = textFirstName.Text;
            person.LastName = textLastName.Text;
            person.Race = _races[comboRace.SelectedIndex];
            person.IsMale = radioButtonMale.Checked;
            person.Power = (int)numPower.Value;
            person.Wits = (int)numWit.Value;
            person.Skill = (int)numSkill.Value;
            person.Charisma = (int)numCharisma.Value;
            person.SocialStatus = (SocialStatus)comboStatus.SelectedIndex;
            person.HomeCity = _cities[comboCities.SelectedIndex];

            ReloadUiData();

            _personService.Update(person);
        }

        private void comboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboStatus.SelectedIndex == 0)
            //{
            //    groupBoxSlaves.Enabled = true;
            //}
            //else
            //{
            //    groupBoxSlaves.Enabled = false;
            //}
        }

        private void btnEditRaces_Click(object sender, EventArgs e)
        {
            var raceForm = new RaceForm(new RaceRepository(_context));
            raceForm.Show();
            ResetRaces();
        }
    }
}
