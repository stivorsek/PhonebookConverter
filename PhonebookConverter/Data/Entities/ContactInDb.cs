namespace PhonebookConverter.Data.Entities
{
    public class ContactInDb : EntityBase
    {
        public string Name { get; set; }
        public int? Phone1 { get; set; }
        public int? Phone2 { get; set; }
        public int? Phone3 { get; set; }

    }
}
