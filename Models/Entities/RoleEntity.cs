namespace Курсова_робота.Models.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoleOperationEntity> RoleOperations { get; set; } = new List<RoleOperationEntity>();
    }
}
