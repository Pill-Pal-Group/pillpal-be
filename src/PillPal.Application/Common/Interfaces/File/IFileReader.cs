using System.Data;

namespace PillPal.Application.Common.Interfaces.File;

public interface IFileReader
{
    DataTable ReadFile(Stream stream);
}
