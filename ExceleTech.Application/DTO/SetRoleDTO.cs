using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Application.DTO
{
    public sealed class SetRoleDTO(string RoleName, Guid UserId)
    {
        public string RoleName { get; set; }
        public Guid UserId { get; set; } 
    }



}
