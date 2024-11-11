using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CompareHistory
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ACoinId { get; set; }
        public int BCoinId { get; set; }
        public DateTime DateOfComaring { get; set; }
    }
}
