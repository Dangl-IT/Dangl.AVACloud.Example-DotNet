using Dangl.AVACloud.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dangl.AVACloud.Example
{
    static class Program
    {
        // IMPORTANT
        // You need to enter your own values here for the conversion
        // If you don't have a client yet, you need to register as a developer and create one
        // You can find a step by step guide how to do this here:
        // https://docs.dangl-it.com/Projects/AVACloud/latest/howto/registration/developer_signup.html
        private static readonly string clientId = "";

        private static readonly string clientSecret = "";

        private static async Task Main(string[] args)
        {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                Console.WriteLine($"Please set the {nameof(clientId)} and {nameof(clientSecret)} variables in the source code in Program.cs to be able to run the demo.");
                Console.WriteLine("Conversion finished. Press any key to exit...");
                Console.ReadKey();
            }

            var clientFactory = GetAvaCloudClientFactory();

            await ConvertGaebToProjectAndPrintPositionsAsync(clientFactory.GaebConversionClient);
            await ConvertGaebToExcelAsync(clientFactory.GaebConversionClient);

            Console.WriteLine("Conversion finished. Press any key to exit...");
            Console.ReadKey();
        }

        private static AvaCloudClientFactory GetAvaCloudClientFactory()
        {
            // We need to supply our own values for clientId and clientSecret here.
            // You can optionally set other properties on the avaCloudConfig after it
            // was created, e.g. if you want to connect to an on premises installation
            // of AVACloud or want to set a different HttpHandler
            var avaCloudConfig = new AvaCloudConfiguration(clientId, clientSecret);
            return new AvaCloudClientFactory(avaCloudConfig);
        }

        private static async Task ConvertGaebToProjectAndPrintPositionsAsync(GaebConversionClient gaebConversionClient)
        {
            Console.WriteLine("Converting a GAEB file via AVACloud and printing all positions to the console.");
            using var gaebFileStream = System.IO.File.OpenRead("GAEBXML_EN.X86");
            var fileParameter = new FileParameter(gaebFileStream);
            // We're sending the GAEB file to AVACloud and get a ProjectDto back
            var avaProject = await gaebConversionClient.ConvertToAvaAsync(fileParameter,
                supportSkippedItemNumberLevelsInPositions: false,
                removePlainTextLongTexts: false,
                removeHtmlLongTexts: false);

            // Here, we just recursively get a flat list of all positions in the project.
            // GAEB files (or ServiceSpecifications) have a hierarchical structure.
            var positions = GetAllPositionsRecursively(avaProject.ServiceSpecifications.Single().Elements);

            foreach (var position in positions)
            {
                Console.WriteLine($"{position.ItemNumber.StringRepresentation} - {position.ShortText}");
            }
        }

        private static async Task ConvertGaebToExcelAsync(GaebConversionClient gaebConversionClient)
        {
            Console.WriteLine("Converting a GAEB file to Excel and saving it to disk.");
            using var gaebFileStream = System.IO.File.OpenRead("GAEBXML_EN.X86");
            var fileParameter = new FileParameter(gaebFileStream);
            // You can use "de" for the conversionCulture to produce German Excel files
            var excelResult = await gaebConversionClient.ConvertToExcelAsync(fileParameter,
                supportSkippedItemNumberLevelsInPositions: false,
                writePrices: true,
                writeLongTexts: true,
                conversionCulture: "en");
            using var excelDiskStream = System.IO.File.Create("ExcelConversion.xlsx");
            await excelResult.Stream.CopyToAsync(excelDiskStream);
            var fullSavePath = System.IO.Path.GetFullPath("ExcelConversion.xlsx");
            Console.WriteLine($"Converted GAEB file to Excel and saved it to {fullSavePath}.");
        }

        private static IEnumerable<PositionDto> GetAllPositionsRecursively(IList<IElementDto> elementsList)
        {
            if (elementsList?.Count > 0)
            {
                foreach (var element in elementsList)
                {
                    switch (element)
                    {
                        case PositionDto position:
                            yield return position;
                            break;

                        case ServiceSpecificationGroupDto group:
                            foreach (var subPosition in GetAllPositionsRecursively(group.Elements))
                            {
                                yield return subPosition;
                            }
                            break;
                    }
                }
            }
        }
    }
}
