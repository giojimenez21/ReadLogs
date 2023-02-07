using Aspose.Cells;
using ConsoleApp1;
using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        ArrayList petitionsWithStatus200 = new ArrayList();
        ArrayList petitionsWithStatus400 = new ArrayList();
        ArrayList petitionsWithStatus500 = new ArrayList();
        StatusCodes statusCodes = new StatusCodes();
        //string pathFile = "D:/Escritorio/proyectos/Ejemplo.log";
        Console.WriteLine("Escriba la ruta absoluta del archivo log:");
        string pathFile = Console.ReadLine()!;

        Workbook workbook = new Workbook();
        workbook.Worksheets.Add();
        workbook.Worksheets.Add();
        workbook.Worksheets[0].Name = "Status 200";
        workbook.Worksheets[1].Name = "Status 400";
        workbook.Worksheets[2].Name = "Status 500";


        using (FileStream fs = File.Open(pathFile!, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (BufferedStream bs = new BufferedStream(fs))
        using (StreamReader sr = new StreamReader(bs))
        {
            string line;
            while ((line = sr.ReadLine()!) != null)
            {
                if(Array.Exists(statusCodes.statusCodes200, statusCode => line.Contains(statusCode)))
                {
                    petitionsWithStatus200.Add(line.Substring(0, 8) + " " + line.Split("INFO")[1]);
                    Console.WriteLine(line.Substring(0, 8) + " " + line.Split("INFO")[1]);
                }

                if (Array.Exists(statusCodes.statusCodes400, statusCode => line.Contains(statusCode)))
                {
                    petitionsWithStatus400.Add(line.Substring(0, 8) + " " + line.Split("INFO")[1]);
                    Console.WriteLine(line.Substring(0, 8) + " " + line.Split("INFO")[1]);
                }

                if (Array.Exists(statusCodes.statusCodes500, statusCode => line.Contains(statusCode)))
                {
                    petitionsWithStatus500.Add(line.Substring(0, 8) + " " + line.Split("INFO")[1]);
                    Console.WriteLine(line.Substring(0, 8) + " " + line.Split("INFO")[1]);
                }

            }
        }

        workbook.Worksheets[0].Cells.ImportArrayList(petitionsWithStatus200, 0, 0, true);
        workbook.Worksheets[1].Cells.ImportArrayList(petitionsWithStatus400, 0, 0, true);
        workbook.Worksheets[2].Cells.ImportArrayList(petitionsWithStatus500, 0, 0, true);

        workbook.Save("data.xlsx");
    }
}