﻿namespace GladiatorTycoon.Services.Models
{
    public class Slave : Person
    {
        public int? Loyalty { get; set; }
        public Master Owner { get; set; }

        public Slave()
        {

        }

        public Slave(Person person, int loyalty, Master dominus)
        {
            Skill = person.Skill;
            Charisma = person.Charisma;
            FirstName = person.FirstName;
            Gold = person.Gold;
            HomeCity = person.HomeCity;
            Id = person.Id;
            Wits = person.Wits;
            IsAlive = person.IsAlive;
            Gender = person.Gender;
            LastName = person.LastName;
            Race = person.Race;
            SocialStatus = person.SocialStatus;
            Power = person.Power;

            Loyalty = loyalty;
            Owner = dominus;
        }
    }
}
