﻿using EZ.Data.Contracts;
using EZ.Domain;
using System.Data.Entity;

namespace EZ.Data
{
    public class PersonRepository : EZRepository<Person> , IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context) { }
    }
}