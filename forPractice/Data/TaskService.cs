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


        public async Task UpdateStatusAsync(TaskModel taskModel)
        {
            var existingTask = await _dbContext.taskModels.FindAsync(taskModel.Id);

            if (existingTask != null)
            {
                existingTask.Status = taskModel.Status;
              

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Task not found");
            }
        }


        public async Task<bool> DeleteTaskByIdAsync(int TaskId)
        {
            var taskToDelete = await _dbContext.taskModels.FindAsync(TaskId);

            if (taskToDelete != null)
            {
                _dbContext.taskModels.Remove(taskToDelete);
                await _dbContext.SaveChangesAsync();
               
                return true;
            }

            return false;
        }

    }
}
