using Microsoft.AspNetCore.SignalR;
using forPractice.Data;
namespace forPractice.Data
{
    public class Employeehub:Hub
    {
        public async Task RefreshEmployees(List<MemberModel> memberModels)
        {
            await Clients.All.SendAsync("RefreshEmployees", memberModels);
        }
    }
}
