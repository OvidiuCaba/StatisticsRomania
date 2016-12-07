using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using StatisticsRomania.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class AzureService
    {
        private static DateTime LastSync = DateTime.Now;

        private static MobileServiceClient client;

        public static IMobileServiceSyncTable<Data> Table { get; set; }

        public static async Task Initialize()
        {
            if (client?.SyncContext?.IsInitialized ?? false)
            {
                return;
            }

            var azureUrl = "http://statistics-romania.azurewebsites.net";

            client = new MobileServiceClient(azureUrl);

            var path = "data.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<Data>();

            await client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            Table = client.GetSyncTable<Data>();
        }

        public static async Task SyncData()
        {
            try
            {
                if ((DateTime.Now - LastSync).Minutes < 60)
                {
                    return;
                }

                await client.SyncContext.PushAsync();
                await Table.PullAsync("allData", Table.CreateQuery());
                LastSync = DateTime.Now;
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    var syncErrors = exc.PushResult.Errors;

                    // Simple error/conflict handling.
                    if (syncErrors != null)
                    {
                        foreach (var error in syncErrors)
                        {
                            if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                            {
                                // Update failed, revert to server's copy
                                await error.CancelAndUpdateItemAsync(error.Result);
                            }
                            else
                            {
                                // Discard local change
                                await error.CancelAndDiscardItemAsync();
                            }

                            Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync, use offline capabilities: " + ex);
            }
        }

        public static async Task Insert(List<Data> data)
        {
            await Initialize();

            await Task.WhenAll(data.Select(x => Table.InsertAsync(x)));

            await SyncData();
        }
    }
}