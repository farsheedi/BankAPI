using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class BankItem
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Asset { get; set; }
        public string AssetType { get; set; }
        public string Value { get; set; }
    }
}
