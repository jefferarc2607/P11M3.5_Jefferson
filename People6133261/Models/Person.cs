using SQLite;
namespace People6133261.Models;


[Table("people")]
public class Person
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(250), Unique]
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}