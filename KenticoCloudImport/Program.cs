using System;

namespace KenticoCloudImport
{
    class Program
    {

        static void Main(string[] args)
        {
            var controller = new ImportController(projectId: "02c61010-af4d-4ec1-a7bf-2ad08454111a", apiKey: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiIwZTAyNjNlODNmNTY0M2NlYjRlNDNjMjU3OTllYzczZSIsImlhdCI6IjE1MjEyNDEyMzQiLCJleHAiOiIxNTI5MDE3MjM0IiwicHJvamVjdF9pZCI6IjAyYzYxMDEwYWY0ZDRlYzFhN2JmMmFkMDg0NTQxMTFhIiwidmVyIjoiMi4wLjAiLCJ1aWQiOiJ1c3JfMHZPTjBVTDFBQ2ZVOXNyYjVUczQyWSIsInBlcm1pc3Npb25zIjpbInZpZXctY29udGVudCIsImNvbW1lbnQiLCJ1cGRhdGUtd29ya2Zsb3ciLCJ1cGRhdGUtY29udGVudCIsInB1Ymxpc2giLCJjb25maWd1cmUtc2l0ZW1hcCIsImNvbmZpZ3VyZS10YXhvbm9teSIsImNvbmZpZ3VyZS1jb250ZW50X3R5cGVzIiwiY29uZmlndXJlLXdpZGdldHMiLCJjb25maWd1cmUtd29ya2Zsb3ciLCJtYW5hZ2UtcHJvamVjdHMiLCJtYW5hZ2UtdXNlcnMiLCJjb25maWd1cmUtcHJldmlldy11cmwiLCJjb25maWd1cmUtY29kZW5hbWVzIiwiYWNjZXNzLWFwaS1rZXlzIiwibWFuYWdlLWFzc2V0cyIsIm1hbmFnZS1sYW5ndWFnZXMiLCJtYW5hZ2Utd2ViaG9va3MiLCJtYW5hZ2UtdHJhY2tpbmciXSwiYXVkIjoibWFuYWdlLmtlbnRpY29jbG91ZC5jb20ifQ.XFJvRQ3z5F0HXwtInlUx2KEhf7Szmy1mISuWvJV_44k");
            CreateContentItem(controller);
            //CreateAsset(controller);
            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static void CreateAsset(ImportController controller)
        {
            Console.WriteLine("Creating asset...");
            var assetId = controller.CreateAssetAsync();
            Console.WriteLine($"\tCreated asset with id '{assetId.Result}'");
        }

        private static void CreateContentItem(ImportController controller)
        {
            Console.WriteLine($"Creating document...");
            var itemCodename = controller.CreateItemAsync("Test Document").Result;
            Console.WriteLine($"\tCreated item with code '{itemCodename}'");
            var itemGuid = controller.CreateItemVariantAsync(itemCodename);
            Console.WriteLine($"\tUpdated content item {itemGuid.Result}");
        }
    }
}
