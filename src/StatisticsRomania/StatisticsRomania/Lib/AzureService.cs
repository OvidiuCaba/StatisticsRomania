using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using StatisticsRomania.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class AzureService
    {
        private static DateTime _lastSync = DateTime.Now.AddMinutes(-61);

        private static MobileServiceClient _client;

        private static bool _isSyncing = false;

        public static IMobileServiceSyncTable<Data> Table { get; set; }

        public static async Task Initialize()
        {
            if (_client?.SyncContext?.IsInitialized ?? false)
            {
                return;
            }

            var handler = new AuthHandler();
            var azureUrl = "http://statistics-romania.azurewebsites.net";

            _client = new MobileServiceClient(azureUrl, handler);
            handler.Client = _client;

            var path = "data.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<Data>();

            await _client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            Table = _client.GetSyncTable<Data>();
        }

        public static async Task SyncData()
        {
            try
            {
                // If it's syncing, I need to wait so I make sure that after executing this method I have something up to date to load on UI; if I don't wait, I might quit too soon and have nothing to load from cache
                while(_isSyncing)
                {
                    await Task.Delay(1000);
                }

                if (!CrossConnectivity.Current.IsConnected)
                    return;

                if ((DateTime.Now - _lastSync).TotalMinutes < 60)
                {
                    return;
                }

                _isSyncing = true;
#if DEBUG
                await _client.SyncContext.PushAsync();
#endif
                await Table.PullAsync("allData", Table.CreateQuery());
                _lastSync = DateTime.Now;
            }
            catch (MobileServicePushFailedException exc)
            {
                try
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
                catch
                {
                    // there's nothing I can do here
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync, use offline capabilities: " + ex);
            }

            _isSyncing = false;
        }

        public static async Task Insert(List<Data> data)
        {
            await Initialize();

            await Task.WhenAll(data.Select(x => Table.InsertAsync(x)));

            await SyncData();
        }

        public static async Task<string> GetAzureAccessToken()
        {
            var tokenCache = new TokenCache();
            AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/" + $"{AppResource.AzureTenantId}", tokenCache);
            ClientCredential clientCredential = new ClientCredential(AppResource.AzureClientId, AppResource.AzureClientSecret);
            var result = await authContext.AcquireTokenAsync(AppResource.AzureApiEndtpointUri, clientCredential);
            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");
            return result.AccessToken;
        }
    }

    internal class AuthHandler : DelegatingHandler
    {
        public IMobileServiceClient Client { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("Make sure to set the 'Client' property in this handler before using it.");
            }

            // Cloning the request, in case we need to send it again
            var clonedRequest = await CloneRequest(request);
            var response = await base.SendAsync(clonedRequest, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    // Clone the request
                    clonedRequest = await CloneRequest(request);

                    var token = await AzureService.GetAzureAccessToken();

                    clonedRequest.Headers.Remove("X-ZUMO-AUTH");
                    clonedRequest.Headers.Add("X-ZUMO-AUTH", token);

                    response = await base.SendAsync(clonedRequest, cancellationToken);
                }
                catch (InvalidOperationException)
                {
                    // user cancelled auth, so let’s return the original response
                    return response;
                }
            }

            return response;
        }

        private async Task<HttpRequestMessage> CloneRequest(HttpRequestMessage request)
        {
            var result = new HttpRequestMessage(request.Method, request.RequestUri);
            foreach (var header in request.Headers)
            {
                result.Headers.Add(header.Key, header.Value);
            }

            if (request.Content != null && request.Content.Headers.ContentType != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var mediaType = request.Content.Headers.ContentType.MediaType;
                result.Content = new StringContent(requestBody, Encoding.UTF8, mediaType);
                foreach (var header in request.Content.Headers)
                {
                    if (!header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Content.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            return result;
        }
    }
}