// See https://aka.ms/new-console-template for more information

using ConsoleTables;
using Npgsql;

internal class Program
{
	public static void Main(string[] args)
	{
		var connectionString =
			"Server=192.168.150.2;Port=5432;Database=postgres;User Id=postgres;Password=13_ppp_11;Pooling=true;";
		
		List<Student> students = new List<Student>();
		
		using (var connection = new NpgsqlConnection(connectionString))
		{
			var query = @$"
SELECT student_name, date_of_birth
FROM students 
WHERE study_group = 'БСБО-04-20'";

			var command = new NpgsqlCommand(query, connection);

			try
			{
				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					students.Add(new Student(){
						student_name = reader["student_name"].ToString(),
						date_of_birth = DateOnly.Parse(reader["date_of_birth"].ToString())
					});
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			
			var table = new ConsoleTable("student_name", "date_of_birth");

			foreach (var student in students)
			{
				table.AddRow(student.student_name, student.date_of_birth);
			}

			table.Write(Format.Alternative);
			Console.WriteLine();
		}
	}

	public class Student
	{
		public string student_name { get; set; }	
		public DateOnly date_of_birth { get; set; }
	}
}