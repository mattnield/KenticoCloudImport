using System;
using System.Runtime.CompilerServices;
using KenticoCloud.ContentManagement;
using KenticoCloud.ContentManagement.Models.Items;

namespace KenticoCloudImport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var clientOptions = new ContentManagementOptions { 
                ProjectId="[PROJECT ID]", 
                ApiKey="[API KEY]" 
            };

            var client = new ContentManagementClient(clientOptions);

            var tCreateItem = CreateStub(client);
            var createItem = tCreateItem.Result;


        }

        private static System.Threading.Tasks.Task<ContentItemModel> CreateStub(ContentManagementClient client){
            var item = new ContentItemCreateModel
            {
                Name = "Imported item",
                Type = ContentTypeIdentifier.ByCodename("dish"),
                ExternalId = "myDish"
            };

            return client.CreateContentItemAsync(item);
        }
    }
}
