using System.Threading.Tasks;

namespace MyProgram.Services
{
    public class WaitService : IWaitService
    {
        public async Task Wait()
        {
            await Task.Delay(3000);
        }
    }
}