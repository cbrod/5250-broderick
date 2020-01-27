namespace Mine.Models
{
    /// <summary>
    /// Item for the Game
    /// </summary>
    public class ItemModel : BaseModel
    {
        // Add Unique attributes for Item

        // the Value of the item
        public int Value { get; set; } = 0;

        /// <summary>
        /// Update the ItemModel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(ItemModel data)
        {
            // Update Base
            Name = data.Name;
            Description = data.Description;

            // Update the extended
            Value = data.Value;

            return true;
        }
    }
}