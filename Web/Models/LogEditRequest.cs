namespace Web.Models
{
    public class LogEditRequest
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }

        public Logs.BLL.Entities.Log ToLog()
        {
            return new Logs.BLL.Entities.Log
            {
                Description = Description,
                LogTypeId = TypeId
            };
        }
    }
}
