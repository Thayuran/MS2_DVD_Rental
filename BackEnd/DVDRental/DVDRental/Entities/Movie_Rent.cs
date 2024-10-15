﻿namespace DVDRental.Entities
{
    public class Movie_Rent
    {
        public int ID { get; set; }
       
        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public decimal AdvancePayment {  get; set; }

        public decimal RentalCharge {  get; set; }

        public decimal? DelayFine { get; set; }
        public decimal? DamageFine { get; set; }

        public decimal Balance { get; set; }
        public string Status { get; set; }


        public MovieDvd dvd { get; set; }
        public int dvdId { get; set; }

        public Customer customer { get; set; }
        public int customerID {  get; set; }

    }
}
