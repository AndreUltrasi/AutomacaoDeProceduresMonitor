using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace AutomacaoDeProceduresMonitor.Models {
    public class ListProcedureClass {
        public IEnumerable<ProcedureClass> ProcedureClass { get; set; }
        public int Page { get; set; }
    }

    public class ProcedureClass {
        public Guid Guid { get; set;}
        public int WorkItem { get; set; }
        public int ChangesetId { get; set; }
        public string Ambiente { get; set; }
        public DateTime DataModificacao { get; set; }
        public string ChangesetStatus { get; set; }
        public string NomeArquivo { get; set; }

        public static IEnumerable<ProcedureClass> GetProcedureClass(IConfiguration _configuration) {
            var procedureClass = new List<ProcedureClass>();
            try {
                var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
                string queryFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SQLContext", "QuerySQLFile.sql");
                string queryFileString = File.ReadAllText(queryFilePath);
                using (var connectionBD = new SqlConnection(connectionString)) {
                    procedureClass = connectionBD.Query<ProcedureClass>(queryFileString).AsList();
                }
                foreach(var item in procedureClass) {
                    item.Guid = Guid.NewGuid();
                }
                var onlyProcedures = procedureClass.Where(proc => proc.NomeArquivo.ToLower().Contains("sproc"));

                return onlyProcedures;
            } catch (Exception) {
                return null;
                //Core.ColoredConsoleWrite(ConsoleColor.Gray, error.ToString());
            }       
        }
    }
}