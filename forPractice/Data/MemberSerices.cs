using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace forPractice.Data
{
    public class MemberServices
    {
        private readonly addDbContex _dbContext;

        public MemberServices(addDbContex dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MemberModel>> GetMembersAsync()
        {
            return await _dbContext.memberModels.ToListAsync();
        }

        public async Task<MemberModel> GetMemberByIdAsync(int memberId)
        {
            return await _dbContext.memberModels.FirstOrDefaultAsync(m => m.Id == memberId);
        }




        public async Task AddMemberAsync(MemberModel newMember)
        {
            _dbContext.memberModels.Add(newMember);
    
            await _dbContext.SaveChangesAsync();
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
