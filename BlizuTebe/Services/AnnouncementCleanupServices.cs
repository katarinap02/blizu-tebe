using BlizuTebe.Services.Interfaces;

namespace BlizuTebe.Services
{
    public class AnnouncementCleanupServices : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AnnouncementCleanupServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var announcementService = scope.ServiceProvider.GetRequiredService<IAnnouncementService>();

                    var result = announcementService.GetAnnouncements();
                    if (result.IsSuccess)
                    {
                        var expired = result.Value
                            .Where(a => a.ExpirationDate < DateTime.UtcNow)
                            .ToList();

                        foreach (var ann in expired)
                        {
                            announcementService.DeleteById(ann.Id);
                        }

                        if (expired.Any())
                        {
                            Console.WriteLine($"Obrisano {expired.Count} zastarelih obaveštenja ({DateTime.UtcNow}).");
                        }
                    }
                }

                // ponavlja proveru svakih 24h
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

    }
}
