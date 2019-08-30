using System;
using System.Collections.Generic;

namespace AppAnimals.Models
{
    public partial class Animals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AnimalLocationId { get; set; }
        public int? AnimalRegionId { get; set; }
        public int? AnimalTypeId { get; set; }
        public int? AnimalSkinColorId { get; set; }
        
        public virtual AnimalLocation AnimalLocation { get; set; }
        public virtual AnimalRegion AnimalRegion { get; set; }
        public virtual AnimalSkinColor AnimalSkinColor { get; set; }
        public virtual AnimalType AnimalType { get; set; }
    }
}
