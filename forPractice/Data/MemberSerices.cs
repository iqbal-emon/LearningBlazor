using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace forPractice.Data
{
    public class MemberServices
    {
        private readonly IHubContext<Employeehub> _hubContext;
        private readonly addDbContex _dbContext;
        private readonly SqlTableDependency<MemberModel> _dependency;

        public MemberServices(addDbContex dbContext, IHubContext<Employeehub> hubContext)
        {
            _hubContext = hubContext;
            _dbContext = dbContext;
            _dependency = new SqlTableDependency<MemberModel>(dbContext.Database.GetDbConnection().ConnectionString, "TaskModels");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<MemberModel> e)
        {
            await SendUpdateToClients();
        }

        private async Task SendUpdateToClients()
        {
            var employees = await GetMembersAsync();
            await _hubContext.Clients.All.SendAsync("RefreshEmployees", employees);
        }

        public async Task<List<MemberModel>> GetMembersAsync()
        {
            return await _dbContext.memberModels.AsNoTracking().ToListAsync();
        }

        public async Task<MemberModel> GetMemberByIdAsync(int memberId)
        {
            return await _dbContext.memberModels.FirstOrDefaultAsync(m => m.Id == memberId);
        }

        public async Task AddMemberAsync(MemberModel newMember)
        {
            _dbContext.memberModels.Add(newMember);
            await _dbContext.SaveChangesAsync();
            await SendUpdateToClients();
        }

        public async Task UpdateMemberAsync(MemberModel updatedMember)
        {
            var existingMember = await _dbContext.memberModels.FindAsync(updatedMember.Id);

            if (existingMember != null)
            {
                // Update the properties of the existing member with the new values
                existingMember.Name = updatedMember.Name;
                existingMember.Age = updatedMember.Age;
                existingMember.Email = updatedMember.Email;
                existingMember.JoiningDate = updatedMember.JoiningDate;
                existingMember.Password = updatedMember.Password;

                // Save the changes to the database
                await _dbContext.SaveChangesAsync();
                await SendUpdateToClients();
            }
            else
            {
                throw new InvalidOperationException("Member not found");
            }
        }

        public async Task<bool> DeleteMemberByIdAsync(int memberId)
        {
            var memberToDelete = await _dbContext.memberModels.FindAsync(memberId);

            if (memberToDelete != null)
            {
                _dbContext.memberModels.Remove(memberToDelete);
                await _dbContext.SaveChangesAsync();
                await SendUpdateToClients();
                return true;
            }

            return false;
        }

        public MemberModel? GetByUserName(string userName)
        {
            return _dbContext.memberModels.FirstOrDefault(x => x.Name == userName);
        }
    }
}
