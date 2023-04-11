using System.Reflection;

namespace MusicLibrary.Client.Shared;
public class TableCell
{
    public object? Value { get; set; }
}
public class TableColumn
{
    public string Name { get; set; } = string.Empty;
    public PropertyInfo PropertyInfo { get; set; } = null!;
}

public class TableRow<TItem>
{
    public TableRow(TItem originValue)
    {
        OriginValue = originValue;
    }

    public List<TableCell> Values { get; set; } = new();
    public TItem OriginValue { get; set; }
}

public class Table<TItem>
{
    public IEnumerable<TableColumn> Columns { get; set; } = new List<TableColumn>();
    public List<TableRow<TItem>> Rows { get; set; } = new();
}