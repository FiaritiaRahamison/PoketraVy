using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml; // Pour le traitement des fichiers Excel
using CsvHelper; // Pour le traitement des fichiers CSV
using System.Globalization;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;

namespace PoketraVy_backoffice.Pages.Utilisateurs
{
    public class ImportModel : PageModel
    {
        private readonly PoketraVy_backofficeContext _context;

        public ImportModel(PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                ErrorMessage = "Veuillez sélectionner un fichier.";
                return Page();
            }

            try
            {
                var fileExtension = Path.GetExtension(UploadedFile.FileName).ToLower();
                if (fileExtension == ".csv")
                {
                    await ProcessCsvFile(UploadedFile);
                }
                else if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    await ProcessExcelFile(UploadedFile);
                }
                else
                {
                    ErrorMessage = "Format de fichier non supporté. Veuillez télécharger un fichier CSV ou Excel.";
                    return Page();
                }

                SuccessMessage = "Les utilisateurs ont été importés avec succès.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Une erreur s'est produite lors du traitement du fichier : {ex.Message}";
            }

            return Page();
        }

        private async Task ProcessCsvFile(IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                MissingFieldFound = null // Ignore missing fields
            }))
            {
                var records = new List<Utilisateur>();

                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var utilisateur = new Utilisateur
                    {
                        Username = csv.GetField("Username"),
                        Password = null,
                        Role = csv.GetField("Role").ToLower() == "true" || csv.GetField("Role").ToLower() == "yes" || csv.GetField("Role") == "1"
                    };
                    records.Add(utilisateur);
                }

                _context.Utilisateurs.AddRange(records);
                await _context.SaveChangesAsync();
            }
        }


        private async Task ProcessExcelFile(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var roleText = worksheet.Cells[row, 3].Text.ToLower();
                        bool role = roleText == "true" || roleText == "yes" || roleText == "1";
                        var utilisateur = new Utilisateur
                        {
                            Username = worksheet.Cells[row, 1].Text,
                            Password = worksheet.Cells[row, 2].Text,
                            Role = role
                        };

                        _context.Utilisateurs.Add(utilisateur);
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

