namespace DTOs.EstateDtos
{
    public class InsertEstateDto
    {
        public string Address { get; set; } = string.Empty;
        public int StartingPrice { get; set; }

        public int LivingAreaKvm { get; set; }

        public int NumberOfRooms { get; set; }

        public int BiAreaKvm { get; set; }

        public int EstateAreaKvm { get; set; }

        public int MonthlyFee { get; set; }

        public int RunningCosts { get; set; }

        public DateOnly? ConstructionDate { get; set; }

        public string EstateDescription { get; set; } = string.Empty;

        public DateOnly? PublishDate { get; set; } = new DateOnly();

        public bool OnTheMarket { get; set; }
        //Relational
        public int CountyId { get; set; }
        public string? RealtorId { get; set; }
        public int CategoryId { get; set; }
    }
}
