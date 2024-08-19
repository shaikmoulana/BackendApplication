using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly DataBaseContext _context;

        public EmployeeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.TblEmployee.ToListAsync();
        }

        public async Task<Employee> Get(string id)
        {
            return await _context.TblEmployee.FindAsync(id);
        }

        public async Task<Employee> Create(Employee employee)
        {
            _context.TblEmployee.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> Delete(string id)
        {
            var employee = await _context.TblEmployee.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.TblEmployee.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}