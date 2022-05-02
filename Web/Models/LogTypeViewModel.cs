namespace Web.Models
{
    public class LogTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static LogTypeViewModel FromLogType(Logs.BLL.Entities.LogType item)
        {
            return new LogTypeViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}
