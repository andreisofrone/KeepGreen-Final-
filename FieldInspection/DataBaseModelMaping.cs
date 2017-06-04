using System;
using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace FieldInspection
{
    public class Inspection
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public byte[] Image { get; set; }
        public int CultureID { get; set; }
        public int AuthorID { get; set; }

        public virtual Culture Culture { get; set; }
        public virtual User User { get; set; }
    }

    public class Culture
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public System.DateTime StartDate { get; set; }
        public virtual ICollection<Inspection> Inspections { get; set; }
    }

    public partial class Dashboard
    {
        public int ID { get; set; }
        public double? WindSpeed { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? Humidity { get; set; }
        public DateTime? Date { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Inspection> Inspections { get; set; }
    }
}