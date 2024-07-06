using ExcelDataReader;
using PillPal.Application.Common.Interfaces.File;
using System.Data;

namespace PillPal.Infrastructure.File;

public class FileReader : IFileReader
{
    public DataTable ReadFile(Stream stream)
    {
        var configuration = new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true
            }
        };

        using var reader = ExcelReaderFactory.CreateReader(stream);

        var result = reader.AsDataSet(configuration);

        return result.Tables[0];
    }
}
