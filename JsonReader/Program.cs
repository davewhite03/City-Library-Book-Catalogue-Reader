using Newtonsoft.Json.Linq;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        FileEnumeratorWrapper fileEnumeratorWrapper = new FileEnumeratorWrapper();

        // Loop allows continuous running of the program
        while (true)
        {
            Console.WriteLine("Which City library would you like to access? Type 'exit' to leave the program");

            // Prints out all library file names for user selection
            fileEnumeratorWrapper.PrintAllFileNames();
            string fileName = Console.ReadLine();

            // Retrieves the file path for the user's selected file
            string currentDirectoy = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            string filePath = Path.Combine(currentDirectoy, "Data", $"{fileName}.json");

            // Checks if the user chose to exit the program
            if (fileName.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Thank you, Goodbye!");
                break;
            }
            //Checks if response is empty
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Invalid input. Please enter a valid file name.");
                continue;
            }

            // Checks if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No file found");
                continue;
            }

            // JsonParserWrapper prints all books from the chosen library
            JsonParserWrapper jsonParserWrapper = new();
            jsonParserWrapper.ParseAndPrintBooks(File.ReadAllText(filePath));
        }
    }
}

class Book
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    // Overrides ToString for easy display
    public override string ToString()
    {
        return $"Title: {Title}\nAuthor: {Author}\nDescription: {Description}\nYear: {Year}";
    }
}

class FileEnumeratorWrapper
{
    // Prints all file names in the Data directory
    public void PrintAllFileNames()
    {
        // Retrieves the path of the Data directory
        string currentDirectoy = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        string dataDirectoryPath = Path.Combine(currentDirectoy, "Data");

        try
        {
            IEnumerable<string> files = Directory.EnumerateFiles(dataDirectoryPath);
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine($"- {fileName}");
            }
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine($"Directory not found: {e.Message}");
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Access denied to directory: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }
    }
}

class JsonParserWrapper
{
    // Parses and prints book details from a JSON string
    public void ParseAndPrintBooks(string json)
    {
        try
        {
            // Parses the JSON string to extract the array of books
            JObject jsonObject = JObject.Parse(json);
            JToken jsonArray = jsonObject["books"];

            if (jsonArray != null)
            {
                // Converts the JSON array to a list of Book objects and prints each
                var books = jsonArray.ToObject<List<Book>>();
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            else
            {
                Console.WriteLine("No books found in array");
            }
        }
        // Handles errors in JSON parsing
        catch (JsonException e)
        {
            Console.WriteLine($"JSON parsing error: {e.Message}");
        }
        // Handles other unexpected errors
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }
}
