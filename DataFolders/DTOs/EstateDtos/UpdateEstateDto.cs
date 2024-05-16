namespace DTOs.EstateDtos
{
    public class UpdateEstateDto
    {
        //Id needed for finding the correct object via the api updating endpoint.
        public int Id { get; set; }
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

        //bool for setting of the object should show up on lists or not.
        public bool OnTheMarket { get; set; } = true;

        //Relational forgine keys because this is an update object
        //and only needs to change the Id of what ever object it should be connected to
        public int CountyId { get; set; }
        public int CategoryId { get; set; }
        public string? RealtorId { get; set; }

    }
}
