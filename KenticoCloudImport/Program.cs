using System;
using System.Runtime.CompilerServices;
using KenticoCloud.ContentManagement;
using KenticoCloud.ContentManagement.Models.Assets;
using KenticoCloud.ContentManagement.Models.Items;
using KenticoCloud.ContentManagement.Models.StronglyTyped;
using AngleSharp.Parser.Html;
using KenticoCloud.Delivery;

namespace KenticoCloudImport
{
  class Program
  {

    static ContentManagementClient _managementClient;
    static DeliveryClient _deliveryClient;
    static string _testName = "test_10";
    static void Main(string[] args)
    {
      Console.WriteLine("Creating document");

      var clientOptions = new ContentManagementOptions
      {
        ProjectId = "[Project Id]",
        ApiKey = "[API Key]"
      };

      _managementClient = new ContentManagementClient(clientOptions);
      _deliveryClient = new DeliveryClient(new DeliveryOptions { ProjectId = clientOptions.ProjectId });

      var tCreateItem = CreateStub();
      var createItem = tCreateItem.Result;
      Console.WriteLine($"\tCreated {createItem.Id}");

      CreateVariant();
      Console.WriteLine("Done");
    }
    
    private static System.Threading.Tasks.Task<ContentItemModel> CreateStub()
    {
      var item = new ContentItemCreateModel
      {
        Name = _testName,
        Type = ContentTypeIdentifier.ByCodename("simple_page")
      };

      return _managementClient.CreateContentItemAsync(item);
    }

    private static bool IsValidHtml(string html)
    {
      var parser = new HtmlParser(new HtmlParserOptions { IsStrictMode = false });
      try
      {
        var doc = parser.Parse(html);

        return true;
      }
      catch (HtmlParseException parseException)
      {
        Console.WriteLine($"Computer says no: {parseException.Message}");
        return false;
      }
    }

    private static async void CreateVariant()
    {
      Console.WriteLine("Creating variant");
      var htmlMarkup = @"<h1>Some content</h1>
                        <p>This is the content</p>";

      if (!IsValidHtml(htmlMarkup)) { return; }

      var taxonmy = _deliveryClient.GetTaxonomyAsync("dish_colour");

      var content = new SimplePage
      {
        PageTitle = "Test import",
        PageContent = htmlMarkup,
        DishColour = new[] { TaxonomyTermIdentifier.ByCodename("green") },
        PageTeaser = new[] { AssetIdentifier.ByExternalId(_testName) }
      };

      // Specifies the content item and the language variant
      ContentItemIdentifier itemIdentifier = ContentItemIdentifier.ByCodename(_testName);
      LanguageIdentifier languageIdentifier = LanguageIdentifier.DEFAULT_LANGUAGE;
      ContentItemVariantIdentifier identifier = new ContentItemVariantIdentifier(itemIdentifier: itemIdentifier, languageIdentifier: languageIdentifier);

      // Upserts a language variant of your content item
      try
      {
        ContentItemVariantModel<SimplePage> response = await _managementClient.UpsertContentItemVariantAsync<SimplePage>(identifier, content);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"ERROR: {ex.Message}");
      }
    }
  }
}
