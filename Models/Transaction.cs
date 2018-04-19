using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankaccounts.Models{
    public class Transaction: BaseEntity{
        
        public int TransactionId {get; set;}
        [Required]
        [DataType("float")]
        public float Amount {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        [ForeignKey("User")]
        public int UsersId {get; set;}
        public User User {get; set;}
    }
}