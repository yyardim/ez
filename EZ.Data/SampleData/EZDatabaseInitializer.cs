using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EZ.Domain;
using EZ.Domain;

namespace EZ.Data.SampleData
{
    public class EzDatabaseInitializer
        //: DropCreateDatabaseAlways<EzDbContext>
        : DropCreateDatabaseIfModelChanges<EzDbContext>
    {
        private Random random = new Random();
        protected override void Seed(EzDbContext context)
        {
            var persons = AddPeople(context,10);
            var addresses = AddAddresses(context);
            var categories = AddCategories(context);            
            var ezs = AddEzs(context);
            var ezPersons = AddEzPersons(context,persons,ezs);
            var personAddresses = AddPersonAddresses(context, persons, addresses);
        }

        private List<PersonAddress> AddPersonAddresses(EzDbContext context, List<Person> persons, List<Address> addresses)
        {
            var personAddresses = new List<PersonAddress>();
            var addressCnt = addresses.Count;

            foreach(var p in persons)
            {
                var personAddress = new PersonAddress
                {
                    IsDefault = true,
                    PersonId = p.PersonId,
                    AddressId = addresses.ElementAt(random.Next(0, addressCnt - 1)).AddressId
                };
                personAddresses.Add(personAddress);
            };

            personAddresses.ForEach(pa => context.PersonAddresses.Add(pa));
            context.SaveChanges();
            return personAddresses;
        }

        private List<EzPerson> AddEzPersons(EzDbContext context, List<Person> persons, List<Ez> ezs)
        {
            var ezCnt = ezs.Count;

            var ezPersons = persons.Select(p => new EzPerson
            {
                Guest = 3, 
                IsGoing = true, 
                HasJoined = false, 
                IsInvited = true, 
                PersonId = p.PersonId, 
                EzId = ezs.ElementAt(random.Next(0, ezCnt - 1)).EzId
            }).ToList();

            ezPersons.ForEach(ez => context.EzPersons.Add(ez));
            context.SaveChanges();
            return ezPersons;
        }

        private Int32 RandomNumber(int min, int max)
        {
            var random1 = new Random();
            return random1.Next(min, max);
        }

        private List<Address> AddAddresses(EzDbContext context)
        {
            var addresses = new List<Address>();
            
            var homeAddress1 = new Address
                {
                    AddressLine1 = "801 Brighton Point",
                    AddressType = Enums.AddressType.House,
                    City = "Atlanta",
                    CountryCode = "US",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    PostalCode = "30328",
                    StateOrProvinceCode = "GA"
                };
            addresses.Add(homeAddress1);

            var homeAddress2 = new Address
            {
                AddressLine1 = "440 University Street",
                AddressType = Enums.AddressType.House,
                City = "Hammond",
                CountryCode = "US",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                PostalCode = "70401",
                StateOrProvinceCode = "LA"
            };
            addresses.Add(homeAddress2);

            var workAddress = new Address
            {
                AddressLine1 = "3550 Engineering Dr",
                AddressType = Enums.AddressType.Work,
                City = "Atlanta",
                CountryCode = "US",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                PostalCode = "30309",
                StateOrProvinceCode = "GA"
            };
            addresses.Add(workAddress);
            
            var gaTechAddress = new Address
                {
                    AddressLine1 = "ga tect street",
                    AddressType = Enums.AddressType.Other,
                    City = "Atlanta",
                    CountryCode = "US",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    PostalCode = "30309",
                    StateOrProvinceCode = "GA"
                };
            addresses.Add(gaTechAddress);

            addresses.ForEach(a => context.Addresses.Add(a));
            context.SaveChanges();

            return addresses;
        }

        private List<Category> AddCategories(EzDbContext context)
        {
            var CatTextGenerator = new SampleTextGenerator();
            const SampleTextGenerator.SourceNames CatTextSource = SampleTextGenerator.SourceNames.Decameron;

            var categories = new List<Category>();
            var categoriesFlat = new List<Category>();
            
            //--- I ----
            var mainCat = new Category
            {
                Name = "ez",
                Description = CatTextGenerator.GenSentences(5, CatTextSource),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            //--- II ----
            var sportCat = new Category
            {
                Name = "Sports",
                Description = CatTextGenerator.GenSentences(5, CatTextSource),
                ParentCategories = new List<Category> { mainCat },
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            var outdoorCat = new Category
            {
                Name = "Outdoors",
                Description = CatTextGenerator.GenSentences(5, CatTextSource),
                ParentCategories = new List<Category> { mainCat, sportCat },
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            //--- III ----
            var fitnessCat = new Category
            {
                Name = "Fitness",
                Description = CatTextGenerator.GenSentences(5, CatTextSource),
                ParentCategories = new List<Category> { sportCat },
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            //--- IV ---
            var soccerCat = new Category
            {
                Name = "Soccer",
                Description = CatTextGenerator.GenSentences(5, CatTextSource),
                ParentCategories = new List<Category> { outdoorCat, sportCat },
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            outdoorCat.SubCategories.Add(soccerCat);

            sportCat.SubCategories.Add(soccerCat);
            sportCat.SubCategories.Add(outdoorCat);
            sportCat.SubCategories.Add(fitnessCat);

            mainCat.SubCategories.Add(sportCat);
            mainCat.SubCategories.Add(outdoorCat);
            
            categories.Add(mainCat);
            categoriesFlat = categories
                .Where(cat => cat.Name == "ez")
                .Flatten(cat => cat.SubCategories)
                .ToList();

            categoriesFlat.ForEach(ec => context.Categories.Add(ec));

            context.SaveChanges();

            return categories;
        }

        private List<Ez> AddEzs(EzDbContext context)
        {
            var ezTextGenerator = new SampleTextGenerator();
            const SampleTextGenerator.SourceNames ezTextSource = SampleTextGenerator.SourceNames.Decameron;
            var CatTextGenerator = new SampleTextGenerator();
            const SampleTextGenerator.SourceNames CatTextSource = SampleTextGenerator.SourceNames.Decameron;

            var ezs = new List<Ez>();
            var categories = new List<Category>();
            var categoriesFlat = context.Categories
                .Where(cat => cat.CategoryId == 1)
                .Flatten(cat => cat.SubCategories)
                .ToList();

            var sportCat = categoriesFlat.FirstOrDefault(t => t.Name == "Sports");
            var outdoorCat = categoriesFlat.FirstOrDefault(t => t.Name == "Outdoors");
            var fitnessCat = categoriesFlat.FirstOrDefault(t => t.Name == "Fitness");
            var soccerCat = categoriesFlat.FirstOrDefault(t => t.Name == "Soccer");
            
            categories = new List<Category> {soccerCat};
            var soccerEz = new Ez
            {
                Name = "Play Soccer at GA Tech this weekend!!!",
                Categories = categories,
                Description = ezTextGenerator.GenSentences(5, ezTextSource),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                AddressId = context.Addresses.FirstOrDefault(a => a.AddressLine1 == "ga tect street").AddressId,
                IsActive = true,
                IsCheckInRequired = true,
                IsPublic = true,
                MaxGuests = 10,
                DateTime = DateTime.Now
            };
            ezs.Add(soccerEz);

            categories = new List<Category> {outdoorCat};
            var outdoorEz = new Ez
            {
                Name = "Let's get out this weekend! To the mountains!!!",
                Categories = categories,
                Description = ezTextGenerator.GenSentences(5, ezTextSource),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                AddressId = context.Addresses.FirstOrDefault(a => a.AddressLine1 == "ga tect street").AddressId,
                IsActive = true,
                IsCheckInRequired = false,
                IsPublic = true,
                MaxGuests = 1000,
                DateTime = DateTime.Now
            };
            ezs.Add(outdoorEz);

            categories = new List<Category> { fitnessCat };
            var indoorEz = new Ez
            {
                Name = "I am too goofy.. Join me!!",
                Categories = categories,
                Description = ezTextGenerator.GenSentences(5, ezTextSource),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                AddressId = context.Addresses.FirstOrDefault(a => a.AddressLine1 == "801 Brighton Point").AddressId,
                IsActive = true,
                IsCheckInRequired = false,
                IsPublic = true,
                MaxGuests = 5,
                DateTime = DateTime.Now
            };
            ezs.Add(indoorEz);

            ezs.ForEach(e => context.Ezs.Add(e));
            context.SaveChanges();
            return ezs;
        }

        private List<Person> AddPeople(EzDbContext context, int count)
        {
            var persons = new List<Person>();
            AddKnownPeople(persons, context);
            AddPeople(count, persons);
            persons.ForEach(p => context.Persons.Add(p));
            context.SaveChanges();
            return persons;
        }
        private void AddKnownPeople(List<Person> persons, EzDbContext context)
        {
            var bioTextGenerator = new SampleTextGenerator();
            const SampleTextGenerator.SourceNames bioTextSource = SampleTextGenerator.SourceNames.ChildHarold;

            persons.Add(new Person
            {
                FirstName = "Yener",
                LastName = "Yardim",
                Email = "yyardim@contoso.com",
                Gender = "M",
                Biography = bioTextGenerator.GenSentences(12, bioTextSource),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                RememberMe = true
            });
            persons.Add(new Person
            {
                FirstName = "Cenk",
                LastName = "Ursavas",
                Email = "cengo@contoso.com",
                Gender = "M",
                Biography = bioTextGenerator.GenSentences(20, bioTextSource),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                RememberMe = false,
            });
        }
        private void AddPeople(int count, List<Person> persons)
        {
            var enumerator = PeopleNames.RandomNameEnumerator();
            const string netNameFmt = "{0}.{1}{2}";
            var netNameCounter = 1;
            var bioTextGenerator = new SampleTextGenerator();
            const SampleTextGenerator.SourceNames bioTextSource = SampleTextGenerator.SourceNames.TheRaven;

            while (count-- >0)
            {
                enumerator.MoveNext();
                var name = enumerator.Current;

                var netName = string.Format(netNameFmt, name.First, name.Last, netNameCounter++);
                var item =
                    new Person
                    {
                        FirstName = name.First,
                        LastName = name.Last,
                        Email = netName + "@contoso.com",
                        Biography = bioTextGenerator.GenSentences(8, bioTextSource),
                        Gender = name.Gender,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        RememberMe = true
                    };
                persons.Add(item);
            }
        }
    }
}
