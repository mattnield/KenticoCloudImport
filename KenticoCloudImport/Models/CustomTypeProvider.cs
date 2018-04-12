using System;
using KenticoCloud.Delivery;

namespace KenticoCloudImport
{
    public class CustomTypeProvider : ICodeFirstTypeProvider
    {
        public Type GetType(string contentType)
        {
            switch (contentType)
            {
                case "dish":
                    return typeof(Dish);
                default:
                    return null;
            }
        }
    }
}