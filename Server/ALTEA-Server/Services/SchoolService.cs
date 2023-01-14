﻿using ALTEA_Server.Data;
using ALTEA_Server.Models;

namespace ALTEA_Server.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly DataContext _dataContext;

        public SchoolService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SaveSchool(School school)
        {
            var principal = _dataContext.Users.FirstOrDefault(user => user.Id == school.Principal.Id)!;
            if (principal is not null) {
                school.Principal = (User)principal;
            }
            else
            {
                school.Principal = null;
            }
            _dataContext.Schools.Add(school);
            _dataContext.SaveChanges();

        }

        public void SaveSchools(List<School> schools)
        {

            schools.ForEach(school =>
            {
                _dataContext.Schools.Add(school);
                _dataContext.SaveChanges();
            });

        }

        public async Task<School> GetSchoolByID(int id)
        {

            var school = _dataContext.Schools.FirstOrDefaultAsync(s => s.Id == id);
            if (school is not null)
                return await school;
            else
                throw new Exception($"School with ID '{id}' not found.");
        }

        public async Task<List<School>> GetAllSchools()
        {
            return await _dataContext.Schools.ToListAsync();
        }

        public void DeleteSchool(School school)
        {
            var removeSchool = _dataContext.Schools.FirstOrDefault(d => d.Id == school.Id);
            if (removeSchool is not null)
                _dataContext.Remove(removeSchool);
                _dataContext.SaveChanges();
        }

        public void UpdateSchool(School school)
        {
            var updateSchool = _dataContext.Schools.FirstOrDefault(d => d.Id == school.Id)!;
            updateSchool.Name = school.Name;
            updateSchool.PhoneNumber = school.PhoneNumber;
            updateSchool.Principal = school.Principal;
            updateSchool.EmailAddress = school.EmailAddress;
            updateSchool.Address = school.Address;
            _dataContext.SaveChanges();
        }
    }
}
