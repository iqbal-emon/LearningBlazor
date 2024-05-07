using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace forPractice.Data
{
    public class TaskService
    {

        private readonly addDbContex _dbContext;

        public TaskService(addDbContex dbContext)
        {
            _dbContext = dbContext;
        }

   public async Task<List<TaskModel>> GetAllTaskAsync()
   {
       return await _dbContext.taskModels.Include(task => task.Member).ToListAsync();
    }
        public async Task AddTask(TaskModel task)
        {
            _dbContext.taskModels.Add(task);

            await _dbContext.SaveChangesAsync();
        }
        

      public async Task<List<TaskModel>> GetMemberTask(string id)
    {
            var convertedId = Int32.Parse(id);
        // Assuming taskModels is a DbSet<TaskModel> in your DbContext
        var tasks = await _dbContext.taskModels
                                    .Where(task => task.MemberId == convertedId) // Assuming there's a MemberId property in your TaskModel
                                    .ToListAsync();

        return tasks;
    }




    }
}
