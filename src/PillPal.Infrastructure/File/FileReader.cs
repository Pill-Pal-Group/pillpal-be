using ExcelDataReader;

namespace PillPal.Infrastructure.File;

public class FileReader : IFileReader
{
    public DataTable ReadExcelFile(Stream stream, bool useHeaderRow)
    {
        var configuration = new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = useHeaderRow
            }
        };

        using var reader = ExcelReaderFactory.CreateReader(stream);

        var result = reader.AsDataSet(configuration);

        return result.Tables[0];
    }
}