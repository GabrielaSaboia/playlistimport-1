using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Utilities;

public class CustomIntConverter : DefaultTypeConverter
{
    public override object ConvertFromString(String text, IReaderRow row, MemberMapData mamberMapData)
    {
        if (text != "")
        {
            return int.Parse(text);
        }
        else
        {
            return 0;
        }
    }
}