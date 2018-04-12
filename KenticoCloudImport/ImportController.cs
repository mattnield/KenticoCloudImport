using AngleSharp.Parser.Html;
using KenticoCloud.ContentManagement;
using KenticoCloud.ContentManagement.Models.Assets;
using KenticoCloud.ContentManagement.Models.Items;
using KenticoCloud.ContentManagement.Models.StronglyTyped;
using KenticoCloudImport.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KenticoCloudImport
{
    public class ImportController
    {

        private readonly ContentManagementClient _managementClient;
        //private readonly DeliveryClient _deliveryClient;

        public async Task<string> CreateAssetAsync()
        {
            // Defines the description of an asset
            var assetDescription = new AssetDescription
            {
                Description = "Description of the asset in the default Language",
                Language = LanguageIdentifier.DEFAULT_LANGUAGE
            };

            IEnumerable<AssetDescription> descriptions = new [] { assetDescription };

            const string filePath = @"C:\Users\mattn\projects\KenticoCloudImport\KenticoCloudImport\Images\KenticoCloudLogo.jpg";
            const string contentType = "image/jpg";

            // Uploads the file and links it to a new asset
            var response = await _managementClient.CreateAssetAsync(new FileContentSource(filePath, contentType), descriptions);
            return response.ExternalId;
        }

        /// <summary>
        /// Creates a basic content item with the supplied content name
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public async Task<string> CreateItemAsync(string documentName)
        {
            var item = new ContentItemCreateModel
            {
                Name = documentName,
                Type = ContentTypeIdentifier.ByCodename("simple_page")
            };

            var itemResult = await _managementClient.CreateContentItemAsync(item);
            return itemResult.CodeName;
        }

        /// <summary>
        /// Adds language variant content to the content item with the supplied codename
        /// </summary>
        /// <param name="codename">Codename of the content item needed</param>
        /// <returns></returns>
        public async Task<Guid> CreateItemVariantAsync(string codename)
        {
            const string htmlMarkup = @"<h1>Some content</h1>
                        <p>This is the content</p>";

            if (!IsValidHtml(htmlMarkup)) { return Guid.Empty; }

            var content = new SimplePage
            {
                PageTitle = "Test import",
                PageContent = htmlMarkup,
                DishColour = new[]
                {
                    TaxonomyTermIdentifier.ByCodename("green") 
                },
                PageTeaser = new AssetIdentifier[0] 
            };

            // Specifies the content item and the language variant
            ContentItemIdentifier itemIdentifier = ContentItemIdentifier.ByCodename(codename);
            LanguageIdentifier languageIdentifier = LanguageIdentifier.DEFAULT_LANGUAGE;
            ContentItemVariantIdentifier identifier = new ContentItemVariantIdentifier(itemIdentifier: itemIdentifier, languageIdentifier: languageIdentifier);

            // Upserts a language variant of your content item
            try
            {
                ContentItemVariantModel<SimplePage> response = await _managementClient.UpsertContentItemVariantAsync<SimplePage>(identifier, content);
                return response.Item.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return Guid.Empty;
            }
        }

        public ImportController(string projectId, string apiKey)
        {
            _managementClient = new ContentManagementClient(new ContentManagementOptions { ProjectId = projectId, ApiKey = apiKey });
        }

        /// <summary>
        /// Checks that the supplied HTML is valid.
        /// </summary>
        /// <param name="html">HTML markup to be validated.</param>
        /// <returns>Boolean indicating whether or not the supplied HTML was valid.</returns>
        private static bool IsValidHtml(string html)
        {
            var parser = new HtmlParser(new HtmlParserOptions { IsStrictMode = false });
            try
            {
                var doc = parser.Parse(html);

                return true;
            }
            catch (HtmlParseException)
            {
                return false;
            }
        }
    }
}