using Leave_Management_System.Controllers;
namespace Leave_Management_System.BackgroundServices
{
    public class LeaveCreditBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        public LeaveCreditBackgroundService(IServiceProvider services)
        {
            _services = services;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Wait for one day
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                using (var scope = _services.CreateScope())
                {
                    var leaveBalanceController = scope.ServiceProvider.GetRequiredService<LeaveBalanceController>();
                    // Credit leave balance
                    leaveBalanceController.CreditLeaveBalance(DateTime.Now);
                }
            }
        }
    }
}
