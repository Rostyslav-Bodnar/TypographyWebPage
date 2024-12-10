
using System.ComponentModel.DataAnnotations.Schema;

namespace Курсова_робота.Models.Entities
{
    public class RoleOperationEntity
    {
        public int IdRole { get; set; }
        public RoleEntity Role { get; set; }

        public int IdOperation { get; set; }
        public OperationEntity Operation { get; set; }
    }

}
