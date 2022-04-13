namespace App.Server.Data.Models.Base
{
    using System;

    public interface IDeletableEntity : IEntity
    {
        public DateTime? DeletedOn { get; set; }
        
        public string DeletedBy { get; set; }
        
        public bool IsDeleted { get; set; }

    }
}