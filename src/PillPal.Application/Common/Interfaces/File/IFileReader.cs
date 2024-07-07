using System.Data;

namespace PillPal.Application.Common.Interfaces.File;

public class FileExecutionResult
{
    /// <summary>The number of rows read from the file</summary>
    public int ExcelExecutionCount { get; set; }

    /// <summary>Number of rows affected in database</summary>
    public int AffectedRows { get; set; }
}

public interface IFileReader
{
    /// <summary>
    /// Read a file stream
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="useHeaderRow">Define first row as header, default is true</param>
    /// <returns>A DataTable</returns>
    DataTable ReadExcelFile(Stream stream, bool useHeaderRow = true);
}
