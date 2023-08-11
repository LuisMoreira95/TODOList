namespace TODOList.API.Models.Domain
{
    public class Category_Todo
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid TodoId { get; set; }
        public Todo Todo { get; set; }

    }
}
