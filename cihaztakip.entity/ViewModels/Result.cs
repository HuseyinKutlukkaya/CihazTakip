using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.entity.ViewModels
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }

        public Result()
        {
            Errors = new List<string>();
        }
    }
}
