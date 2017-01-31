using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib.Sync
{
    public static class SyncService
    {
        private static bool _isRunning = false;
        private static ConcurrentStack<PullCommand> _syncCommands = new ConcurrentStack<PullCommand>();

        public static async Task SyncAsync()
        {
            _isRunning = true;
            PullCommand command = null;
            while(_syncCommands.TryPop(out command))
            {
                await command.ExecuteAsync();
            }
            _isRunning = false;
        }

        public static async Task PushCommand(PullCommand command)
        {
            _syncCommands.Push(command);

            if (!_isRunning)
                await SyncAsync();
        }

        public static void LoadSyncCommands()
        {
            var years = Enumerable.Range(App.FirstYearAvailableData, DateTime.Now.Year - App.FirstYearAvailableData);
            var chapterList = new[] { "ExportFob", "ImportCif", "SoldFobCif", "NumberOfEmployees", "AverageGrossSalary", "AverageNetSalary", "Unemployed", "NumberOfNights", "NumberOfTourists", };

            var commands = from y in years
                           from c in chapterList
                           orderby y
                           select new PullCommand(c, y);

            _syncCommands.PushRange(commands.ToArray());
        }
    }
}