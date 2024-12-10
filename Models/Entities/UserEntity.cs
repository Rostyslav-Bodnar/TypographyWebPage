using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Курсова_робота.Models.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int IdRole { get; set; }

        public RoleEntity Role { get; set; }

        public bool IsBanned { get; set; }

        public IEnumerable<PurchaseOrderEntity> PurchaseOrderEntities { get; set; }
    }

}
