using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLink.Core.Contracts
{
    internal interface IApplicationModel
    {
        public int Id { get; set; }

        public string DateAndTime { get; set; }
    }
}
